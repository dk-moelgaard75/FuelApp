using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FuelApp.Models
{
    public class UserModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid GID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required(), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(), DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        [Display(Name = "Confirm password")]
        [Required(), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool IsEmailConfirmed { get; set; }
        public System.DateTime? EmailConfirmationDate { get; set; }

        //Add List of Vehicle object later for Entity framework reference

    }
}
