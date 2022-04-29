using Test.WPF.UI.Data.Models;
using Test.WPF.UI.Data.Repositories.Base;

namespace Test.WPF.UI.Data.Repositories
{
    public class TuiPermissionRepository : Repository<TuiPermission>, ITuiPermissionRepository
    {
        public TuiPermissionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}