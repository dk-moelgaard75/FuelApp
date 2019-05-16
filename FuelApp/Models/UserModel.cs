using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelApp.Models
{
    public class UserModel
    {
        //Decorate for DB ID
        public int Id { get; set; }
        public Guid GID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public System.DateTime? EmailConfirmationDate { get; set; }

        //Add List of Vehicle object later for Entity framework reference

    }
}
