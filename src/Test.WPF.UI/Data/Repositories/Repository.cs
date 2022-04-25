using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Event.Default;
using Test.WPF.UI.Data.Models;
using Test.WPF.UI.Data.Repositories.Base;

namespace Test.WPF.UI.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Fields

        protected readonly NHibernateHelper Provider;

        #endregion

        #region Constructor

        public Repository(NHibernateHelper provider)
        {
            this.Provider = provider;
        }

        #endregion

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(T item)
        {
            //User user = new User()
            //    {FirstName = "Иван", LastName = "Сидоров", MiddleName = "Иванович", Login = "Sidorov"};

            //Provider.CurrentSession.Save(user);
            //Provider.PerformCommit();

            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

    }
}