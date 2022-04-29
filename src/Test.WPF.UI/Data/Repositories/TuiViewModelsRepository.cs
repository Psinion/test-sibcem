using System.Collections;
using System.Collections.Generic;
using NHibernate.Criterion;
using Test.WPF.UI.Data.Models;
using Test.WPF.UI.Data.Repositories.Base;

namespace Test.WPF.UI.Data.Repositories
{
    public class TuiViewModelsRepository : Repository<TuiViewModel>, ITuiViewModelsRepository
    {
        public TuiViewModelsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IList<TuiViewModel> GetRootViewModels()
        {
            return Session.CreateCriteria<TuiViewModel>()
                .Add(Restrictions.Eq("Parent", null))
                .List<TuiViewModel>();
        }
    }
}