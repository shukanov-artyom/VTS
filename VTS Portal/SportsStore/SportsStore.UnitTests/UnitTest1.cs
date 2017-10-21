using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanPaginate()
        {
            ProductController controller = new ProductController(GenerateTestRepo());
            controller.PageSize = 3;

            var result = (ProductsListViewModel)controller.List(null, 2).Model;

            Product[] prodArray = result.Products.ToArray();
            Assert.AreEqual(prodArray.Length, 2);
            Assert.AreEqual("P4", prodArray[0].Name);
            Assert.AreEqual("P5", prodArray[1].Name);
        }

        [TestMethod]
        public void CanGenerateTestLinks()
        {
            HtmlHelper myHelper = null;
            PagingInfo pagInf = new PagingInfo()
            {
                CurrentPage = 2,
                TotalItems = 28, 
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => String.Format("Page {0}", i);
            MvcHtmlString result = myHelper.PageLinks(pagInf, pageUrlDelegate);
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page 1"">1</a><a class=""btn btn-default btn-primary selected"" href=""Page 2"">2</a><a class=""btn btn-default"" href=""Page 3"">3</a>", result.ToString());
        }

        [TestMethod]
        public void CanSendPaginationViewModel()
        {
            ProductController controller = new ProductController(GenerateTestRepo());
            controller.PageSize = 3;
            var result = (ProductsListViewModel)controller.List(null, 2).Model;
            PagingInfo pageInfo = result.PageInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void CanFilterProducts()
        {
            var controller = new ProductController(GenerateTestRepo());
            Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Name, "P2");
            Assert.AreEqual(result[0].Category, "Cat2");
            Assert.AreEqual(result[1].Name, "P4");
            Assert.AreEqual(result[1].Category, "Cat2");
        }

        [TestMethod]
        public void TestNavController()
        {
            Mock < IProductRepository > mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product(){ProductId = 1, Name = "P1", Category = "Apples"},
                new Product(){ProductId = 2, Name = "P2", Category = "Apples"},
                new Product(){ProductId = 3, Name = "P3", Category = "Plums"},
                new Product(){ProductId = 4, Name = "P4", Category = "Oranges"}
            });
            var repo = mock.Object;
            NavController target = new NavController(repo);
            string[] result = ((IEnumerable<string>)target.Menu().Model).ToArray();

            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "Apples");
            Assert.AreEqual(result[1], "Oranges");
            Assert.AreEqual(result[2], "Plums");
        }

        [TestMethod]
        public void IndicatesSelectedCategory()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product(){ProductId = 1, Name = "P1", Category = "Apples"},
                new Product(){ProductId = 2, Name = "P2", Category = "Oranges"}
            });
            var repo = mock.Object;
            NavController target = new NavController(repo);
            string categoryToSelect = "Apples";
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;
            Assert.AreEqual(categoryToSelect, result);
        }

        private IProductRepository GenerateTestRepo()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product(){ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product(){ProductId = 2, Name = "P2", Category = "Cat2"},
                new Product(){ProductId = 3, Name = "P3", Category = "Cat1"},
                new Product(){ProductId = 4, Name = "P4", Category = "Cat2"},
                new Product(){ProductId = 5, Name = "P5", Category = "Cat3"},
            });
            return mock.Object;
        }
    }
}
