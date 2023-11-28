using Microsoft.AspNetCore.Identity;
using System.Data.Common;

namespace TokenTest.Data.Entity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime BirthDate { get; set; }

        //NavProp
        public List<FavBook> FavBooks { get; set; }
        
    }
}
