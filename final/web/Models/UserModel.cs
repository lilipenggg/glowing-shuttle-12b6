using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First Name")]
        [StringLength(30)]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First Name")]
        [StringLength(30)]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Please enter your shipping address")]
        [Display(Name = "Address Line 1")]
        [StringLength(200)]
        public string ShippingAddress { get; set; }
        
        [Display(Name = "Address Line 2")]
        public string ShippingAddress2 { get; set; }
        
        [Required(ErrorMessage = "Please enter your shipping city")]
        [StringLength(45)]
        public string UserShippingCity { get; set; }
        
        [Required(ErrorMessage = "Please enter your shipping state")]
        [StringLength(45)]
        public string UserShippingState { get; set; }
        
        [Required(ErrorMessage = "Please enter your shipping zip code")]
        [Display(Name = "Zip Code")]
        [StringLength(5)]
        public int UserShippingZipCode { get; set; }
        
        [Required(ErrorMessage = "Please enter your billing address")]
        [Display(Name = "Address Line 1")]
        [StringLength(200)]
        public string UserBillingAddress { get; set; }
        
        [Display(Name = "Address Line 2")]
        public string UserBillingAddress2 { get; set; }
        
        [Required(ErrorMessage = "Please enter your shipping city")]
        [StringLength(45)]
        public string UserBillingCity { get; set; }
        
        [Required(ErrorMessage = "Please enter your shipping state")]
        [StringLength(45)]
        public string UserBillingState { get; set; }
        
        [Required(ErrorMessage = "Please enter your shipping zip code")]
        [Display(Name = "Zip Code")]
        [StringLength(5)]
        public int UserBillingZipCode { get; set; }

        public CreditCardModel UserCreditCard { get; set; }
    }
}