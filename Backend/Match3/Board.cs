using Match3;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3;

public class Board
{
    public const int LENGTH = 9;
    public const int WIDTH = 7;
    public List<char> Colors { get; } = new() { 'A', 'N', 'R', 'V', 'Y', 'M' };
    private readonly Random _rnd;
    public Tile[,] Tiles { get; } //set; } = new Tile[LENGTH, WIDTH];

    public Board(Random? rnd = null)
    {
        _rnd = rnd ?? new Random();
        Tiles = new Tile[LENGTH, WIDTH];
        BuildBoard();
    }

    // Construye el tablero con fichas aleatorias
    public void BuildBoard()
    {
        for (int i = 0; i < LENGTH; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                //int ind = _rnd.Next(Colors.Count);
                Tiles[i, j] = new Tile(i, j, RandomTile());
            }
        }
    }

    // Devuelve la ficha en la posición dada, null si no existe
    public Tile? GetTile(int row, int col)
    {
        if (row >= LENGTH || col >= WIDTH || row < 0 || col < 0)
        {
            return null;
        }

        return Tiles[row, col];
    }

    // Selecciona o deselecciona una ficha para un jugador, false si no se puede seleccionar
    public bool Select(int row, int col, int playerId)
    {
        Tile? tile = GetTile(row, col);
        if (tile == null) return false;
        if ((tile.IsSelected) & tile.PlayerIdSelection == playerId)
        {
            tile.ReleaseTile();
            return true;
        }
        if ((tile.IsSelected) & tile.PlayerIdSelection != playerId) return false;
        return tile.SelectTile(playerId);
    }


    // Cambia la ficha en la posicion dada por una nueva ficha aleatoria
    public void CleanTile(int row, int col)
    {
        Tile? tile = GetTile(row, col);
        if (tile == null) return;
        char color = RandomTile();//Colors[rnd.Next(Colors.Count)];
        tile.ChangeTile(color);
    }

    // Verifica si dos posiciones son adyacentes
    public static bool Adjacent(int r1, int c1, int r2, int c2)
    {
        return (Math.Abs(r1 - r2) + Math.Abs(c1 - c2) == 1);
    }

    // Intercambiar dos fichas
    public void SwapTiles(int r1, int c1, int r2, int c2)
    {
        /*
        Tile? t1 = GetTile(r1, c1);
        Tile? t2 = GetTile(r2, c2);
        if (t1 == null || t2 == null) return false;
        
        char temp = t1.Color;
        t1.Color = t2.Color;
        t2.Color = temp;
        */
        var t1 = Tiles[r1, c1];
        var t2 = Tiles[r2, c2];
        Tiles[r1, c1] = t2;
        Tiles[r2, c2] = t1;
        t1.SetPosition(r2, c2);
        t2.SetPosition(r1, c1);
        t1.ReleaseTile();
        t2.ReleaseTile();

        //return true;
    }

    // Devuelve un color aleatorio
    public char RandomTile()
    {
        //Tile(int row, int col, char color)
        return Colors[_rnd.Next(Colors.Count)];
    }

    // Devuelve una representacion simple del tablero con solo los colores de las fichas
    public List<List<char>> SimpleBoard()
    {

        List<List<char>> board = new List<List<char>>();

        for (int i = 0; i < LENGTH; i++)
        {
            List<char> tiles = new();
            for (int j = 0; j < WIDTH; j++)
            {
                tiles.Add(Tiles[i, j].Color);
            }

            board.Add(tiles);
        }

        return board;
    }
}

