using System;

namespace Game0
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new TheDesert())
                game.Run();
        }
    }
}
