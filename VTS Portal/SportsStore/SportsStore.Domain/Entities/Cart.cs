using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        private readonly List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.FirstOrDefault(l => l.Product.ProductId == product.ProductId);
            if (line == null)
            {
                line = new CartLine
                       {
                           Product = product,
                           Quantity = quantity
                       };
                lineCollection.Add(line);
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        public decimal ComputeTotalValue()
        {
            return (from line in lineCollection select line.Product.Price*line.Quantity).Sum();
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get
            {
                return lineCollection;
            }
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
