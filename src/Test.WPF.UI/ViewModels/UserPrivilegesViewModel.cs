using System;
using System.Collections;
using System.Collections.Generic;
using Test.WPF.UI.Data.Models;
using Test.WPF.UI.Data.Repositories;
using Test.WPF.UI.Data.Repositories.Base;
using Test.WPF.UI.Services;
using Test.WPF.UI.ViewModels.Base;

namespace Test.WPF.UI.ViewModels
{
    public class UserPrivilegesViewModel : BaseViewModel
    {
        private readonly User user;
        private IEnumerable<TuiViewModel> rootViewModelses;

        private IUnitOfWork unitOfWork;
        private ITuiViewModelsRepository tuiViewModelsRepository;

        public IEnumerable<TuiViewModel> RootViewModels { get => rootViewModelses; set => Set(ref rootViewModelses, value); }

        public event EventHandler<DialogResult<bool>> OnComplete;

        public UserPrivilegesViewModel() { }

        public UserPrivilegesViewModel(User user)
        {
            this.user = user;
            unitOfWork = new UnitOfWork();
            tuiViewModelsRepository = new TuiViewModelsRepository(unitOfWork);

            RootViewModels = tuiViewModelsRepository.GetRootViewModels();
        }
    }
}