using System;
using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repo;

        public int PageSize
        {
            get;
            set;
        }

        public ProductController(IProductRepository repo)
        {
            this.repo = repo;
            PageSize = 4;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel();
            model.PageInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = repo.Products.Count(p => p.Category.Equals(category))
            };
            model.Products = repo.Products.
                Where(p=>p.Category == null || p.Category.Equals(category)).
                OrderBy(p => p.ProductId).
                Skip((page - 1)*PageSize).
                Take(PageSize);
            model.CurrentCategory = category;
            return View(model);
        }
    }
}