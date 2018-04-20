using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace web.Models
{
    public class CreditCardModel
    {
        public CreditCardModel()
        {
            ApplicationUser  = new HashSet<ApplicationUserModel>();
            Order = new HashSet<OrderModel>();
        }
        
        [Required(ErrorMessage = "Please enter the first name on credit card")]
        [StringLength(45)]
        [Display(Name = "First Name")]
        public string CreditCardFirstName { get; set; }
        
        [Required(ErrorMessage = "Please enter the last name on credit card")]

        [Display(Name = "Last Name")]
        public string CreditCardLastName { get; set; }
        
        [Required(ErrorMessage = "Please enter the expiration date on credit card")]
        [Display(Name = "Expiration Date")]
        public DateTime CreditCardExpirationDate { get; set; }
        
        [Required(ErrorMessage = "Please enter credit card number")]
        [Display(Name = "Credit Card Number")]
        [StringLength(50)]
        public string CreditCardNumber { get; set; }
        
        [Required(ErrorMessage = "Please enter credit card CVV")]
        [Display(Name = "CVV")]
        [StringLength(3)]
        public int CreditCardCvv { get; set; }
        
        public ICollection<ApplicationUserModel> ApplicationUser { get; set; }
        public ICollection<OrderModel> Order { get; set; }
    }
}