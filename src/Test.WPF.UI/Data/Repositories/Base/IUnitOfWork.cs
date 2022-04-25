using System;

namespace Test.WPF.UI.Data.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }

        int SaveChanges();
    }
}