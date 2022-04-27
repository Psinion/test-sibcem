using System.Collections.ObjectModel;
using System.Windows.Input;
using Test.WPF.UI.Commands;
using Test.WPF.UI.Data.Models;
using Test.WPF.UI.Data.Repositories;
using Test.WPF.UI.Data.Repositories.Base;
using Test.WPF.UI.Services;
using Test.WPF.UI.Services.Base;
using Test.WPF.UI.ViewModels.Base;

namespace Test.WPF.UI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IDialogService userEditorDialog;
        private readonly IDialogService userPrivilegesDialog;

        private UnitOfWork unitOfWork;
        private UsersRepository usersRepository;

        private ObservableCollection<User> users = 
            new ObservableCollection<User>();

        private User selectedUser;

        public ObservableCollection<User> Users { get => users; set => Set(ref users, value); }

        public User SelectedUser { get => selectedUser; set => Set(ref selectedUser, value); }

        #region Commands

        public ICommand AddUserCommand { get; }
        public ICommand ChangeUserCommand { get; }
        public ICommand RemoveUserCommand { get; }

        public ICommand ChangeUserPrivilegesCommand { get; }

        #endregion


        public MainWindowViewModel()
        {
            userEditorDialog = new UserEditorDialogService();
            userPrivilegesDialog = new UserPrivilegesEditorDialogService();

            unitOfWork = new UnitOfWork();
            usersRepository = new UsersRepository(unitOfWork);

            AddUserCommand = new RelayCommand(OnAddUser);
            ChangeUserCommand = new RelayCommand(OnChangeUser, CanChangeUser);
            RemoveUserCommand = new RelayCommand(OnRemoveUser, CanRemoveUser);
            ChangeUserPrivilegesCommand = new RelayCommand(OnChangeUserPrivileges, CanChangeUserPrivileges);

            Refresh();
        }

        #region Command Methods

        private void OnAddUser(object obj)
        {
            User newUser = new User();

            if (userEditorDialog.Edit(newUser))
            {
                unitOfWork.BeginTransaction();
                usersRepository.Save(newUser);
                unitOfWork.CommitTransaction();
                
                Refresh();
            }
        }

        private bool CanChangeUser(object obj)
        {
            return obj is User;
        }

        private void OnChangeUser(object obj)
        {
            if (obj is User user && userEditorDialog.Edit(user))
            {
                unitOfWork.BeginTransaction();
                usersRepository.Save(user);
                unitOfWork.CommitTransaction();

                Refresh();
            }
        }

        private bool CanRemoveUser(object obj)
        {
            return obj is User;
        }

        private void OnRemoveUser(object obj)
        {
            if (obj is User user)
            {
                unitOfWork.BeginTransaction();
                usersRepository.Delete(user);
                unitOfWork.CommitTransaction();

                Refresh();
            }
        }

        private bool CanChangeUserPrivileges(object obj)
        {
            return obj is User;
        }

        private void OnChangeUserPrivileges(object obj)
        {
            if (obj is User user && userPrivilegesDialog.Edit(user))
            {
                /*unitOfWork.BeginTransaction();
                usersRepository.Save(user);
                unitOfWork.CommitTransaction();

                Refresh();*/
            }
        }

        #endregion

        #region Private Methods

        private void Refresh()
        {
            Users.Clear();
            unitOfWork.BeginTransaction();
            foreach (var user in usersRepository.GetAll())
            {
                Users.Add(user);
            }
            unitOfWork.CommitTransaction();
        }

        #endregion
    }
}
