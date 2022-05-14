using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Web_1.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string Mobileno { get; set; }

        [Required(ErrorMessage = "Enter the Birth date.")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public List<Customer> ShowallCustomer { get; set; }
    }
}