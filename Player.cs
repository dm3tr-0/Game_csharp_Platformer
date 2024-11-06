using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_csharp_Platformer
{
    // Token: 0x0200000B RID: 11
    internal class Player
    {
        // Token: 0x06000046 RID: 70 RVA: 0x00006574 File Offset: 0x00004774
        public Player(Texture2D playerText, Rectangle playerSource, Rectangle playerDisplay, Texture2D playerHitBoxText, Rectangle playerHitBoxDisplay, Color playerColor, int sourceWidthLimit, Texture2D attackHitBoxText, Rectangle attackHitBoxDisplay)
        {
            this.animState = Player.AnimState.Idle;
            this.playerText = playerText;
            this.playerSource = playerSource;
            this.playerDisplay = playerDisplay;
            this.playerHitBoxText = playerHitBoxText;
            this.playerHitBoxDisplay = playerHitBoxDisplay;
            this.playerColor = playerColor;
            this.sourceWidthLimit = sourceWidthLimit;
            this.attackHitBoxText = attackHitBoxText;
            this.attackHitBoxDisplay = attackHitBoxDisplay;
            this.spriteRow = 0;
            this.checkPoint = new Point(playerDisplay.X, playerDisplay.Y - 50);
            this.playerMoveSpeed = 2;
            this.playerSprintSpeed = 2;
            this.delay = 0;
            this.animSpeed = 8;
            this.facingLeftRight = true;
            this.playerLife = 3;
            this.gravity = 18;
        }

        // Token: 0x1700001E RID: 30
        // (get) Token: 0x06000047 RID: 71 RVA: 0x00006626 File Offset: 0x00004826
        public Texture2D PlayerText
        {
            get
            {
                return this.playerText;
            }
        }

        // Token: 0x1700001F RID: 31
        // (get) Token: 0x06000048 RID: 72 RVA: 0x0000662E File Offset: 0x0000482E
        public Rectangle PlayerSource
        {
            get
            {
                return this.playerSource;
            }
        }

        // Token: 0x17000020 RID: 32
        // (get) Token: 0x06000049 RID: 73 RVA: 0x00006636 File Offset: 0x00004836
        // (set) Token: 0x0600004A RID: 74 RVA: 0x0000663E File Offset: 0x0000483E
        public Rectangle PlayerDisplay
        {
            get
            {
                return this.playerDisplay;
            }
            set
            {
                this.playerDisplay = value;
            }
        }

        // Token: 0x17000021 RID: 33
        // (get) Token: 0x0600004B RID: 75 RVA: 0x00006647 File Offset: 0x00004847
        public Texture2D PlayerHitBoxText
        {
            get
            {
                return this.playerHitBoxText;
            }
        }

        // Token: 0x17000022 RID: 34
        // (get) Token: 0x0600004C RID: 76 RVA: 0x0000664F File Offset: 0x0000484F
        public Rectangle PlayerHitBoxDisplay
        {
            get
            {
                return this.playerHitBoxDisplay;
            }
        }

        // Token: 0x17000023 RID: 35
        // (get) Token: 0x0600004D RID: 77 RVA: 0x00006657 File Offset: 0x00004857
        public Color PlayerColor
        {
            get
            {
                return this.playerColor;
            }
        }

        // Token: 0x17000024 RID: 36
        // (get) Token: 0x0600004E RID: 78 RVA: 0x0000665F File Offset: 0x0000485F
        public Texture2D AttackHitBoxText
        {
            get
            {
                return this.attackHitBoxText;
            }
        }

        // Token: 0x17000025 RID: 37
        // (get) Token: 0x0600004F RID: 79 RVA: 0x00006667 File Offset: 0x00004867
        public Rectangle AttackHitBoxDisplay
        {
            get
            {
                return this.attackHitBoxDisplay;
            }
        }

        // Token: 0x17000026 RID: 38
        // (get) Token: 0x06000050 RID: 80 RVA: 0x0000666F File Offset: 0x0000486F
        // (set) Token: 0x06000051 RID: 81 RVA: 0x00006677 File Offset: 0x00004877
        public Point CheckPoint
        {
            get
            {
                return this.checkPoint;
            }
            set
            {
                this.checkPoint = value;
            }
        }

        // Token: 0x17000027 RID: 39
        // (get) Token: 0x06000052 RID: 82 RVA: 0x00006680 File Offset: 0x00004880
        // (set) Token: 0x06000053 RID: 83 RVA: 0x00006688 File Offset: 0x00004888
        public int PlayerLife
        {
            get
            {
                return this.playerLife;
            }
            set
            {
                this.playerLife = value;
            }
        }

        // Token: 0x06000054 RID: 84 RVA: 0x00006694 File Offset: 0x00004894
        public void PlayerUpdate(List<Enemy> enemies, SoundEffectInstance sfxWalk, SoundEffectInstance sfxSlash, SoundEffectInstance sfxRun, SoundEffectInstance sfxWhoosh, SoundEffectInstance sfxJump, SoundEffectInstance sfxHit, SoundEffectInstance sfxDeath)
        {
            if (this.animState != Player.AnimState.Death)
            {
                if (this.animState == Player.AnimState.Moving)
                {
                    this.PlayerMovement(sfxWalk, sfxRun);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && this.animState != Player.AnimState.Attacking && this.jumpState == Player.JumpState.Ground)
                {
                    sfxWhoosh.Play();
                    this.PlayerAttack();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && this.jumpState == Player.JumpState.Ground)
                {
                    sfxJump.Play();
                    this.jumpState = Player.JumpState.Jumped;
                    this.playerSource.X = 0;
                    this.force = this.gravity;
                    this.animState = Player.AnimState.Moving;
                }
                if (this.animState == Player.AnimState.Hit && !this.isHit)
                {
                    this.isHit = true;
                    this.PlayerHit(sfxHit, sfxDeath);
                }
                else if (this.PlayerDisplay.Top >= 544)
                {
                    this.PlayerHit(sfxHit, sfxDeath);
                    if (this.animState != Player.AnimState.Death)
                    {
                        this.playerDisplay.Location = this.checkPoint;
                        this.animState = Player.AnimState.Idle;
                    }
                }
                if (this.animState == Player.AnimState.Idle)
                {
                    sfxWalk.Stop();
                    sfxRun.Stop();
                    this.isHit = false;
                    this.animSpeed = 8;
                    if (this.facingLeftRight)
                    {
                        this.spriteRow = 0;
                    }
                    else
                    {
                        this.spriteRow = 7;
                    }
                    if (this.playerLife <= 0)
                    {
                        this.animState = Player.AnimState.Death;
                    }
                }
                foreach (Enemy enemy in enemies)
                {
                    if (this.attackHitBoxDisplay.Intersects(enemy.HitBoxDisplay) && this.animState == Player.AnimState.Attacking)
                    {
                        sfxSlash.Play();
                        if (enemy.animState != Enemy.AnimState.Death)
                        {
                            enemy.animState = Enemy.AnimState.Hit;
                            break;
                        }
                        break;
                    }
                }
                this.PlayerKeyState();
            }
            else
            {
                if (sfxDeath.State == SoundState.Stopped)
                {
                    Game1.hasPaused = true;
                }
                this.PlayerDeathStateAnim();
            }
            this.PlayerJumpState();
            this.AnimationDelay(this.animSpeed);
            this.ChangeAnimationState(this.spriteRow);
            this.playerHitBoxDisplay.Location = new Point(this.playerDisplay.X + 50, this.playerDisplay.Y + this.playerDisplay.Height / 2);
        }

        // Token: 0x06000055 RID: 85 RVA: 0x000068C8 File Offset: 0x00004AC8
        private void ChangeAnimationState(int spriteRow)
        {
            if (spriteRow == 1 || spriteRow == 8)
            {
                this.sourceWidthLimit = this.playerText.Width - this.playerSource.Width * 3;
            }
            else if (spriteRow == 3 || spriteRow == 10)
            {
                this.sourceWidthLimit = this.playerText.Width - this.playerSource.Width * 7;
            }
            else if (spriteRow == 5 || spriteRow == 12)
            {
                this.sourceWidthLimit = this.playerText.Width - this.playerSource.Width * 6;
            }
            else if (spriteRow == 6 || spriteRow == 13)
            {
                this.sourceWidthLimit = this.playerText.Width - this.playerSource.Width * 4;
            }
            else
            {
                this.sourceWidthLimit = this.playerText.Width - this.playerSource.Width;
            }
            this.playerSource.Y = this.playerSource.Height * spriteRow;
        }

        // Token: 0x06000056 RID: 86 RVA: 0x000069B4 File Offset: 0x00004BB4
        private void AnimationDelay(int animSpeed)
        {
            if (this.delay >= animSpeed)
            {
                this.PlayerAnimation();
                this.delay = 0;
            }
            this.delay++;
        }

        // Token: 0x06000057 RID: 87 RVA: 0x000069DC File Offset: 0x00004BDC
        private void PlayerAnimation()
        {
            if (this.jumpState != Player.JumpState.Ground)
            {
                this.playerSource.X = 0;
                return;
            }
            if (this.playerSource.X < this.sourceWidthLimit)
            {
                this.playerSource.X = this.playerSource.X + this.playerSource.Width;
                return;
            }
            if (this.animState != Player.AnimState.Death)
            {
                if (this.animState == Player.AnimState.Attacking || this.animState == Player.AnimState.Hit)
                {
                    this.animState = Player.AnimState.Idle;
                }
                this.playerSource.X = 0;
            }
        }

        // Token: 0x06000058 RID: 88 RVA: 0x00006A5C File Offset: 0x00004C5C
        private void PlayerMovement(SoundEffectInstance sfxWalk, SoundEffectInstance sfxRun)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && this.playerDisplay.X >= -30)
            {
                sfxWalk.Play();
                this.playerDisplay.X = this.playerDisplay.X - this.playerMoveSpeed;
                this.spriteRow = 8;
                this.facingLeftRight = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && this.playerDisplay.X <= 2200)
            {
                sfxWalk.Play();
                this.playerDisplay.X = this.playerDisplay.X + this.playerMoveSpeed;
                this.spriteRow = 1;
                this.facingLeftRight = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && this.animState == Player.AnimState.Moving)
            {
                sfxWalk.Stop();
                sfxRun.Play();
                if (this.facingLeftRight && this.playerDisplay.X <= 2200)
                {
                    this.playerDisplay.X = this.playerDisplay.X + this.playerMoveSpeed * this.playerSprintSpeed;
                    this.spriteRow = 2;
                    return;
                }
                if (!this.facingLeftRight && this.playerDisplay.X >= -30)
                {
                    this.playerDisplay.X = this.playerDisplay.X - this.playerMoveSpeed * this.playerSprintSpeed;
                    this.spriteRow = 9;
                }
            }
        }

        // Token: 0x06000059 RID: 89 RVA: 0x00006BA8 File Offset: 0x00004DA8
        private void PlayerAttack()
        {
            this.animSpeed = 3;
            this.playerSource.X = 0;
            if (this.facingLeftRight)
            {
                this.attackHitBoxDisplay.Location = new Point(this.playerDisplay.X + 90, this.playerDisplay.Y + this.playerDisplay.Height / 2);
                this.spriteRow = 4;
            }
            else
            {
                this.attackHitBoxDisplay.Location = new Point(this.playerDisplay.X + 5, this.playerDisplay.Y + this.playerDisplay.Height / 2);
                this.spriteRow = 11;
            }
            this.animState = Player.AnimState.Attacking;
        }

        // Token: 0x0600005A RID: 90 RVA: 0x00006C58 File Offset: 0x00004E58
        private void PlayerJumpState()
        {
            if (this.jumpState == Player.JumpState.Jumped)
            {
                if (this.animState != Player.AnimState.Hit)
                {
                    if (this.facingLeftRight)
                    {
                        this.spriteRow = 3;
                    }
                    else
                    {
                        this.spriteRow = 10;
                    }
                }
                this.playerDisplay.Y = this.playerDisplay.Y - this.force;
                this.force--;
                if (this.force <= 0)
                {
                    this.jumpState = Player.JumpState.Falling;
                }
            }
            if (this.jumpState == Player.JumpState.Falling)
            {
                this.PlayerGravity();
            }
        }

        // Token: 0x0600005B RID: 91 RVA: 0x00006CD1 File Offset: 0x00004ED1
        private void PlayerGravity()
        {
            this.playerDisplay.Y = this.playerDisplay.Y - this.force;
            this.force--;
        }

        // Token: 0x0600005C RID: 92 RVA: 0x00006CF8 File Offset: 0x00004EF8
        private void PlayerKeyState()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
            if (pressedKeys.Length != 0)
            {
                for (int i = 0; i < pressedKeys.Length; i++)
                {
                    if ((pressedKeys[i] == Keys.Left || pressedKeys[i] == Keys.Right) && this.animState == Player.AnimState.Idle)
                    {
                        this.animSpeed = 8;
                        this.animState = Player.AnimState.Moving;
                        this.playerSource.X = 0;
                    }
                    if (pressedKeys.Length == 1 && pressedKeys[i] == Keys.LeftShift && this.animState != Player.AnimState.Attacking)
                    {
                        this.animState = Player.AnimState.Idle;
                    }
                }
                return;
            }
            if (pressedKeys.Length == 0 && this.animState != Player.AnimState.Attacking && this.jumpState == Player.JumpState.Ground && this.animState != Player.AnimState.Hit && this.animState != Player.AnimState.Death)
            {
                this.animState = Player.AnimState.Idle;
            }
        }

        // Token: 0x0600005D RID: 93 RVA: 0x00006DA4 File Offset: 0x00004FA4
        private void PlayerHit(SoundEffectInstance sfxHit, SoundEffectInstance sfxDeath)
        {
            sfxHit.Play();
            if (this.animState != Player.AnimState.Death && this.playerLife > 0)
            {
                this.animSpeed = 5;
                this.playerSource.X = 0;
                if (this.facingLeftRight)
                {
                    this.playerDisplay.X = this.playerDisplay.X - 40;
                    this.spriteRow = 5;
                }
                else
                {
                    this.spriteRow = 12;
                    this.playerDisplay.X = this.playerDisplay.X + 40;
                }
                this.animState = Player.AnimState.Hit;
                this.playerLife--;
                if (this.playerLife <= 0)
                {
                    this.PlayerDeath(sfxDeath);
                }
            }
        }

        // Token: 0x0600005E RID: 94 RVA: 0x00006E3C File Offset: 0x0000503C
        private void PlayerDeath(SoundEffectInstance sfxDeath)
        {
            sfxDeath.Play();
            this.animState = Player.AnimState.Death;
            this.animSpeed = 12;
            this.playerSource.X = 0;
            this.PlayerDeathStateAnim();
        }

        // Token: 0x0600005F RID: 95 RVA: 0x00006E65 File Offset: 0x00005065
        private void PlayerDeathStateAnim()
        {
            if (this.facingLeftRight)
            {
                this.spriteRow = 6;
                return;
            }
            this.spriteRow = 13;
        }

        // Token: 0x06000060 RID: 96 RVA: 0x00006E80 File Offset: 0x00005080
        public void PlayerDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.PlayerText, this.PlayerDisplay, new Rectangle?(this.PlayerSource), this.PlayerColor);
            spriteBatch.Draw(this.PlayerHitBoxText, this.PlayerHitBoxDisplay, Color.Transparent);
            if (this.animState == Player.AnimState.Attacking)
            {
                spriteBatch.Draw(this.AttackHitBoxText, this.AttackHitBoxDisplay, Color.Transparent);
            }
        }

        // Token: 0x0400009A RID: 154
        public Player.AnimState animState;

        // Token: 0x0400009B RID: 155
        public Player.JumpState jumpState;

        // Token: 0x0400009C RID: 156
        private Texture2D playerText;

        // Token: 0x0400009D RID: 157
        private Texture2D playerHitBoxText;

        // Token: 0x0400009E RID: 158
        private Texture2D attackHitBoxText;

        // Token: 0x0400009F RID: 159
        private Rectangle playerSource;

        // Token: 0x040000A0 RID: 160
        private Rectangle playerDisplay;

        // Token: 0x040000A1 RID: 161
        private Rectangle playerHitBoxDisplay;

        // Token: 0x040000A2 RID: 162
        private Rectangle attackHitBoxDisplay;

        // Token: 0x040000A3 RID: 163
        private Color playerColor;

        // Token: 0x040000A4 RID: 164
        private int sourceWidthLimit;

        // Token: 0x040000A5 RID: 165
        private int spriteRow;

        // Token: 0x040000A6 RID: 166
        private Point checkPoint;

        // Token: 0x040000A7 RID: 167
        private int playerLife;

        // Token: 0x040000A8 RID: 168
        private int playerMoveSpeed;

        // Token: 0x040000A9 RID: 169
        private int playerSprintSpeed;

        // Token: 0x040000AA RID: 170
        private int delay;

        // Token: 0x040000AB RID: 171
        private int animSpeed;

        // Token: 0x040000AC RID: 172
        private bool facingLeftRight;

        // Token: 0x040000AD RID: 173
        private bool isHit;

        // Token: 0x040000AE RID: 174
        private int gravity;

        // Token: 0x040000AF RID: 175
        private int force;

        // Token: 0x02000012 RID: 18
        public enum AnimState
        {
            // Token: 0x040000C9 RID: 201
            Attacking,
            // Token: 0x040000CA RID: 202
            Moving,
            // Token: 0x040000CB RID: 203
            Idle,
            // Token: 0x040000CC RID: 204
            Hit,
            // Token: 0x040000CD RID: 205
            Death
        }

        // Token: 0x02000013 RID: 19
        public enum JumpState
        {
            // Token: 0x040000CF RID: 207
            Jumped,
            // Token: 0x040000D0 RID: 208
            Falling,
            // Token: 0x040000D1 RID: 209
            Ground
        }
    }
}
