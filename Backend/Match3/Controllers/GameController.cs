using Microsoft.AspNetCore.Mvc;
using Match3.Services;
using Match3.Models;
using System.Linq;

namespace Match3.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GamesController : ControllerBase
    {
        private readonly GameService _gameService;

        public GamesController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public ActionResult CreateGame([FromBody] CreateGameDto dto)
        {
            var game = _gameService.CreateGame();
            if (!string.IsNullOrWhiteSpace(dto.Theme))
                game.Theme = dto.Theme!;
            if (dto.MaxPlayers.HasValue)
                Game.MAX_PLAYERS = dto.MaxPlayers.Value; 

            return CreatedAtAction(nameof(GetGame), new { id = game.GameId }, new { gameId = game.GameId });
        }

        [HttpGet("{id}")]
        public ActionResult<GameStateDto> GetGame(string id)
        {
            if (!_gameService.TryGetGame(id, out var game)) return NotFound();
            var dto = new GameStateDto
            {
                GameId = game.GameId,
                State = game.State,
                Theme = game.Theme,
                Board = game.StartGame(), 
                Players = game.Players.Select(p => new { p.PlayerId, p.Nickname, p.PlayerScore, p.IsReady })
            };
            return Ok(dto);
        }

        [HttpPost("{id}/join")]
        public ActionResult JoinGame(string id, [FromBody] JoinDto joinDto)
        {
            if (!_gameService.TryGetGame(id, out var game)) return NotFound();
            var player = new Player { Nickname = joinDto.Nickname };
            game.AddPlayer(player);
            return Ok(new { playerId = player.PlayerId, nickname = player.Nickname });
        }

        [HttpPost("{id}/start")]
        public ActionResult StartGame(string id)
        {
            if (!_gameService.TryGetGame(id, out var game)) return NotFound();
            var board = game.StartGame();
            return Ok(board);
        }

        [HttpPost("{id}/swap")]
        public ActionResult ProcessSwap(string id, [FromBody] SwapDto swap)
        {
            if (!_gameService.TryGetGame(id, out var game)) return NotFound();

            var player = game.Players.FirstOrDefault(p => p.PlayerId == swap.PlayerId);
            if (player == null) return BadRequest("Player not in game.");

            var ok = game.ProcessSwap(player, swap.Row1, swap.Col1, swap.Row2, swap.Col2);
            if (!ok) return BadRequest("Invalid swap or no match produced.");
            return Ok(game.StartGame()); 
        }
    }
}
