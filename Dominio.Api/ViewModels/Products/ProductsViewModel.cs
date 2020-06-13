using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Api.ViewModels.Products
{
    public class ProductsViewModel
    {
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        public int amount { get; set; }
        public int avalaible { get; set; }
    }
}
