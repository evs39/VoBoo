using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoBoo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public IEnumerable<UserDictionary> Dictionaries { get; set; }
    }
}
