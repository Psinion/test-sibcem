using System.Collections.Generic;
using Test.WPF.UI.Data.Models;

namespace Test.WPF.UI.Data.Repositories.Base
{
    public interface ITuiViewModelsRepository : IRepository<TuiViewModel>
    {
        IList<TuiViewModel> GetRootViewModels();
    }
}