using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_csharp_Platformer
{
    // Token: 0x02000008 RID: 8
    internal class ListButtons
    {
        // Token: 0x06000030 RID: 48 RVA: 0x00006000 File Offset: 0x00004200
        public ListButtons(Texture2D normalText, Texture2D hoverText, Rectangle buttonRect, Color buttonColor, Vector2 fontPosUnhover, Vector2 fontPosHover, Color fontColor, int i)
        {
            this.normalText = normalText;
            this.hoverText = hoverText;
            this.buttonText = normalText;
            this.buttonRect = buttonRect;
            this.buttonColor = buttonColor;
            this.fontPosUnhover = fontPosUnhover;
            this.fontPosHover = fontPosHover;
            this.fontPos = fontPosUnhover;
            this.fontColor = fontColor;
            this.fontText = this.fontLabels[i];
            this.fontPosX = (int)this.FontPos.X;
            this.buttonRectPosX = this.ButtonRect.X;
            this.initialPos = buttonRect;
        }

        // Token: 0x17000011 RID: 17
        // (get) Token: 0x06000031 RID: 49 RVA: 0x000060D4 File Offset: 0x000042D4
        public Texture2D ButtonText
        {
            get
            {
                return this.buttonText;
            }
        }

        // Token: 0x17000012 RID: 18
        // (get) Token: 0x06000032 RID: 50 RVA: 0x000060DC File Offset: 0x000042DC
        public Rectangle ButtonRect
        {
            get
            {
                return this.buttonRect;
            }
        }

        // Token: 0x17000013 RID: 19
        // (get) Token: 0x06000033 RID: 51 RVA: 0x000060E4 File Offset: 0x000042E4
        public Color ButtonColor
        {
            get
            {
                return this.buttonColor;
            }
        }

        // Token: 0x17000014 RID: 20
        // (get) Token: 0x06000034 RID: 52 RVA: 0x000060EC File Offset: 0x000042EC
        public string FontText
        {
            get
            {
                return this.fontText;
            }
        }

        // Token: 0x17000015 RID: 21
        // (get) Token: 0x06000035 RID: 53 RVA: 0x000060F4 File Offset: 0x000042F4
        public Vector2 FontPos
        {
            get
            {
                return this.fontPos;
            }
        }

        // Token: 0x17000016 RID: 22
        // (get) Token: 0x06000036 RID: 54 RVA: 0x000060FC File Offset: 0x000042FC
        public Color FontColor
        {
            get
            {
                return this.fontColor;
            }
        }

        // Token: 0x17000017 RID: 23
        // (get) Token: 0x06000037 RID: 55 RVA: 0x00006104 File Offset: 0x00004304
        public Rectangle InitialPos
        {
            get
            {
                return this.initialPos;
            }
        }

        // Token: 0x06000038 RID: 56 RVA: 0x0000610C File Offset: 0x0000430C
        public void HoverButton()
        {
            this.buttonText = this.hoverText;
            this.fontPos.Y = this.fontPosHover.Y;
        }

        // Token: 0x06000039 RID: 57 RVA: 0x00006130 File Offset: 0x00004330
        public void NormalButton()
        {
            this.buttonText = this.normalText;
            this.fontPos.Y = this.fontPosUnhover.Y;
        }

        // Token: 0x0600003A RID: 58 RVA: 0x00006154 File Offset: 0x00004354
        public void Move(float cameraPosX)
        {
            this.fontPos.X = (float)(-(float)((int)cameraPosX) + this.fontPosX);
            this.buttonRect.X = -(int)cameraPosX + this.buttonRectPosX;
        }

        // Token: 0x0400007F RID: 127
        private Texture2D normalText;

        // Token: 0x04000080 RID: 128
        private Texture2D hoverText;

        // Token: 0x04000081 RID: 129
        private Texture2D buttonText;

        // Token: 0x04000082 RID: 130
        private Rectangle buttonRect;

        // Token: 0x04000083 RID: 131
        private Rectangle initialPos;

        // Token: 0x04000084 RID: 132
        private Color buttonColor;

        // Token: 0x04000085 RID: 133
        private Vector2 fontPosUnhover;

        // Token: 0x04000086 RID: 134
        private Vector2 fontPosHover;

        // Token: 0x04000087 RID: 135
        private Vector2 fontPos;

        // Token: 0x04000088 RID: 136
        private Color fontColor;

        // Token: 0x04000089 RID: 137
        private string[] fontLabels = new string[]
        {
            "       START",
            "   CONTINUE",
            "   CONTROLS",
            "        EXIT",
            "        BACK",
            " MAIN MENU",
            "         QUIT"
        };

        // Token: 0x0400008A RID: 138
        private string fontText;

        // Token: 0x0400008B RID: 139
        private int fontPosX;

        // Token: 0x0400008C RID: 140
        private int buttonRectPosX;
    }
}
