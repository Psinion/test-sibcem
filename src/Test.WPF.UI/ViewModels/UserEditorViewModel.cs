using System;
using System.Windows.Input;
using Test.WPF.UI.Commands;
using Test.WPF.UI.Data.Models;
using Test.WPF.UI.Services;
using Test.WPF.UI.ViewModels.Base;

namespace Test.WPF.UI.ViewModels
{
    public class UserEditorViewModel : BaseViewModel
    {
        #region Fields

        private readonly User user;

        private string title;

        private string firstName;
        private string lastName;
        private string middleName;
        private string login;

        #endregion

        #region Properties

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public string FirstName
        {
            get => firstName;
            set => Set(ref firstName, value);
        }

        public string LastName
        {
            get => lastName;
            set => Set(ref lastName, value);
        }

        public string MiddleName
        {
            get => middleName;
            set => Set(ref middleName, value);
        }

        public string Login
        {
            get => login;
            set => Set(ref login, value);
        }

        #endregion

        #region Events

        public event EventHandler<DialogResult<bool>> OnComplete;

        #endregion

        #region Commands

        public ICommand ProceedCommand { get; }
        public ICommand CancelCommand { get; }

        #endregion

        #region Constructors

        public UserEditorViewModel() : this(new User()) { }

        public UserEditorViewModel(User user)
        {
            this.user = user;

            FirstName = user.FirstName;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
            Login = user.Login;

            ProceedCommand = new RelayCommand(OnProceed, CanProceed);
            CancelCommand = new RelayCommand(OnCancel);

            Title = user.Id == 0 ? "Создание пользователя" : "Редактирование пользователя";
        }

        #endregion

        #region Commands Methods

        private bool CanProceed(object obj)
        {
            return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(middleName) && !string.IsNullOrEmpty(login);
        }

        private void OnProceed(object obj)
        {
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.MiddleName = MiddleName;
            user.Login = Login;

            OnComplete?.Invoke(this, true);
        }

        private void OnCancel(object obj)
        {
            OnComplete?.Invoke(this, false);
        }

        #endregion
    }
}