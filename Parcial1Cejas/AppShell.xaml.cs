using Parcial1Cejas.Views;

namespace Parcial1Cejas
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
            Routing.RegisterRoute(nameof(AddGamePage), typeof(AddGamePage));
        }
    }
}
