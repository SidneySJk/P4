namespace Match3.Models
{
    public class SwapDto
    {
        public string PlayerId { get; set; } = string.Empty;
        public int Row1 { get; set; }
        public int Col1 { get; set; }
        public int Row2 { get; set; }
        public int Col2 { get; set; }
    }
}
