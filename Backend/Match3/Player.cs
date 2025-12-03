using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3;

public class Player
{
    public string PlayerId { get; set; } = "P-" + Guid.NewGuid().ToString().Substring(0, 8);
    public string Nickname { get; set; } = string.Empty;
    public double PlayerScore { get; private set; } = 0;
    public List<(int fila, int col)> Selections { get; } = new();
    public int totalMatches = 0;
    public bool _isReady = false;

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

    public void changeReady()
    {
        _isReady = !_isReady;
    }
}
