using System;
using System.Collections.Generic;
using Test.WPF.UI.Data.Models.Base;

namespace Test.WPF.UI.Data.Repositories.Base
{
    public interface IRepository<T> where T : class, IEntity
    {
        T Get(int id);

        IEnumerable<T> GetAll();

        void Save(T item);

        void Delete(T item);
    }
}
