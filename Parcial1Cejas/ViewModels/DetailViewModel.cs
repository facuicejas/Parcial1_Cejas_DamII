using CommunityToolkit.Mvvm.ComponentModel;
using Parcial1Cejas.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parcial1Cejas.ViewModels
{
    // Esta parte va a mostrar los datos de un juego en especifico
    // cuando es seleccionado

    [QueryProperty(nameof(Game), "Game")]
    public partial class DetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private Game? game;
    }

}
