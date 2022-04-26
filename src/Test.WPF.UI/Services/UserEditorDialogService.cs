using System.Windows;
using Test.WPF.UI.Data.Models;
using Test.WPF.UI.Data.Models.Base;
using Test.WPF.UI.Services.Base;
using Test.WPF.UI.ViewModels;
using Test.WPF.UI.Views.Windows;

namespace Test.WPF.UI.Services
{
    public class UserEditorDialogService : IDialogService
    {
        public bool Edit(Entity entity)
        {
            if (entity is User user)
            {
                var viewModel = new UserEditorViewModel(user);
                var view = new UserEditorWindow
                {
                    DataContext = viewModel,
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                viewModel.OnComplete += (_, args) =>
                {
                    view.DialogResult = args;
                    view.Close();
                };

                return view.ShowDialog() ?? false;
            }
            
            return false;
        }
    }
}