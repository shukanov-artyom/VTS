using System;
using SportsStore.Domain.Abstract;
using System.Collections.Generic;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        public IEnumerable<Entities.Product> Products
        {
            get
            {
                return new EFDbContext().Products;
            }
        }
    }
}
