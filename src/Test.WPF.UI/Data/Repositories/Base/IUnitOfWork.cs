using System;
using NHibernate;

namespace Test.WPF.UI.Data.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        ISession Session { get; }

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}