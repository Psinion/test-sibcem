using System.Collections;
using System.Collections.Generic;
using Test.WPF.UI.Data.Models.Base;

namespace Test.WPF.UI.Data.Models
{
    public class TuiViewModel : Entity
    {
        public virtual string Name { get; set; }

        public virtual TuiViewModel Parent { get; set; }

        public virtual ICollection<TuiViewModel> Children { get; set; }

        public TuiViewModel()
        {
            Children = new List<TuiViewModel>();
        }
    }
}