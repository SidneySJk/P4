using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3;

public class Board
{
    //Atributos
    public static int LENGTH { get; } = 9;
    public static int WIDTH { get; }  = 7;
    public List<char> Colors { get; } = new List<char>() { 'A', 'N', 'R', 'V', 'Y', 'M' };
    public Tile[,] Tiles { get; } = new Tile[LENGTH, WIDTH];

    //Metodos

    // Contructoye el tablero con fichas aleatorias
    public void BuildBoard()
    {
        Random rnd = new Random();
        for (int i =0; i < LENGTH; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                int ind = rnd.Next(Colors.Count);
                Tiles[i, j] = new Tile(i, j, Colors[ind]);
            }
        }
    }

    // Devuelve la ficha en la posicion dada, o null si no existe
    public Tile GetTile(int row, int col)
    {
        if (row >= LENGTH || col >= WIDTH || row < 0 || col < 0)
        {
            return null;
        }

        return Tiles[row, col];
    }

    // Selecciona o deselecciona una ficha para un jugador, devuelve false si no se puede seleccionar
    public bool Select(int row, int col, int playerId)
    {
        Tile tile = GetTile(row, col);
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
        Tile tile = GetTile(row, col);
        if (tile == null) return;
        Random rnd = new Random();
        char color = Colors[rnd.Next(Colors.Count)];
        tile.ChangeTile(color);
    }

    // Devuelve un color aleatorio de los disponibles
    public char RandomTile()
    {
        //Tile(int row, int col, char color)
        Random rnd = new Random();
        return Colors[rnd.Next(Colors.Count)];
    }

    // Devuelve una representacion simple del tablero con solo los colores de las fichas
    public List<List<char>> SimpleBoard() 
    {

        List<List<char>> board = new List<List<char>>();

        for (int i = 0; i < LENGTH; i++)
        {
            List<char> tiles = new List<char>();
            for (int j = 0; j < WIDTH; j++)
            {
                char color = Tiles[i, j].Color;
                tiles.Add(color);
            }

            board.Add(tiles);
        }

        return board;
    }
}
