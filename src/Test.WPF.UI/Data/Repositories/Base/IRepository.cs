using System.Collections.Generic;

namespace Test.WPF.UI.Data.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);

        IEnumerable<T> GetAll();

        void Add(T item);

        void Update(T item);

        void Remove(int id);
    }
}
