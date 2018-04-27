using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class OrderModel
    {
        public OrderModel()
        {
            OrderItem = new HashSet<OrderItemModel>();
        }

        public string OrderId { get; set; }
        // optional data fields - available to registered customers only
        public DateTime OrderDateTime { get; set; }
        public int OrderAppliedAwardPoints { get; set; }
        public double OrderAppliedDiscount { get; set; }
        public double OrderTotal { get; set; }
        public int OrderAwardPoints { get; set; }
        
        [Required(ErrorMessage = "Please enter your shipping address")]
        [Display(Name = "Shipping Address Line 1")]
        [StringLength(300)]
        public string OrderShippingAddress1 { get; set; }
        
        [Display(Name = "Shipping Address Line 2")]
        public string OrderShippingAddress2 { get; set; }
        
        [Required(ErrorMessage = "Please enter your shipping city")]
        [Display(Name = "Shipping City")]
        [StringLength(45)]
        public string OrderShippingCity { get; set; }
        
        [Required(ErrorMessage = "Please enter your shipping state")]
        [Display(Name = "Shipping State")]
        [StringLength(45)]
        public string OrderShippingState { get; set; }
        
        [Required(ErrorMessage = "Please enter your shipping zip code")]
        [Display(Name = "Shipping Zip Code")]
        public int OrderShippingZipCode { get; set; }
        
        [Required(ErrorMessage = "Please enter your billing address")]
        [Display(Name = "Billing Address Line 1")]
        [StringLength(300)]
        public string OrderBillingAddress1 { get; set; }
        
        [Display(Name = "Billing Address Line 2")]
        public string OrderBillingAddress2 { get; set; }
        
        [Required(ErrorMessage = "Please enter your billing city")]
        [Display(Name = "Billing City")]
        [StringLength(45)]
        public string OrderBillingCity { get; set; }
        
        [Required(ErrorMessage = "Please enter your billing state")]
        [Display(Name = "Billing State")]
        [StringLength(45)]
        public string OrderBillingState { get; set; }
        
        [Required(ErrorMessage = "Please enter your billing zipcode")]
        [Display(Name = "Billing Zipcode")]
        public string OrderBillingZipCode { get; set; }
        
        [Required(ErrorMessage = "Please enter your shipping first name")]
        [Display(Name = "Shipping First Name")]
        [StringLength(45)]
        public string OrderShippingFirstName { get; set; }
        
        [Required(ErrorMessage = "Please enter your shipping last name")]
        [Display(Name = "Shipping Last Name")]
        [StringLength(45)]
        public string OrderShippingLastName { get; set; }
        
        [Required(ErrorMessage = "Please enter your billing first name")]
        [Display(Name = "Billing First Name")]
        [StringLength(45)]
        public string OrderBillingFirstName { get; set; }
        
        [Required(ErrorMessage = "Please enter your billing last name")]
        [Display(Name = "Billing Last Name")]
        [StringLength(45)]
        public string OrderBillingLastName { get; set; }

        public CreditCardModel OrderCreditCard { get; set; }
        public ApplicationUserModel OrderCustomer { get; set; }
        public ICollection<OrderItemModel> OrderItem { get; set; }
    }
}