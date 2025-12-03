namespace Match3.Services
{
    public class CreateBoards
    {
        private readonly Random random = new();
        public CreateBoards(Random? _rnd = null)
        {
            random = _rnd ?? new Random(); 
        }

        public Board CreateBoard()
        {
            return new Board(random);
        }
    }
}
