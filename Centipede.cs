using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_csharp_Platformer
{
    // Token: 0x02000002 RID: 2
    internal class Centipede : Enemy
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        public Centipede(Texture2D charText, Rectangle charSource, Rectangle charDisplay, Texture2D hitBoxText, Rectangle hitBoxDisplay, Color charColor, int sourceWidthLimit, Texture2D attackHitBoxText, Rectangle attackHitBoxDisplay, int minPosX, int maxPosX, Texture2D rayCastHitBoxText, Rectangle rayCastHitBoxDisplay, int charLife, int chaseDistance) : base(charText, charSource, charDisplay, hitBoxText, hitBoxDisplay, charColor, sourceWidthLimit, attackHitBoxText, attackHitBoxDisplay, minPosX, maxPosX, rayCastHitBoxText, rayCastHitBoxDisplay, charLife, chaseDistance)
        {
            this.animSpeed = 12;
            this.initialAnimSpeed = this.animSpeed;
            this.charMoveSpeed = 1;
            this.charChaseSpeed = this.charMoveSpeed * 4;
            this.hitBoxPosX = 60;
        }

        // Token: 0x06000002 RID: 2 RVA: 0x000020B0 File Offset: 0x000002B0
        protected override void ChangeAnimationState(int spriteRow)
        {
            if (spriteRow == 2 || spriteRow == 7)
            {
                this.sourceWidthLimit = this.charText.Width - this.charSource.Width;
            }
            else if (spriteRow == 4 || spriteRow == 9)
            {
                this.sourceWidthLimit = this.charText.Width - this.charSource.Width * 5;
            }
            else
            {
                this.sourceWidthLimit = this.charText.Width - this.charSource.Width * 3;
            }
            this.charSource.Y = this.charSource.Height * spriteRow;
        }
    }
}
