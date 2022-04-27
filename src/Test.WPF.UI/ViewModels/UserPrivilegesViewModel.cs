using System;
using Test.WPF.UI.Data.Models;
using Test.WPF.UI.Services;
using Test.WPF.UI.ViewModels.Base;

namespace Test.WPF.UI.ViewModels
{
    public class UserPrivilegesViewModel : BaseViewModel
    {
        private readonly User user;

        public event EventHandler<DialogResult<bool>> OnComplete;

        public UserPrivilegesViewModel()
        {

        }

        public UserPrivilegesViewModel(User user)
        {
            this.user = user;
        }
    }
}