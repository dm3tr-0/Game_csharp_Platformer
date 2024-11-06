using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_csharp_Platformer
{
    // Token: 0x02000005 RID: 5
    internal class GameHUDwText : GameHUD
    {
        // Token: 0x06000025 RID: 37 RVA: 0x00005F25 File Offset: 0x00004125
        public GameHUDwText(Texture2D uiText, Rectangle uiDisplay, Color uiColor, Vector2 fontPos, Color fontColor) : base(uiText, uiDisplay, uiColor)
        {
            this.fontPos = fontPos;
            this.fontColor = fontColor;
            this.fontPosX = (int)this.FontPos.X;
        }

        // Token: 0x1700000C RID: 12
        // (get) Token: 0x06000026 RID: 38 RVA: 0x00005F52 File Offset: 0x00004152
        public Vector2 FontPos
        {
            get
            {
                return this.fontPos;
            }
        }

        // Token: 0x1700000D RID: 13
        // (get) Token: 0x06000027 RID: 39 RVA: 0x00005F5A File Offset: 0x0000415A
        public Color FontColor
        {
            get
            {
                return this.fontColor;
            }
        }

        // Token: 0x06000028 RID: 40 RVA: 0x00005F62 File Offset: 0x00004162
        public override void Move(float cameraPosX)
        {
            base.Move(cameraPosX);
            this.fontPos.X = (float)(-(float)((int)cameraPosX) + this.fontPosX);
        }

        // Token: 0x04000076 RID: 118
        private Vector2 fontPos;

        // Token: 0x04000077 RID: 119
        private Color fontColor;

        // Token: 0x04000078 RID: 120
        private int fontPosX;
    }
}
