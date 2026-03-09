using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace P2FixAnAppDotNetCode.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.Order),ErrorMessageResourceName = "ErrorMissingName")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.Order), ErrorMessageResourceName = "ErrorMissingAddress")]
        public string Address { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.Order), ErrorMessageResourceName = "ErrorMissingCity")]
        public string City { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.Order), ErrorMessageResourceName = "ErrorMissingCountry")]
        public string Country { get; set; }

        [BindNever]
        public DateTime Date { get; set; }
    }
}
