using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Html.Models;
using VTS.Site.DomainObjects;
using VTS.Site.WebService.Assemblers;

namespace Html.Controllers
{
    public class NewsManagementController : Controller
    {
        public ActionResult Index()
        {
            IList<SystemNewsViewModel> dataContext = 
                new List<SystemNewsViewModel>();
            NewsPersistency persistency = new NewsPersistency();
            foreach (SystemNews item in persistency.GetAll())
            {
                dataContext.Add(new SystemNewsViewModel(item));
            }
            return View(dataContext);
        }

        //
        // GET: /NewsManagement/Details/5

        public ActionResult Details(int id)
        {
            //return View();
            throw new NotImplementedException();
        }

        public ActionResult Create()
        {
            return View(new SystemNewsViewModel());
        }

        //
        // POST: /NewsManagement/Create

        [HttpPost]
        public ActionResult Create(SystemNewsViewModel model)
        {
            try
            {
                SystemNews item = model.GetModel();
                NewsPersistency persistency = new NewsPersistency();
                persistency.Persist(item);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            NewsPersistency persistency = new NewsPersistency();
            SystemNews item = persistency.Get(id);
            SystemNewsViewModel model = new SystemNewsViewModel(item);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SystemNewsViewModel model)
        {
            try
            {
                NewsPersistency persistency = new NewsPersistency();
                persistency.Update(model.GetModel());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            NewsPersistency persistency = new NewsPersistency();
            SystemNews item = persistency.Get(id);
            SystemNewsViewModel model = new SystemNewsViewModel(item);
            return View(model);
        }

        //
        // POST: /NewsManagement/Delete/5

        [HttpPost]
        public ActionResult Delete(SystemNewsViewModel model)
        {
            try
            {
                NewsPersistency persistency = new NewsPersistency();
                persistency.Delete(model.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
