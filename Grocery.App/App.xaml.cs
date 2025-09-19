using Grocery.App.ViewModels;
using Grocery.App.Views;

namespace Grocery.App
{
    public partial class App : Application
    {
        public App(LoginViewModel viewModel) // uncommented
        {
            InitializeComponent();

            //MainPage = new AppShell(); //commented
            MainPage = new LoginView(viewModel); //uncommented
        }
    }
}
