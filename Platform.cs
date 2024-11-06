using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_csharp_Platformer
{
    // Token: 0x0200000A RID: 10
    internal class Platform
    {
        // Token: 0x0600003E RID: 62 RVA: 0x00006504 File Offset: 0x00004704
        public Platform(Texture2D platText, Rectangle platSource, Rectangle platDisplay, Color platColor, Texture2D hitBoxText, Rectangle hitBoxDisplay)
        {
            this.platText = platText;
            this.platSource = platSource;
            this.platDisplay = platDisplay;
            this.platColor = platColor;
            this.hitBoxText = hitBoxText;
            this.hitBoxDisplay = hitBoxDisplay;
        }

        // Token: 0x17000018 RID: 24
        // (get) Token: 0x0600003F RID: 63 RVA: 0x00006539 File Offset: 0x00004739
        // (set) Token: 0x06000040 RID: 64 RVA: 0x00006541 File Offset: 0x00004741
        public Texture2D PlatText
        {
            get
            {
                return this.platText;
            }
            set
            {
                this.platText = value;
            }
        }

        // Token: 0x17000019 RID: 25
        // (get) Token: 0x06000041 RID: 65 RVA: 0x0000654A File Offset: 0x0000474A
        public Rectangle PlatDisplay
        {
            get
            {
                return this.platDisplay;
            }
        }

        // Token: 0x1700001A RID: 26
        // (get) Token: 0x06000042 RID: 66 RVA: 0x00006552 File Offset: 0x00004752
        public Rectangle PlatSource
        {
            get
            {
                return this.platSource;
            }
        }

        // Token: 0x1700001B RID: 27
        // (get) Token: 0x06000043 RID: 67 RVA: 0x0000655A File Offset: 0x0000475A
        public Color PlatColor
        {
            get
            {
                return this.platColor;
            }
        }

        // Token: 0x1700001C RID: 28
        // (get) Token: 0x06000044 RID: 68 RVA: 0x00006562 File Offset: 0x00004762
        public Texture2D HitBoxText
        {
            get
            {
                return this.hitBoxText;
            }
        }

        // Token: 0x1700001D RID: 29
        // (get) Token: 0x06000045 RID: 69 RVA: 0x0000656A File Offset: 0x0000476A
        public Rectangle HitBoxDisplay
        {
            get
            {
                return this.hitBoxDisplay;
            }
        }

        // Token: 0x04000094 RID: 148
        private Texture2D platText;

        // Token: 0x04000095 RID: 149
        private Texture2D hitBoxText;

        // Token: 0x04000096 RID: 150
        private Rectangle platSource;

        // Token: 0x04000097 RID: 151
        private Rectangle platDisplay;

        // Token: 0x04000098 RID: 152
        private Rectangle hitBoxDisplay;

        // Token: 0x04000099 RID: 153
        private Color platColor;
    }
}
