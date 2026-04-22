using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Parcial1Cejas.Model;
using Parcial1Cejas.Messages;
using Parcial1Cejas.Services;
using Parcial1Cejas.Views;
using System.Collections.ObjectModel;

namespace Parcial1Cejas.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ApiService _api = new ApiService();

        // Este comando hace visible la lista
        [ObservableProperty]
        private ObservableCollection<Game> games = new();

        // Este comando muestra la lista original
        private List<Game> allGames = new();

        [ObservableProperty]
        private string status = "";

        [ObservableProperty]
        private string searchText = "";

        public MainViewModel()
        {
            WeakReferenceMessenger.Default.Register<GameAddedMessage>(this, (r, m) =>
            {
                Games.Insert(0, m.Game);
                allGames.Insert(0, m.Game);
            });

            // carga automática
            LoadGamesCommand.Execute(null);
        }

        // Con esta clase se carga la API
        [RelayCommand]
        public async Task LoadGames()
        {
            try
            {
                Status = "Cargando los datos desde la API";

                var list = await _api.GetGames();

                allGames = list;

                Games.Clear();

                foreach (var game in list)
                {
                    Games.Add(game);
                }

                Status = $"Se cargaron {Games.Count} juegos de la lista";
            }
            catch (Exception ex)
            {
                Status = ex.Message;
            }
        }

        // Esto es un filtro que se aplica sin que haya errores en la lista
        [RelayCommand]
        public void FilterGames()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Games.Clear();

                foreach (var g in allGames)
                    Games.Add(g);

                return;
            }

            var filtered = allGames
                .Where(g => g.Name != null &&
                            g.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Games.Clear();

            foreach (var g in filtered)
                Games.Add(g);
        }

        // Esto muestra los detalles de cada objeto
        [RelayCommand]
        public async Task GoToDetail(Game game)
        {
            if (game == null) return;

            await Shell.Current.GoToAsync(nameof(DetailPage), true,
                new Dictionary<string, object>
                {
                    { "Game", game }
                });
        }

        // Con esto se agrega juegos
        [RelayCommand]
        public async Task GoToAdd()
        {
            await Shell.Current.GoToAsync(nameof(AddGamePage));
        }
    }
}