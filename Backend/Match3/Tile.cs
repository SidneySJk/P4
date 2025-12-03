using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3;

public class Tile
{
    public int Row { get; private set; }
    public int Col { get; private set; }
    public char Color { get; private set; }
    public bool IsSelected { get; private set; } = false;
    public int? PlayerIdSelection { get; private set; } = null;
    public int GetRow() => Row;
    public int GetCol() => Col;
    public char GetColor() => Color;
    public Tile(int row, int col, char color)
    {
        Row = row;
        Col = col;
        Color = color;
    }

    public void SetPosition(int r, int c)
    {
        Row = r;
        Col = c;
    }

    public bool SelectTile(int playerId)
    {
        if (IsSelected) 
        {
            return false;
        }
        PlayerIdSelection = playerId;
        IsSelected = true;
        return true;
    }

    public void ReleaseTile() 
    {
        IsSelected = false;
        PlayerIdSelection = null;
    }

    
    public void RandomizeColor(Board board)
    {
        Color = board.RandomTile();
        ReleaseTile();
    }

    public void ChangeTile(char newColor)
    {
        Color = newColor;
        ReleaseTile();
    }
    
}
