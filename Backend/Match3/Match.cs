using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3;

public class Match
{
    private readonly Board board;
    private readonly Tile[,] Tiles;
    private readonly int rows;
    private readonly int cols;
    public bool[,] Matched; //{ get; private set; } = new bool[Board.LENGTH, Board.WIDTH];
    private bool matching = false;
    public int MAX_MATCHES { get; } = (int)(Math.Pow(6, Board.LENGTH * Board.WIDTH));
    public List<(int Row, int Col)> MatchLine { get; private set; } = new List<(int, int)>();
    public Match(Board _board) 
    {
        board = _board;
        Tiles = board.Tiles;
        rows = Board.LENGTH;
        cols = Board.WIDTH;
        Matched = new bool[rows, cols];
    }

    /*
    public bool MakeMatch()
    {
        MatchLine.Clear();
        bool matching = false;
        matching |= Vertical(board);
        matching |= Horizontal(board);
        return matching;
    }
    */

    private void MarkMatch(int r, int c)
    {
        Matched[r, c] = true;
        MatchLine.Add((r, c));

    }

    private bool Horizontal()
    {
        matching = false;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols - 2; c++)
            {
                char color = Tiles[r, c].Color;
                if (Tiles[r, c + 1].Color == color && Tiles[r, c + 2].Color == color)
                {
                    MarkMatch(r, c);
                    MarkMatch(r, c + 1);
                    MarkMatch(r, c + 2);
                    int rest = c + 3;
                    while (rest < cols && Tiles[r, rest].Color == color)
                    {
                        MarkMatch(r, rest);
                        rest++;
                    }
                    matching = true;
                }
            }
        }
        return matching;
    }

    private bool Vertical()
    {
        bool match = false;
        for (int c = 0; c < cols; c++)
        {
            for (int r = 0; r < rows - 2; r++)
            {
                char color = Tiles[r, c].Color;
                if (Tiles[r+1, c].Color == color && Tiles[r+2, c].Color == color)
                {
                    MarkMatch(r, c);
                    MarkMatch(r + 1, c);
                    MarkMatch(r + 2, c);
                    int rest = r + 3;
                    while (rest < rows && Tiles[rest, c].Color == color)
                    {
                        MarkMatch(rest, c);
                        rest++;
                    }
                    match = true;
                }
            }
            
        }
        return match;
    }

    private bool Diagonal()
    {
        matching = false;
        for (int r = 0; r < rows - 2; r++)
        {
            for (int c = 0; c < cols - 2; c++)
            {
                char color = Tiles[r, c].Color;
                if (Tiles[r + 1, c + 1].Color == color && Tiles[r + 2, c + 2].Color == color)
                {
                    MarkMatch(r, c);
                    MarkMatch(r + 1, c + 1);
                    MarkMatch(r + 2, c + 2);
                    int rest = 3;
                    while (r+rest < rows && c+rest < cols && Tiles[r+rest, c+rest].Color == color)
                    {
                        MarkMatch(r+rest, c+rest);
                        rest++;
                    }
                    matching = true;
                }
            }
        }
        for (int r = 0; r < rows - 2; r++)
        {
            for (int c = 2; c < cols; c++)
            {
                char color = Tiles[r, c].Color;
                if (Tiles[r + 1, c - 1].Color == color && Tiles[r + 2, c - 2].Color == color)
                {
                    MarkMatch(r, c);
                    MarkMatch(r + 1, c - 1);
                    MarkMatch(r + 2, c - 2);
                    int rest = 3;
                    while (r + rest < 0 && c - rest < cols && Tiles[r + rest, c - rest].Color == color)
                    {
                        MarkMatch(r + rest, c - rest);
                        rest++;
                    }
                    matching = true;

                }
            }
        }
        return matching;
    }

    // Actualiza el tablero con nuevas fichas despues de un match, mueve las de arriba a abajo y genera nueva en puntos vacios
    public void UpdateMatch()
    {
        for (int c = 0; c < cols; c++)
        {
            int reWriteRow = rows - 1;
            for (int r = rows - 1; r >= 0; r--)
            {
                if (!Matched[r, c])
                {
                    Tiles[reWriteRow, c].ChangeTile(Tiles[r, c].Color);// = Tiles[r,c];
                    reWriteRow--;
                }
            }
            //Random rnd = new Random();
            while (reWriteRow >= 0)
            {
                Tiles[reWriteRow, c].RandomizeColor(board);// = Board.RandomTile();
                reWriteRow--;
            }
        }
        Matched = new bool[rows, cols];
    }

    // Busca y marca todas las coincidencias en el tablero
    public bool MakeMatch()
    {
        matching = false;
        MatchLine.Clear();
        Matched = new bool[rows, cols];
        bool v = Vertical();
        bool h = Horizontal();
        bool d = Diagonal();
        matching = v || h || d;
        return matching;
    }
    public void ResolveAllMatches()
    {
        while (MakeMatch())
        {
            UpdateMatch();
        }
    }

    // Calcula la puntuacion obtenida en el match actual
    public double Scored()
    {
        return (Math.Pow(MatchLine.Count, 2));
    }

}
