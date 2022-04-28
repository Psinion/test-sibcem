using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
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

        private IUnitOfWork unitOfWork;
        private ITuiViewModelsRepository tuiViewModelsRepository;

        #endregion

        #region Properties

        public IEnumerable<TuiViewModel> RootViewModels { get => rootViewModels; set => Set(ref rootViewModels, value); }
        public TuiViewModel SelectedViewModel
        {
            get => selectedViewModel;
            set => Set(ref selectedViewModel, value);
        }


        #endregion

        #region Events

        public event EventHandler<DialogResult<bool>> OnComplete;

        #endregion

        #region Commands

        public ICommand SelectViewModelCommand { get; set; }

        #endregion

        #region Constructors

        public UserPrivilegesViewModel() { }

        public UserPrivilegesViewModel(User user)
        {
            this.user = user;
            unitOfWork = new UnitOfWork();
            tuiViewModelsRepository = new TuiViewModelsRepository(unitOfWork);

            SelectViewModelCommand = new RelayCommand(OnSelectViewModel);

            RootViewModels = tuiViewModelsRepository.GetRootViewModels();
        }

        #endregion

        #region Commands Methods

        private void OnSelectViewModel(object obj)
        {
            if(obj is TuiViewModel viewModel)
            {
                SelectedViewModel = viewModel;
            }
        }

        #endregion
    }
}