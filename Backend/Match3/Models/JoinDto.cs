namespace Match3.Models
{
    public class GameStateDto
    {
        public string GameId { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public object Board { get; set; } = new object();
        public IEnumerable<object> Players { get; set; } = Enumerable.Empty<object>();
    }
}
