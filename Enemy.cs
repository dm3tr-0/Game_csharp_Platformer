using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;


namespace Game_csharp_Platformer
{
    // Token: 0x02000003 RID: 3
    internal class Enemy
    {
        // Token: 0x06000003 RID: 3 RVA: 0x00002148 File Offset: 0x00000348
        public Enemy(Texture2D charText, Rectangle charSource, Rectangle charDisplay, Texture2D hitBoxText, Rectangle hitBoxDisplay, Color charColor, int sourceWidthLimit, Texture2D attackHitBoxText, Rectangle attackHitBoxDisplay, int minPosX, int maxPosX, Texture2D rayCastHitBoxText, Rectangle rayCastHitBoxDisplay, int charLife, int chaseDistance)
        {
            this.animState = Enemy.AnimState.Patrolling;
            this.charText = charText;
            this.charSource = charSource;
            this.charDisplay = charDisplay;
            this.hitBoxText = hitBoxText;
            this.hitBoxDisplay = hitBoxDisplay;
            this.charColor = charColor;
            this.sourceWidthLimit = sourceWidthLimit;
            this.attackHitBoxText = attackHitBoxText;
            this.attackHitBoxDisplay = attackHitBoxDisplay;
            this.minPosX = minPosX;
            this.maxPosX = maxPosX;
            this.rayCastHitBoxText = rayCastHitBoxText;
            this.rayCastHitBoxDisplay = rayCastHitBoxDisplay;
            this.charLife = charLife;
            this.chaseDistance = chaseDistance;
            this.spriteRow = 1;
            this.animSpeed = 24;
            this.initialAnimSpeed = this.animSpeed;
            this.charMoveSpeed = 1;
            this.charChaseSpeed = this.charMoveSpeed * 3;
            this.delay = 0;
            this.attackDelay = 0;
            this.hitBoxPosX = 60;
            this.facingLeftRight = true;
        }

        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000004 RID: 4 RVA: 0x00002224 File Offset: 0x00000424
        public Texture2D CharText
        {
            get
            {
                return this.charText;
            }
        }

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000005 RID: 5 RVA: 0x0000222C File Offset: 0x0000042C
        public Rectangle CharSource
        {
            get
            {
                return this.charSource;
            }
        }

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x06000006 RID: 6 RVA: 0x00002234 File Offset: 0x00000434
        public Rectangle CharDisplay
        {
            get
            {
                return this.charDisplay;
            }
        }

        // Token: 0x17000004 RID: 4
        // (get) Token: 0x06000007 RID: 7 RVA: 0x0000223C File Offset: 0x0000043C
        public Texture2D HitBoxText
        {
            get
            {
                return this.hitBoxText;
            }
        }

        // Token: 0x17000005 RID: 5
        // (get) Token: 0x06000008 RID: 8 RVA: 0x00002244 File Offset: 0x00000444
        public Rectangle HitBoxDisplay
        {
            get
            {
                return this.hitBoxDisplay;
            }
        }

        // Token: 0x17000006 RID: 6
        // (get) Token: 0x06000009 RID: 9 RVA: 0x0000224C File Offset: 0x0000044C
        public Color CharColor
        {
            get
            {
                return this.charColor;
            }
        }

        // Token: 0x17000007 RID: 7
        // (get) Token: 0x0600000A RID: 10 RVA: 0x00002254 File Offset: 0x00000454
        public Texture2D AttackHitBoxText
        {
            get
            {
                return this.attackHitBoxText;
            }
        }

        // Token: 0x17000008 RID: 8
        // (get) Token: 0x0600000B RID: 11 RVA: 0x0000225C File Offset: 0x0000045C
        public Rectangle AttackHitBoxDisplay
        {
            get
            {
                return this.attackHitBoxDisplay;
            }
        }

        // Token: 0x17000009 RID: 9
        // (get) Token: 0x0600000C RID: 12 RVA: 0x00002264 File Offset: 0x00000464
        public Texture2D RayCastHitBoxText
        {
            get
            {
                return this.rayCastHitBoxText;
            }
        }

        // Token: 0x1700000A RID: 10
        // (get) Token: 0x0600000D RID: 13 RVA: 0x0000226C File Offset: 0x0000046C
        public Rectangle RayCastHitBoxDisplay
        {
            get
            {
                return this.rayCastHitBoxDisplay;
            }
        }

        // Token: 0x1700000B RID: 11
        // (set) Token: 0x0600000E RID: 14 RVA: 0x00002274 File Offset: 0x00000474
        public int CharLife
        {
            set
            {
                this.charLife = value;
            }
        }

        // Token: 0x0600000F RID: 15 RVA: 0x00002280 File Offset: 0x00000480
        public void EnemyUpdate(Player player, SoundEffectInstance sfxAttack, SoundEffectInstance sfxDeath)
        {
            if (this.animState != Enemy.AnimState.Death)
            {
                Vector2 value = new Vector2((float)this.charDisplay.X, (float)this.charDisplay.Y);
                Vector2 value2 = new Vector2((float)player.PlayerDisplay.X, (float)player.PlayerDisplay.Y);
                this.distanceToPlayer = Vector2.Distance(value, value2);
                if (this.animState != Enemy.AnimState.Idle && this.animState != Enemy.AnimState.Attacking && this.animState != Enemy.AnimState.Hit)
                {
                    if (this.distanceToPlayer < (float)this.chaseDistance && this.charDisplay.X >= this.minPosX - 50 && this.charDisplay.X <= this.maxPosX + 50)
                    {
                        this.animState = Enemy.AnimState.Chasing;
                    }
                    else
                    {
                        this.animState = Enemy.AnimState.Patrolling;
                    }
                }
                switch (this.animState)
                {
                    case Enemy.AnimState.Attacking:
                        sfxAttack.Play();
                        this.Attack();
                        this.isPlayerInFront = false;
                        break;
                    case Enemy.AnimState.Patrolling:
                        this.Patrol();
                        break;
                    case Enemy.AnimState.Chasing:
                        this.Chase(player);
                        break;
                    case Enemy.AnimState.Hit:
                        if (!this.isHit && this.animState != Enemy.AnimState.Death)
                        {
                            this.isHit = true;
                            this.EnemyHit();
                        }
                        break;
                    case Enemy.AnimState.Idle:
                        this.isHit = false;
                        this.IdleState(player);
                        break;
                }
            }
            else if (!this.isDead)
            {
                this.isDead = !this.isDead;
                sfxDeath.Play();
                Game1.currentEnemyCount--;
                this.EnemyDeath();
            }
            this.AnimationDelay(this.animSpeed);
            this.ChangeAnimationState(this.spriteRow);
            this.hitBoxDisplay.Location = new Point(this.charDisplay.X + this.hitBoxPosX, this.charDisplay.Y + this.charDisplay.Height / 2);
            this.rayCastHitBoxDisplay.Location = new Point(this.charDisplay.X + this.rayHitBoxPosX, this.charDisplay.Y + 75);
        }

        // Token: 0x06000010 RID: 16 RVA: 0x0000247C File Offset: 0x0000067C
        protected virtual void ChangeAnimationState(int spriteRow)
        {
            if (spriteRow == 0 || spriteRow == 5)
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

        // Token: 0x06000011 RID: 17 RVA: 0x00002510 File Offset: 0x00000710
        private void AnimationDelay(int animSpeed)
        {
            if (this.delay >= animSpeed)
            {
                this.CharAnimation();
                this.delay = 0;
            }
            this.delay++;
        }

        // Token: 0x06000012 RID: 18 RVA: 0x00002538 File Offset: 0x00000738
        private void CharAnimation()
        {
            if (this.charSource.X < this.sourceWidthLimit)
            {
                this.charSource.X = this.charSource.X + this.charSource.Width;
                return;
            }
            if (this.animState != Enemy.AnimState.Death)
            {
                if (this.animState == Enemy.AnimState.Attacking || this.animState == Enemy.AnimState.Hit)
                {
                    this.animState = Enemy.AnimState.Idle;
                }
                this.charSource.X = 0;
            }
        }

        // Token: 0x06000013 RID: 19 RVA: 0x000025A0 File Offset: 0x000007A0
        private void Patrol()
        {
            this.animSpeed = this.initialAnimSpeed;
            if (this.charDisplay.X >= this.maxPosX)
            {
                this.facingLeftRight = true;
            }
            else if (this.charDisplay.X <= this.minPosX)
            {
                this.facingLeftRight = false;
            }
            if (this.facingLeftRight)
            {
                this.charDisplay.X = this.charDisplay.X - this.charMoveSpeed;
                this.spriteRow = 0;
                this.hitBoxPosX = 60;
                this.rayHitBoxPosX = -220;
                return;
            }
            this.charDisplay.X = this.charDisplay.X + this.charMoveSpeed;
            this.spriteRow = 5;
            this.hitBoxPosX = 40;
            this.rayHitBoxPosX = 85;
        }

        // Token: 0x06000014 RID: 20 RVA: 0x00002654 File Offset: 0x00000854
        private void Chase(Player player)
        {
            if (this.distanceToPlayer <= 60f && this.distanceToPlayer >= 55f && this.RayCastHitBoxDisplay.Intersects(player.PlayerDisplay))
            {
                this.isPlayerInFront = true;
            }
            else
            {
                this.isPlayerInFront = false;
            }
            if (this.isPlayerInFront)
            {
                this.animState = Enemy.AnimState.Idle;
                this.charSource.X = 0;
                this.isRay = false;
                return;
            }
            if (!this.RayCastHitBoxDisplay.Intersects(player.PlayerDisplay) && !this.isRay)
            {
                this.isRay = true;
                this.facingLeftRight = !this.facingLeftRight;
            }
            if (this.facingLeftRight)
            {
                this.spriteRow = 0;
                this.hitBoxPosX = 60;
                this.charDisplay.X = this.charDisplay.X - this.charMoveSpeed * this.charChaseSpeed;
                this.rayHitBoxPosX = -220;
                return;
            }
            this.spriteRow = 5;
            this.hitBoxPosX = 40;
            this.charDisplay.X = this.charDisplay.X + this.charMoveSpeed * this.charChaseSpeed;
            this.rayHitBoxPosX = 85;
        }

        // Token: 0x06000015 RID: 21 RVA: 0x00002770 File Offset: 0x00000970
        private void Attack()
        {
            this.animSpeed = 7;
            if (this.facingLeftRight)
            {
                this.attackHitBoxDisplay.Location = new Point(this.charDisplay.X, this.charDisplay.Y + this.charDisplay.Height / 2);
                this.spriteRow = 2;
                return;
            }
            this.attackHitBoxDisplay.Location = new Point(this.charDisplay.X + 100, this.charDisplay.Y + this.charDisplay.Height / 2);
            this.spriteRow = 7;
        }

        // Token: 0x06000016 RID: 22 RVA: 0x00002808 File Offset: 0x00000A08
        private void IdleState(Player player)
        {
            if (this.facingLeftRight)
            {
                this.spriteRow = 1;
            }
            else
            {
                this.spriteRow = 6;
            }
            if (player.animState != Player.AnimState.Death)
            {
                if (this.distanceToPlayer >= 60f)
                {
                    this.isPlayerInFront = false;
                }
                if (this.attackDelay >= 5)
                {
                    this.animState = Enemy.AnimState.Attacking;
                    this.attackDelay = 0;
                    if (this.attackHitBoxDisplay.Intersects(player.PlayerHitBoxDisplay))
                    {
                        player.animState = Player.AnimState.Hit;
                    }
                }
                this.attackDelay++;
            }
            else
            {
                this.isPlayerInFront = false;
            }
            if (!this.isPlayerInFront)
            {
                this.animState = Enemy.AnimState.Patrolling;
                this.attackDelay = 0;
            }
        }

        // Token: 0x06000017 RID: 23 RVA: 0x000028A8 File Offset: 0x00000AA8
        private void EnemyHit()
        {
            if (this.animState != Enemy.AnimState.Death)
            {
                this.animSpeed = 5;
                this.charSource.X = 0;
                if (this.facingLeftRight)
                {
                    this.charDisplay.X = this.charDisplay.X + 40;
                    this.spriteRow = 4;
                }
                else
                {
                    this.spriteRow = 9;
                    this.charDisplay.X = this.charDisplay.X - 40;
                }
                this.charLife--;
                if (this.charLife <= 0)
                {
                    this.animState = Enemy.AnimState.Death;
                }
            }
        }

        // Token: 0x06000018 RID: 24 RVA: 0x0000292A File Offset: 0x00000B2A
        private void EnemyDeath()
        {
            this.animSpeed = 12;
            this.charSource.X = 0;
            this.EnemyDeathStateAnim();
        }

        // Token: 0x06000019 RID: 25 RVA: 0x00002946 File Offset: 0x00000B46
        private void EnemyDeathStateAnim()
        {
            if (this.facingLeftRight)
            {
                this.spriteRow = 3;
                return;
            }
            this.spriteRow = 8;
        }

        // Token: 0x0600001A RID: 26 RVA: 0x00002960 File Offset: 0x00000B60
        public void EnemyDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.CharText, this.CharDisplay, new Rectangle?(this.CharSource), this.CharColor);
            if (this.animState != Enemy.AnimState.Death)
            {
                spriteBatch.Draw(this.HitBoxText, this.HitBoxDisplay, Color.Transparent);
                spriteBatch.Draw(this.RayCastHitBoxText, this.RayCastHitBoxDisplay, Color.Transparent);
                if (this.animState == Enemy.AnimState.Attacking)
                {
                    spriteBatch.Draw(this.AttackHitBoxText, this.AttackHitBoxDisplay, Color.Transparent);
                }
            }
        }

        // Token: 0x04000001 RID: 1
        public Enemy.AnimState animState;

        // Token: 0x04000002 RID: 2
        protected Texture2D charText;

        // Token: 0x04000003 RID: 3
        protected Texture2D hitBoxText;

        // Token: 0x04000004 RID: 4
        protected Texture2D attackHitBoxText;

        // Token: 0x04000005 RID: 5
        protected Texture2D rayCastHitBoxText;

        // Token: 0x04000006 RID: 6
        protected Rectangle charSource;

        // Token: 0x04000007 RID: 7
        protected Rectangle charDisplay;

        // Token: 0x04000008 RID: 8
        protected Rectangle hitBoxDisplay;

        // Token: 0x04000009 RID: 9
        protected Rectangle attackHitBoxDisplay;

        // Token: 0x0400000A RID: 10
        protected Rectangle rayCastHitBoxDisplay;

        // Token: 0x0400000B RID: 11
        protected Color charColor;

        // Token: 0x0400000C RID: 12
        protected int sourceWidthLimit;

        // Token: 0x0400000D RID: 13
        protected int spriteRow;

        // Token: 0x0400000E RID: 14
        protected int hitBoxPosX;

        // Token: 0x0400000F RID: 15
        protected int rayHitBoxPosX;

        // Token: 0x04000010 RID: 16
        protected int charMoveSpeed;

        // Token: 0x04000011 RID: 17
        protected int charChaseSpeed;

        // Token: 0x04000012 RID: 18
        protected int delay;

        // Token: 0x04000013 RID: 19
        protected int attackDelay;

        // Token: 0x04000014 RID: 20
        protected int animSpeed;

        // Token: 0x04000015 RID: 21
        protected int initialAnimSpeed;

        // Token: 0x04000016 RID: 22
        protected float distanceToPlayer;

        // Token: 0x04000017 RID: 23
        protected int chaseDistance;

        // Token: 0x04000018 RID: 24
        protected int charLife;

        // Token: 0x04000019 RID: 25
        protected bool facingLeftRight;

        // Token: 0x0400001A RID: 26
        protected bool isPlayerInFront;

        // Token: 0x0400001B RID: 27
        protected bool isRay;

        // Token: 0x0400001C RID: 28
        protected bool isHit;

        // Token: 0x0400001D RID: 29
        protected bool isDead;

        // Token: 0x0400001E RID: 30
        protected int minPosX;

        // Token: 0x0400001F RID: 31
        protected int maxPosX;

        // Token: 0x0200000F RID: 15
        public enum AnimState
        {
            // Token: 0x040000B5 RID: 181
            Attacking,
            // Token: 0x040000B6 RID: 182
            Patrolling,
            // Token: 0x040000B7 RID: 183
            Chasing,
            // Token: 0x040000B8 RID: 184
            Death,
            // Token: 0x040000B9 RID: 185
            Hit,
            // Token: 0x040000BA RID: 186
            Idle
        }
    }
}
