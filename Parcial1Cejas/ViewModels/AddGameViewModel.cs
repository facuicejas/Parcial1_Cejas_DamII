using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Parcial1Cejas.Model;
using Parcial1Cejas.Services;
using Parcial1Cejas.Messages;

// Esta parte se encarga crear nuevos juegos cuando el usuario
// lo pide
namespace Parcial1Cejas.ViewModels
{
    public partial class AddGameViewModel : ObservableObject
    {
        private readonly ApiService _api = new ApiService();

        [ObservableProperty] private string? name;
        [ObservableProperty] private string? genre;
        [ObservableProperty] private string? developer;
        [ObservableProperty] private string? publisher;
        [ObservableProperty] private string? status;

        [RelayCommand]
        public async Task AddGame()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Status = "El nombre debe ser ingresado para agregar el juego";
                return;
            }

            try
            {
                var game = new Game
                {
                    Name = Name,
                    Genre = Genre,
                    Developer = Developer,
                    Publisher = Publisher
                };

                await _api.AddGame(game);

                // Esto sincroniza la lista principal de juegos
                WeakReferenceMessenger.Default.Send(new GameAddedMessage
                {
                    Game = game
                });

                Status = "Juego agregado correctamente";
            }
            catch (Exception ex)
            {
                Status = ex.Message;
            }
        }
    }
}