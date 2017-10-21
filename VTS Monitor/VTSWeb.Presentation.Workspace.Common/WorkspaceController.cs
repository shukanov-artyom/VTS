using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.Localization;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Workspace.Common
{
    public class WorkspaceController
    {
        private Stack<NavigatablePage> prePagesHistory =
            new Stack<NavigatablePage>(4);
        private NavigatablePage currentPage;
        private Stack<NavigatablePage> postPagesHistory =
            new Stack<NavigatablePage>(4);

        private Button buttonForward;
        private Button buttonBackward;
        private TextBlock textBlockForward;
        private TextBlock textBlockBackwards;
        private ContentControl contentControlWorkspace;
        private TextBlock currentPageHeader;

        public WorkspaceController(ContentControl contentControlWorkspace, 
            Button buttonForward, Button buttonBackward, 
            TextBlock textBlockForward, TextBlock textBlockBackwards, 
            TextBlock currentPageHeader)
        {
            this.contentControlWorkspace = contentControlWorkspace;
            this.buttonForward = buttonForward;
            this.buttonBackward = buttonBackward;
            this.textBlockBackwards = textBlockBackwards;
            this.textBlockForward = textBlockForward;
            this.currentPageHeader = currentPageHeader;
            //ApplicationSizeKeeper.AppResized += OnApplicationWindowResized;
            TranslationManager.Instance.LanguageChanged += OnLanguageChanged;
        }

        private void UpdateNavigation()
        {
            UpdateCanMoveForward();
            UpdateCanMoveBackwards();
        }

        private void UpdateCanMoveForward()
        {
            SetCanMoveForward(postPagesHistory.Count != 0);
        }

        private void UpdateCanMoveBackwards()
        {
            SetCanMoveBackwards(prePagesHistory.Count != 0);
        }

        private void SetCanMoveForward(bool can)
        {
            buttonForward.IsEnabled = can;
            textBlockForward.Visibility = can ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void SetCanMoveBackwards(bool can)
        {
            buttonBackward.IsEnabled = can;
            textBlockBackwards.Visibility = can ? Visibility.Visible :
                 Visibility.Collapsed;
        }

        public void NavigateBack()
        {
            if (currentPage == null)
            {
                throw new Exception(
                    "current page cannot be null if navigating back!");
            }
            postPagesHistory.Push(currentPage);
            currentPage = prePagesHistory.Pop();
            DisplayPage(currentPage);
            UpdateNavigation();
        }

        public void NavigateForward()
        {
            if (currentPage == null)
            {
                throw new Exception(
                    "current page cannot be null if navigating forward!");
            }
            prePagesHistory.Push(currentPage);
            currentPage = postPagesHistory.Pop();
            DisplayPage(currentPage);
            UpdateNavigation();
        }

        public void NavigateToPage(NavigatablePage page)
        {
            if (currentPage != null)
            {
                prePagesHistory.Push(currentPage);
            }
            currentPage = page;
            DisplayPage(page);
            UpdateCanMoveBackwards();
            UpdateCanMoveForward();
        }

        private void DisplayPage(NavigatablePage page)
        {
            contentControlWorkspace.Content = null;
            contentControlWorkspace.Content = page;
            currentPageHeader.Text = page.Header;
            UpdatePrePostPagesNames();
        }

        private void OnApplicationWindowResized(object sender, EventArgs e)
        {
            contentControlWorkspace.Height = 
                ApplicationSizeKeeper.Height - 215;
            contentControlWorkspace.Width =
                ApplicationSizeKeeper.Width;
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            currentPageHeader.Text=
                ((NavigatablePage) contentControlWorkspace.Content).Header;
        }

        private void UpdatePrePostPagesNames()
        {
            if (prePagesHistory.Count == 0)
            {
                textBlockBackwards.Text = CodeBehindStringResolver.Resolve(
                    "BackText");
            }
            else
            {
                string prePageName = prePagesHistory.Peek().Header;
                textBlockBackwards.Text = prePageName;
            }
            if (postPagesHistory.Count == 0)
            {
                textBlockForward.Text = CodeBehindStringResolver.Resolve(
                    "ForwardText");
            }
            else
            {
                string postPageName = postPagesHistory.Peek().Header;
                textBlockForward.Text = postPageName;
            }
        }
    }
}
