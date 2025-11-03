using Microsoft.AspNetCore.Identity;

namespace HelloToAsp.Data
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual IList<ToDoList> ToDoLists { get; set; }
    }
}