using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using NHibernate;
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

        private readonly IUnitOfWork unitOfWork;
        private readonly IUsersRepository usersRepository;

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
            usersRepository = unitOfWork.UsersRepository;

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

            unitOfWork.CloseSession();

            if (userEditorDialog.Edit(newUser))
            {
                unitOfWork.OpenSession();

                unitOfWork.BeginTransaction();
                usersRepository.Save(newUser);
                unitOfWork.CommitTransaction();
                
                Refresh();
            }
            else
            {
                unitOfWork.OpenSession();
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
                try
                {
                    unitOfWork.BeginTransaction();
                    usersRepository.Delete(user);
                    unitOfWork.CommitTransaction();
                }
                catch (HibernateException ex)
                {
                    unitOfWork.RollbackTransaction();
                    MessageBox.Show(ex.Message);
                }

                Refresh();
            }
        }

        private bool CanChangeUserPrivileges(object obj)
        {
            return obj is User;
        }

        private void OnChangeUserPrivileges(object obj)
        {
            unitOfWork.CloseSession();

            if (obj is User user)
            {
                userPrivilegesDialog.Edit(user);

                unitOfWork.OpenSession();

                Refresh();
            }
            else
            {
                unitOfWork.OpenSession();
            }
        }

        #endregion

        #region Private Methods

        private void Refresh()
        {
            Users.Clear();
            foreach (var user in usersRepository.GetAll())
            {
                Users.Add(user);
            }
        }

        #endregion
    }
}
