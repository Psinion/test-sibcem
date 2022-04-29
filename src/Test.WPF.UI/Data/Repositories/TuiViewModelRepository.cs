using Test.WPF.UI.Data.Models;
using Test.WPF.UI.Data.Repositories.Base;

namespace Test.WPF.UI.Data.Repositories
{
    public class TuiViewModelRepository : Repository<TuiViewModelAction>, ITuiViewModelActionsRepository
    {
        public TuiViewModelRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}