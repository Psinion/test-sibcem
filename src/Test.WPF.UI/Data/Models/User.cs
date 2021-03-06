using System.Collections;
using System.Collections.Generic;
using Test.WPF.UI.Data.Models.Base;

namespace Test.WPF.UI.Data.Models
{
    public class User : Entity
    {
        public virtual string Login { get; set; }

        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }

        public virtual IList<TuiPermission> Permissions { get; set; }

        public User()
        {
            Permissions = new List<TuiPermission>();
        }
    }
}
