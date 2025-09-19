using Grocery.App.Views;

namespace Grocery.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(GroceryListItemsView), typeof(GroceryListItemsView));
            Routing.RegisterRoute(nameof(ProductView), typeof(ProductView));
            Routing.RegisterRoute(nameof(ChangeColorView), typeof(ChangeColorView));
            Routing.RegisterRoute("Login", typeof(LoginView));
        }
    }
}
