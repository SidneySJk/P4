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

    // Intercambia dos fichas en el tablero
    public void SwapTiles(int r1, int c1, int r2, int c2)
    {
        char tmp = Tiles[r1][c1];
        Tiles[r1][c1] = Tiles[r2][c2];
        Tiles[r2][c2] = tmp;
    }

    // Verifica si dos posiciones son adyacentes
    public static bool Adjacent(int r1, int c1, int r2, int c2)
    {
        return (Math.Abs(r1 - r2) + Math.Abs(c1 - c2) == 1);
    }

    // Marca una ficha como parte de un match
    private void MarkMatch(int r, int c)
    {
        Matched[r, c] = true;
        MatchLine.Add((r, c));

    }

    // Busca y marca todas las coincidencias en el tablero
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

    // Actualiza el tablero con nuevas fichas despues de un match, mueve las de arriba a abajo y genera nueva en puntos vacios
    public void UpdateMatch()
    {
        for (int c = 0; c < cols; c++)
        {
            int reWriteRow = rows - 1;
            for(int r = rows - 1; r >= 0; r--)
            {
                if (!Matched[r, c])
                {
                    Tiles[reWriteRow][c] = Tiles[r][c];
                    reWriteRow--;
                }
            }
            //Random rnd = new Random();
            while (reWriteRow >= 0)
            {
                Tiles[reWriteRow][c] = Board.RandomTile();
                reWriteRow--;
            }
        }
        Matched = new bool[rows, cols];
    }

    // Calcula la puntuacion obtenida en el match actual
    public double Scored()
    {
        return (Math.Pow(MatchLine.Count, 2));
    }

}

