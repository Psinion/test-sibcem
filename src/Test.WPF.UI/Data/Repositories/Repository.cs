using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Event.Default;
using NHibernate.Linq;
using Test.WPF.UI.Data.Models.Base;
using Test.WPF.UI.Data.Repositories.Base;

namespace Test.WPF.UI.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected IUnitOfWork UnitOfWork;

        public ISession Session => UnitOfWork.Session;

        #region Constructors

        public Repository(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        public T Get(int id)
        {
            return Session.Get<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Session.Query<T>().ToList();
        }

        public void Save(T item)
        {
            Session.SaveOrUpdate(item);
        }

        public void Delete(int id)
        {
            Session.Delete(id);
        }

        #endregion
    }
}