using Parcial1Cejas.ViewModels;

namespace Parcial1Cejas.Views;

public partial class DetailPage : ContentPage
{
    public DetailPage()
    {
        InitializeComponent();
        BindingContext = new DetailViewModel();
    }
}