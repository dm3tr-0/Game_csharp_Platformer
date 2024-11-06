using System;

namespace Game_csharp_Platformer
{
    // Token: 0x0200000D RID: 13
    public static class Program
    {
        // Token: 0x06000062 RID: 98 RVA: 0x00006EF0 File Offset: 0x000050F0
        [STAThread]
        private static void Main()
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
}
