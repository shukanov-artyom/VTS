using System;
using System.Collections.Generic;
using SportsStore.Domain;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public PagingInfo PageInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}