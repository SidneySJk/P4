using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3;

public class Match
{
    private static Board Board = new Board();
    private List<List<char>> Tiles = Board.SimpleBoard();
    private static int rows = Board.LENGTH;
    private static int cols = Board.WIDTH;
    private bool matching = false;
    private bool[,] Matched = new bool[rows, cols];
    public List<(int r, int c)> MatchLine { get; } = new List<(int r, int c)>();
    

    public void SwapTiles(int r1, int c1, int r2, int c2)
    {
        char tmp = Tiles[r1][c1];
        Tiles[r1][c1] = Tiles[r2][c2];
        Tiles[r2][c2] = tmp;
    }

    public static bool Adjacent(int r1, int c1, int r2, int c2)
    {
        return (Math.Abs(r1 - r2) + Math.Abs(c1 - c2) == 1);
    }

    private void MarkMatch(int r, int c)
    {
        Matched[r, c] = true;
        MatchLine.Add((r, c));

    }
    public bool MakeMatch()
    {
        MatchLine.Clear();
        
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols - 2; c++)
            {
                char color = Tiles[r][c];
                if (Adjacent(r, c, r, c + 1) && Adjacent(r, c, r, c + 2))//(c == Tiles[i][j+1] & c == Tiles[i][j+2])
                {
                    if (Tiles[r][c + 1] == color && Tiles[r][c + 2] == color)
                    {
                        MarkMatch(r, c);
                        MarkMatch(r, c + 1);
                        MarkMatch(r, c + 2);
                        int rest = c + 3;
                        while (rest < cols && Adjacent(r, c, r, rest) && Tiles[r][rest] == color)
                        {
                            MarkMatch(r, rest);
                            rest++;
                        }
                        matching = true;
                    }
                }
            }
        }
        for (int c = 0; c < cols; c++)
        {
            for (int r = 0; r < rows - 2; r++)
            {
                char color = Tiles[r][c];
                if (Adjacent(r, c, r + 1, c) && Adjacent(r, c, r + 2, c))//(c == Tiles[i][j+1] & c == Tiles[i][j+2])
                {
                    if (Tiles[r + 1][c] == color && Tiles[r + 2][c] == color)
                    {
                        MarkMatch(r, c);
                        MarkMatch(r + 1, c);
                        MarkMatch(r + 2, c);
                        int rest = c + 3;
                        while (rest < rows && Adjacent(r, c, rest, c) && Tiles[rest][c] == color)
                        {
                            MarkMatch(rest, c);
                            rest++;
                        }
                        matching = true;
                    }
                }
            }
        }
        return matching;
    }

    public double Scored()
    {
        return (Math.Pow(MatchLine.Count, 3));
    }

}
