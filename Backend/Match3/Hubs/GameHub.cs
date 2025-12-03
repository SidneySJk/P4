using Match3.Models;
using Match3.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using static Match3.Models.CreateGameDto;

namespace Match3.Hubs
{
    public class GameHub : Hub
    {
        private readonly GameService _gameService;

        public GameHub(GameService gameService)
        {
            _gameService = gameService;
        }

        public async Task JoinGame(string gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Clients.Group(gameId).SendAsync("PlayerJoined", Context.ConnectionId);
        }

        public async Task LeaveGame(string gameId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
            await Clients.Group(gameId).SendAsync("PlayerLeft", Context.ConnectionId);
        }

        // Cliente pide swap — servidor valida y emite nuevo estado si es válido
        public async Task<bool> MakeSwap(string gameId, SwapDto swap)
        {
            if (!_gameService.TryGetGame(gameId, out var game)) return false;

            var player = game.Players.FirstOrDefault(p => p.PlayerId == swap.PlayerId);
            if (player == null) return false;

            var ok = game.ProcessSwap(player, swap.Row1, swap.Col1, swap.Row2, swap.Col2);
            if (ok)
            {
                var board = game.StartGame(); // o serializar a SimpleBoard
                await Clients.Group(gameId).SendAsync("BoardUpdated", board);
            }

            return ok;
        }
    }
}
