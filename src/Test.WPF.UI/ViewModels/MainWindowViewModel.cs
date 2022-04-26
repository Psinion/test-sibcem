using Remotion.Linq.Collections;
using Test.WPF.UI.Data.Models;
using Test.WPF.UI.Data.Repositories;
using Test.WPF.UI.Data.Repositories.Base;
using Test.WPF.UI.ViewModels.Base;

namespace Test.WPF.UI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private UnitOfWork unitOfWork;
        private UsersRepository usersRepository;

        public ObservableCollection<User> Users { get; set; } =
            new ObservableCollection<User>();

        public MainWindowViewModel()
        {
            unitOfWork = new UnitOfWork();
            usersRepository = new UsersRepository(unitOfWork);

            unitOfWork.BeginTransaction();
            foreach (var user in usersRepository.GetAll())
            {
                Users.Add(user);
            }
            unitOfWork.CommitTransaction();
        }
    }
}
