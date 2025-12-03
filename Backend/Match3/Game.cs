using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3;

public class Game
{
    public string GameId { get; set; } = Guid.NewGuid().ToString().Substring(0, 8);
    public List<Player> Players = new List<Player>();
    public string State { get; set; } = "creada";
    public static int MAX_PLAYERS = 2;
    public string Theme { get; set; } = "pink";
    public static int TimeLimit { get; } = 3;
    public static int MatchLimit { get; } = 3;
    public int PlayerSelecting { get; set; } = 0;
    public readonly Board Board;
    public readonly Match Match;

    public Game(Board _board)
    {
        Board = _board;
        Match = new Match(_board);
    }

    // Añadir jugador al juego
    public void AddPlayer(Player player)
    {
        if (!CompletePlayers()) 
        {
            Players.Add(player);
        }
    }

    // Verificar si el juego tiene el número máximo de jugadores
    public bool CompletePlayers()
    {
        return Players.Count == MAX_PLAYERS;
    }

    // Iniciar el juego
    public Tile[,] StartGame()
    {
        State = "iniciada";
        return Board.Tiles;
    }

    // Cambiar el tema del juego
    public void ChangeTheme()
    {
        switch (Theme)
        {
            case "pink":
                Theme = "black";
                break;
            default:
                Theme = "pink";
                break;
        }
    }

    // Procesar el intercambio de fichas y actualizar el estado del tablero
    public bool ProcessSwap(Player player, int r1, int c1, int r2, int c2)
    {
        if (player == null) return false;
        if (!player._isReady) return false;
        if (!Board.Adjacent(r1, c1, r2, c2)) return false;
        Board.SwapTiles(r1, c1, r2, c2);
        if (!Match.MakeMatch())
        {
            Board.SwapTiles(r2, c2, r1, c1); 
            return false;
        }
        if (player.totalMatches <= MatchLimit) 
        {
            player.changeReady();
            player.totalMatches += 1;
            Match.UpdateMatch();
            while (Match.MakeMatch())
            {
                Match.UpdateMatch();
            }
        } else
        {
            player.changeReady();
        }
        player.AddScore((int)Match.Scored());
        //player.PlayerScore += Match.Scored();
        return true;
    }

    // Determinar el ganador del juego
    public Player SetWinner()
    {
        Player Winner = Players.OrderByDescending(p => p.PlayerScore).First();

        return Winner;
    }

    // Finalizar el juego y limpiar el estado
    public bool FinishGame()
    {
        for (int p = 0; p < Players.Count; p++)
        {
            if (Players[p].totalMatches != MatchLimit) return false;
            /*
            for (int c = 0; c < Board.WIDTH; c++)
            {
                Board.Tiles[r, c].ReleaseTile();
            }
            */
        }
        State = "finalizada";
        Players.Clear();
        return true;
    }

}

