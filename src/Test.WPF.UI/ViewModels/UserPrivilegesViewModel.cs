using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NHibernate.Type;
using Test.WPF.UI.Commands;
using Test.WPF.UI.Data.Models;
using Test.WPF.UI.Data.Repositories;
using Test.WPF.UI.Data.Repositories.Base;
using Test.WPF.UI.Services;
using Test.WPF.UI.ViewModels.Base;

namespace Test.WPF.UI.ViewModels
{
    public class UserPrivilegesViewModel : BaseViewModel
    {
        #region Fields

        private readonly User user;
        private IEnumerable<TuiViewModel> rootViewModels;
        private TuiViewModel selectedViewModel;
        private TuiViewModelAction selectedAction;
        private TuiPermission selectedPermission;

        private DateTime selectedDateExpire;
        private bool dateExpireToggle;

        private bool changed;

        private readonly IUnitOfWork unitOfWork;
        private readonly IUsersRepository usersRepository;
        private readonly ITuiViewModelsRepository tuiViewModelsRepository;
        private readonly ITuiViewModelActionsRepository tuiViewModelActionsRepository;
        private readonly ITuiPermissionsRepository tuiPermissionsRepository;

        #endregion

        #region Properties

        public IEnumerable<TuiViewModel> RootViewModels { get => rootViewModels; set => Set(ref rootViewModels, value); }
        public TuiViewModel SelectedViewModel
        {
            get => selectedViewModel;
            set => Set(ref selectedViewModel, value);
        }

        public ObservableCollection<TuiViewModelAction> Actions { get; set; } =
            new ObservableCollection<TuiViewModelAction>();
        public TuiViewModelAction SelectedAction { get => selectedAction; set => Set(ref selectedAction, value); }

        public ObservableCollection<TuiPermission> Permissions { get; set; } =
            new ObservableCollection<TuiPermission>();
        public TuiPermission SelectedPermission { get => selectedPermission; set => Set(ref selectedPermission, value); }

        public DateTime SelectedDateExpire { get => selectedDateExpire; set => Set(ref selectedDateExpire, value); }

        public bool DateExpireToggle { get => dateExpireToggle; set { Set(ref dateExpireToggle, value); } }

        #endregion

        #region Events

        public event EventHandler<DialogResult<bool>> OnComplete;

        #endregion

        #region Commands

        public ICommand SelectViewModelCommand { get; set; }

        public ICommand AddPermissionCommand { get; set; }
        public ICommand RemovePermissionCommand { get; set; }

        public ICommand ProceedCommand { get; set; }

        #endregion

        #region Constructors

        public UserPrivilegesViewModel() { }

        public UserPrivilegesViewModel(User user)
        {
            this.user = user;
            unitOfWork = new UnitOfWork();
            usersRepository = new UsersRepository(unitOfWork);
            tuiViewModelsRepository = new TuiViewModelsRepository(unitOfWork);
            tuiViewModelActionsRepository = new TuiViewModelActionsRepository(unitOfWork);
            tuiPermissionsRepository = new TuiPermissionsRepository(unitOfWork);

            SelectedDateExpire = DateTime.Now;
            changed = false;

            SelectViewModelCommand = new RelayCommand(OnSelectViewModel);
            AddPermissionCommand = new RelayCommand(OnAddPermission, CanAddPermission);
            RemovePermissionCommand = new RelayCommand(OnRemovePermission, CanRemovePermission);
            ProceedCommand = new RelayCommand(OnProceed, CanProceed);

            RootViewModels = tuiViewModelsRepository.GetRootViewModels();
        }

        #endregion

        #region Commands Methods

        private void OnSelectViewModel(object obj)
        {
            if(obj is TuiViewModel viewModel)
            {
                SelectedViewModel = viewModel;

                changed = false;

                Permissions.Clear();
                foreach (var permission in tuiPermissionsRepository.GetPermissions(user.Id, viewModel.Id) /*user.Permissions*/)
                {
                    Permissions.Add(permission);
                }

                Actions.Clear();
                foreach (var action in tuiViewModelActionsRepository.GetFreeActionsOfViewModel(viewModel.Id, user.Id))
                {
                    Actions.Add(action);
                }
            }
        }

        private bool CanAddPermission(object obj)
        {
            return obj is TuiViewModelAction;
        }

        private void OnAddPermission(object obj)
        {
            if (obj is TuiViewModelAction action)
            {
                changed = true;

                Actions.Remove(action);
                DateTime? expiredDate = null;
                if (dateExpireToggle)
                {
                    expiredDate = selectedDateExpire;
                }
                Permissions.Add(new TuiPermission(action, user, expiredDate));
            }
        }

        private bool CanRemovePermission(object obj)
        {
            return obj is TuiPermission;
        }

        private void OnRemovePermission(object obj)
        {
            if (obj is TuiPermission permission)
            {
                changed = true;

                Permissions.Remove(permission);
                Actions.Add(permission.ViewModelAction);
            }
        }

        private bool CanProceed(object obj)
        {
            return changed;
        }

        private void OnProceed(object obj)
        {
            user.Permissions = Permissions;

            unitOfWork.BeginTransaction();
            usersRepository.Save(user);
            unitOfWork.CommitTransaction();
        }

        #endregion
    }
}