using System.Collections;
using System.Collections.Generic;
using Test.WPF.UI.Data.Models.Base;

namespace Test.WPF.UI.Data.Models
{
    public class TuiViewModelAction : Entity
    {
        public virtual TuiViewModel ViewModel { get; set; }

        public virtual string Name { get; set; }

        public virtual string Note { get; set; }

        public virtual IList<TuiPermission> Permissions { get; set; }

        public TuiViewModelAction()
        {
            Permissions = new List<TuiPermission>();
        }
    }
}