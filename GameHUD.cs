using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_csharp_Platformer
{
    // Token: 0x02000007 RID: 7
    internal class GameHUD
    {
        // Token: 0x0600002A RID: 42 RVA: 0x00005F97 File Offset: 0x00004197
        public GameHUD(Texture2D uiText, Rectangle uiDisplay, Color uiColor)
        {
            this.uiText = uiText;
            this.uiDisplay = uiDisplay;
            this.uiColor = uiColor;
            this.posX = this.UIDisplay.X;
        }

        // Token: 0x1700000E RID: 14
        // (get) Token: 0x0600002B RID: 43 RVA: 0x00005FC5 File Offset: 0x000041C5
        // (set) Token: 0x0600002C RID: 44 RVA: 0x00005FCD File Offset: 0x000041CD
        public Texture2D UIText
        {
            get
            {
                return this.uiText;
            }
            set
            {
                this.uiText = value;
            }
        }

        // Token: 0x1700000F RID: 15
        // (get) Token: 0x0600002D RID: 45 RVA: 0x00005FD6 File Offset: 0x000041D6
        public Rectangle UIDisplay
        {
            get
            {
                return this.uiDisplay;
            }
        }

        // Token: 0x17000010 RID: 16
        // (get) Token: 0x0600002E RID: 46 RVA: 0x00005FDE File Offset: 0x000041DE
        public Color UIColor
        {
            get
            {
                return this.uiColor;
            }
        }

        // Token: 0x0600002F RID: 47 RVA: 0x00005FE6 File Offset: 0x000041E6
        public virtual void Move(float cameraPosX)
        {
            this.uiDisplay.X = -(int)cameraPosX + this.posX;
        }

        // Token: 0x0400007B RID: 123
        private Texture2D uiText;

        // Token: 0x0400007C RID: 124
        private Rectangle uiDisplay;

        // Token: 0x0400007D RID: 125
        private Color uiColor;

        // Token: 0x0400007E RID: 126
        private int posX;
    }
}
