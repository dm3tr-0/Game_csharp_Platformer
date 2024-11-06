using System;

namespace Game_csharp_Platformer
{
    // Token: 0x02000006 RID: 6
    internal class GameManager
    {
        // Token: 0x06000029 RID: 41 RVA: 0x00005F81 File Offset: 0x00004181
        public GameManager(GameManager.GameStateEnum gameState, GameManager.GameLevel gameLevel)
        {
            this.gameState = gameState;
            this.gameLevel = gameLevel;
        }

        // Token: 0x04000079 RID: 121
        public GameManager.GameStateEnum gameState;

        // Token: 0x0400007A RID: 122
        public GameManager.GameLevel gameLevel;

        // Token: 0x02000010 RID: 16
        public enum GameStateEnum
        {
            // Token: 0x040000BC RID: 188
            MENU,
            // Token: 0x040000BD RID: 189
            START,
            // Token: 0x040000BE RID: 190
            CONTINUE,
            // Token: 0x040000BF RID: 191
            EXIT
        }

        // Token: 0x02000011 RID: 17
        public enum GameLevel
        {
            // Token: 0x040000C1 RID: 193
            LEVEL1A,
            // Token: 0x040000C2 RID: 194
            LEVEL1B,
            // Token: 0x040000C3 RID: 195
            LEVEL1C,
            // Token: 0x040000C4 RID: 196
            LEVEL2A,
            // Token: 0x040000C5 RID: 197
            LEVEL2B,
            // Token: 0x040000C6 RID: 198
            LEVEL2C,
            // Token: 0x040000C7 RID: 199
            COMPLETE
        }
    }
}
