using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3;

public class Player
{
    public int PlayerId { get; set; } = 0;
    public string Nickname { get; set; } = string.Empty;
    public double PlayerScore { get; set; } = 0;
    public List<(int fila, int col)> Selections = new();
    private bool _isReady = false;

    public void AddSelection(int fila, int col)
    {
        Selections.Add((fila, col));
    }

    public List<(int fila, int col)> GetSeleccion()
    {
        return Selections;
    }

    public void ClearSelection()
    {
        Selections.Clear();
    }

    public void AddScore(int amount)
    {
        PlayerScore += amount;
    }

    public bool IsReady => _isReady;
}
