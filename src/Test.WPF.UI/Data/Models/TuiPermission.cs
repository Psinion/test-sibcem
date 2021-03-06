using System;
using Test.WPF.UI.Data.Models.Base;

namespace Test.WPF.UI.Data.Models
{
    public class TuiPermission : Entity
    {
        public virtual TuiViewModelAction ViewModelAction { get; set; }

        public virtual User User { get; set; }

        public virtual DateTime? DateExpire { get; set; }

        public TuiPermission()
        {

        }

        public TuiPermission(TuiViewModelAction action, User user, DateTime? dateExpire)
        {
            ViewModelAction = action;
            User = user;
            DateExpire = dateExpire;
        }
    }
}