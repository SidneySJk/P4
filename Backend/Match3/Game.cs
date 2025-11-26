using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3;

public class Game
{
    private static Board Board = new Board();
    private List<List<char>> Tiles = Board.SimpleBoard();
    private static Match Match = new Match();
    private static Player player = new Player();
    public int GameId { get; set; }
    public static List<Player> Players = new List<Player>();
    public string State { get; set; } = "creada";
    public static int MAX_PLAYERS = 2;
    public string Theme { get; set; } = "pink";
    public static int TimeLimit { get; } = 3;
    public static int MatchLimit { get; } = 1;
    public int PlayerSelecting { get; set; } = 0;

    public void AddPlayer(Player player)
    {
        Players.Add(player);
    }

    public static bool CompletePlayers()
    {
        return Players.Count == MAX_PLAYERS;
    }

    public List<List<char>> StartGame()
    {
        State = "iniciada";
        return Tiles;
    }

    public static bool ProcessSwap(Player player, int r1, int c1, int r2, int c2)
    {
        if (player == null) return false;
        if (!Match.Adjacent(r1, c1, r2, c2)) return false;
        Match.SwapTiles(r1, c1, r2, c2);
        if (!Match.MakeMatch())
        {
            Match.SwapTiles(r2, c2, r1, c1); return false;
        }
        player.PlayerScore += Match.Scored();
        return true;
    }

}
