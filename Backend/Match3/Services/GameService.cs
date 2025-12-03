using Match3.Models;
using Match3.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Match3.Services
{
    // Administrar multiples partidas
    public class GameService
    {
        private readonly ConcurrentDictionary<string, Game> games = new();
        private readonly CreateBoards createBoards;

        public GameService(CreateBoards _createBoards)
        {
            createBoards = _createBoards;
        }

        // Iniciar una partida nueva
        public Game CreateGame()
        {
            var board = createBoards.CreateBoard();
            var game = new Game(board);
            games.TryAdd(game.GameId, game);
            return game;
        }

        // Obtener un partida por id (si existe)
        public bool TryGetGame(string gameId, out Game? game) => games.TryGetValue(gameId, out game);

        public bool RemoveGame(string gameId) => games.TryRemove(gameId, out _);

        public IEnumerable<Game> GetAllGames() => games.Values;

        public GameStateDto GameToDto(Game game)
        {
            return new GameStateDto
            {
                GameId = game.GameId,
                Theme = game.Theme,
                State = game.State,
                Board = game.Board.SimpleBoard(),
                Players = game.Players.Select(p => new { p.PlayerId, p.Nickname, p.PlayerScore, p.IsReady }).ToList()
            };
        }
    }
}
