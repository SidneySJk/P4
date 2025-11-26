using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3;

public class Tile
{
    private int Row;
    private int Col;
    public char Color;
    public bool IsSelected { get; set; } = false;
    public int? PlayerIdSelection { get; private set; } = null;

    public Tile(int row, int col, char color)
    {
        this.Row = row;
        this.Col = col;
        this.Color = color;
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

    public void ChangeTile(char newColor)
    {
        Color = newColor;
        ReleaseTile();
    }
}
