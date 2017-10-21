using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.Presentation.UserWorkspace
{
    public class UserWorkspaceController : UserControl
    {
        private Stack<UserControl> prePagesHistory =
            new Stack<UserControl>(4);
        private UserControl currentPage;
        private Stack<UserControl> postPagesHistory =
            new Stack<UserControl>(4);

        private Button buttonForward;
        private Button buttonBackward;
        private TextBlock textBlockForward;
        private TextBlock textBlockBackwards;
        private StackPanel stackPanelWorkspace;

        public UserWorkspaceController(StackPanel container, 
            Button buttonForward, Button buttonBackward, 
            TextBlock textBlockForward, TextBlock textBlockBackwards)
        {
            this.stackPanelWorkspace = container;
            this.buttonForward = buttonForward;
            this.buttonBackward = buttonBackward;
            this.textBlockBackwards = textBlockBackwards;
            this.textBlockForward = textBlockForward;
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

        public void NavigateToPage(UserControl page)
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

        private void DisplayPage(UserControl page)
        {
            stackPanelWorkspace.Children.Clear();
            stackPanelWorkspace.Children.Add(page);
        }
    }
}
