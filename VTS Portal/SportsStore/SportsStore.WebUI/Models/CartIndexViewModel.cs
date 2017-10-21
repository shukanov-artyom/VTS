using System;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Models
{
    // Cart -> Index -> View - and we need a model for it
    // model is not a domain object! it is more looks like a MVVM ViewModel class
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }

        public string ReturnUrl { get; set; }
    }
}