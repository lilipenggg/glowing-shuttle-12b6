using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace web.Models
{
    public class ApplicationUserModel
    {
        public ApplicationUserModel()
        {
            Order = new HashSet<OrderModel>();
            Product = new HashSet<ProductModel>();
        }

        public string ApplicationUserId { get; set; }
        
        [Required]
        [Display(Name = "First Name")]
        public string ApplicationUserFirstName { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        public string ApplicationUserLastName { get; set; }
        
        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email address is not entered in a correct format")]
        [Display(Name = "Email")]
        public string ApplicationUserEmail { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", 
            ErrorMessage = "The password must be over 8 characters long and contains at least one capitalilzd character.")]
        [Display(Name = "Password")]
        public string ApplicationUserPassword { get; set; }
        
        public string ApplicationUserTypeId { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Phone numer is not in a valid format.")]
        public string ApplicationUserPhoneNumber { get; set; }
        
        public int? ApplicationUserAwardPoints { get; set; }
        public string ApplicationUserCreditCardId { get; set; }

        public CreditCardModel ApplicationUserCreditCard { get; set; }
        public UserTypeModel ApplicationUserType { get; set; }
        public ICollection<OrderModel> Order { get; set; }
        public ICollection<ProductModel> Product { get; set; }
    }
}