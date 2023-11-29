using Stride.Engine;

namespace RpgGame
{
    class RpgGameApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
