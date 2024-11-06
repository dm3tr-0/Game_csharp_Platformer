using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_csharp_Platformer
{
    // Token: 0x02000004 RID: 4
    public class Game1 : Game
    {
        // Token: 0x0600001B RID: 27 RVA: 0x000029E8 File Offset: 0x00000BE8
        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            base.Content.RootDirectory = "Content";
            base.Window.Title = "Depth of the Thicket";
            this.graphics.PreferredBackBufferHeight = 544;
            base.IsMouseVisible = true;
            this.graphics.ApplyChanges();
        }

        // Token: 0x0600001C RID: 28 RVA: 0x00002A4C File Offset: 0x00000C4C
        protected override void Initialize()
        {
            this.gameManager = new GameManager(GameManager.GameStateEnum.MENU, GameManager.GameLevel.LEVEL1A);
            Game1.hasPressed = false;
            Game1.hasPaused = false;
            this.camera = Matrix.Identity;
            this.saveSystem = new SaveLoadSystem();
            Texture2D menuBGText = base.Content.Load<Texture2D>("BGMENU");
            this.mainMenuUI = new MainMenu(menuBGText, new Rectangle(0, 0, base.Window.ClientBounds.Width, base.Window.ClientBounds.Height), Color.White);
            this.buttonsUI = new List<ListButtons>();
            int num = 70;
            int x = base.Window.ClientBounds.Width / 2 - 90;
            for (int i = 0; i < 5; i++)
            {
                Point point = new Point(x, base.Window.ClientBounds.Height / 2 - num);
                ListButtons item = new ListButtons(base.Content.Load<Texture2D>("buttonN"), base.Content.Load<Texture2D>("buttonP"), new Rectangle(point.X, point.Y, 169, 56), Color.White, new Vector2((float)(point.X + 17), (float)(point.Y + 13)), new Vector2((float)(point.X + 17), (float)(point.Y + 23)), Color.White, i);
                if (i == 3)
                {
                    num = -140;
                }
                else
                {
                    num -= 70;
                }
                this.buttonsUI.Add(item);
            }
            Texture2D texture2D = base.Content.Load<Texture2D>("ConceptMeAnimation");
            Rectangle rectangle = new Rectangle(100, 315, 128, 128);
            Rectangle rectangle2 = new Rectangle(0, 0, texture2D.Width / 8, texture2D.Height / 14);
            this.player = new Player(texture2D, rectangle2, rectangle, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 4, rectangle.Height / 2), Color.White, texture2D.Width - rectangle2.Width, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 4, rectangle.Height / 2));
            this.player.PlayerHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.player.AttackHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.heart = new List<GameHUD>();
            Texture2D uiText = base.Content.Load<Texture2D>("heart");
            int num2 = 0;
            for (int j = 0; j < this.player.PlayerLife; j++)
            {
                GameHUD item2 = new GameHUD(uiText, new Rectangle(num2, 0, 64, 64), Color.White);
                this.heart.Add(item2);
                num2 += 64;
            }
            this.bloatedIcon = base.Content.Load<Texture2D>("bloatedIcon");
            this.centipedeIcon = base.Content.Load<Texture2D>("centipedeIcon");
            this.currentEnemyIcon = new GameHUD(this.bloatedIcon, new Rectangle(base.Window.ClientBounds.Width - 100, 10, 64, 64), Color.White);
            this.objIcon = new GameHUDwText(base.Content.Load<Texture2D>("parchmentUI"), new Rectangle(base.Window.ClientBounds.Width - 280, 20, 234, 64), Color.White, new Vector2((float)(base.Window.ClientBounds.Width - 260), 30f), Color.Black);
            this.pauseUI = new GameHUDwText(base.Content.Load<Texture2D>("bParchmentUI"), new Rectangle(base.Window.ClientBounds.Width / 2 - 125, base.Window.ClientBounds.Height / 2 - 50, 240, 96), Color.White, new Vector2((float)(base.Window.ClientBounds.Width / 2 - 60), (float)(base.Window.ClientBounds.Height / 2 - 20)), Color.Black);
            this.gameOverUIButtons = new List<ListButtons>();
            int x2 = base.Window.ClientBounds.Width / 2 - 300;
            for (int k = 5; k < 7; k++)
            {
                Point point2 = new Point(x2, base.Window.ClientBounds.Height / 2 + 100);
                ListButtons item3 = new ListButtons(base.Content.Load<Texture2D>("buttonN"), base.Content.Load<Texture2D>("buttonP"), new Rectangle(point2.X, point2.Y, 169, 56), Color.White, new Vector2((float)(point2.X + 17), (float)(point2.Y + 13)), new Vector2((float)(point2.X + 17), (float)(point2.Y + 23)), Color.White, k);
                this.gameOverUIButtons.Add(item3);
                x2 = base.Window.ClientBounds.Width / 2 + 130;
            }
            Texture2D texture2D2 = base.Content.Load<Texture2D>("big bloated");
            Rectangle rectangle3 = new Rectangle(0, 0, texture2D2.Width / 6, texture2D2.Height / 10);
            int sourceWidthLimit = texture2D2.Width - rectangle3.Width;
            this.bloatEnemies1A = new List<Enemy>();
            Rectangle rectangle4 = new Rectangle(400, 305, 144, 144);
            Enemy enemy = new Enemy(texture2D2, rectangle3, rectangle4, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle4.X, rectangle4.Y, rectangle4.Width / 3, rectangle4.Height / 2), Color.White, sourceWidthLimit, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle4.X, rectangle4.Y, rectangle4.Width / 4, rectangle4.Height / 2), 400, 700, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle4.X, rectangle4.Y, rectangle4.Width * 2, rectangle4.Height / 4), 7, 200);
            enemy.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            enemy.AttackHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            enemy.RayCastHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.bloatEnemies1A.Add(enemy);
            Rectangle rectangle5 = new Rectangle(1400, 220, 144, 144);
            Enemy enemy2 = new Enemy(texture2D2, rectangle3, rectangle5, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle5.X, rectangle5.Y, rectangle5.Width / 3, rectangle5.Height / 2), Color.White, sourceWidthLimit, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle5.X, rectangle5.Y, rectangle5.Width / 4, rectangle5.Height / 2), 1400, 1800, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle5.X, rectangle5.Y, rectangle5.Width * 2, rectangle5.Height / 4), 7, 200);
            enemy2.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            enemy2.AttackHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            enemy2.RayCastHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.bloatEnemies1A.Add(enemy2);
            this.bloatEnemies1B = new List<Enemy>();
            Rectangle rectangle6 = new Rectangle(200, 225, 144, 144);
            Enemy enemy3 = new Enemy(texture2D2, rectangle3, rectangle6, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle6.X, rectangle6.Y, rectangle6.Width / 3, rectangle6.Height / 2), Color.White, sourceWidthLimit, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle6.X, rectangle6.Y, rectangle6.Width / 4, rectangle6.Height / 2), 200, 500, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle6.X, rectangle6.Y, rectangle6.Width * 2, rectangle6.Height / 4), 7, 200);
            enemy3.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            enemy3.AttackHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            enemy3.RayCastHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.bloatEnemies1B.Add(enemy3);
            Rectangle rectangle7 = new Rectangle(1800, 270, 144, 144);
            Enemy enemy4 = new Enemy(texture2D2, rectangle3, rectangle7, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle7.X, rectangle7.Y, rectangle7.Width / 3, rectangle7.Height / 2), Color.PaleVioletRed, sourceWidthLimit, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle7.X, rectangle7.Y, rectangle7.Width / 4, rectangle7.Height / 2), 1800, 2100, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle7.X, rectangle7.Y, rectangle7.Width * 2, rectangle7.Height / 4), 12, 200);
            enemy4.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            enemy4.AttackHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            enemy4.RayCastHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.bloatEnemies1B.Add(enemy4);
            this.bloatEnemies1C = new List<Enemy>();
            Rectangle rectangle8 = new Rectangle(1500, 220, 144, 144);
            Enemy enemy5 = new Enemy(texture2D2, rectangle3, rectangle8, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle8.X, rectangle8.Y, rectangle8.Width / 3, rectangle8.Height / 2), Color.MediumVioletRed, sourceWidthLimit, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle8.X, rectangle8.Y, rectangle8.Width / 4, rectangle8.Height / 2), 1500, 2100, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle8.X, rectangle8.Y, rectangle8.Width * 2, rectangle8.Height / 4), 30, 300);
            enemy5.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            enemy5.AttackHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            enemy5.RayCastHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.bloatEnemies1C.Add(enemy5);
            Texture2D texture2D3 = base.Content.Load<Texture2D>("centipede");
            Rectangle rectangle9 = new Rectangle(0, 0, texture2D3.Width / 6, texture2D3.Height / 10);
            int sourceWidthLimit2 = texture2D3.Width - rectangle9.Width;
            this.centipedeEnemies2A = new List<Enemy>();
            int num3 = 400;
            int num4 = 700;
            for (int l = 0; l < 3; l++)
            {
                Rectangle rectangle10 = new Rectangle(num3, 305, 144, 144);
                Centipede centipede = new Centipede(texture2D3, rectangle9, rectangle10, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle10.X, rectangle10.Y, rectangle10.Width / 3, rectangle10.Height / 2), Color.White, sourceWidthLimit2, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle10.X, rectangle10.Y, rectangle10.Width / 4, rectangle10.Height / 2), num3, num4, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle10.X, rectangle10.Y, rectangle10.Width * 2, rectangle10.Height / 4), 10, 200);
                centipede.HitBoxText.SetData<Color>(new Color[]
                {
                    Color.White
                });
                centipede.AttackHitBoxText.SetData<Color>(new Color[]
                {
                    Color.White
                });
                centipede.RayCastHitBoxText.SetData<Color>(new Color[]
                {
                    Color.White
                });
                this.centipedeEnemies2A.Add(centipede);
                num3 += 450;
                num4 += 450;
            }
            this.centipedeEnemies2B = new List<Enemy>();
            Rectangle rectangle11 = new Rectangle(1600, 180, 144, 144);
            Centipede centipede2 = new Centipede(texture2D3, rectangle9, rectangle11, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle11.X, rectangle11.Y, rectangle11.Width / 3, rectangle11.Height / 2), Color.BlueViolet, sourceWidthLimit2, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle11.X, rectangle11.Y, rectangle11.Width / 4, rectangle11.Height / 2), 1300, 1600, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle11.X, rectangle11.Y, rectangle11.Width * 2, rectangle11.Height / 4), 20, 200);
            centipede2.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            centipede2.AttackHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            centipede2.RayCastHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.centipedeEnemies2B.Add(centipede2);
            this.centipedeEnemies2C = new List<Enemy>();
            Rectangle rectangle12 = new Rectangle(1300, 300, 144, 144);
            Centipede centipede3 = new Centipede(texture2D3, rectangle9, rectangle12, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle12.X, rectangle12.Y, rectangle12.Width / 3, rectangle12.Height / 2), Color.PaleVioletRed, sourceWidthLimit2, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle12.X, rectangle12.Y, rectangle12.Width / 4, rectangle12.Height / 2), 1300, 1600, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle12.X, rectangle12.Y, rectangle12.Width * 2, rectangle12.Height / 4), 30, 400);
            centipede3.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            centipede3.AttackHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            centipede3.RayCastHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.centipedeEnemies2C.Add(centipede3);
            Texture2D charText = base.Content.Load<Texture2D>("big bloated");
            Rectangle rectangle13 = new Rectangle(1400, 300, 144, 144);
            Enemy enemy6 = new Enemy(charText, rectangle9, rectangle13, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle13.X, rectangle13.Y, rectangle13.Width / 3, rectangle13.Height / 2), Color.MediumVioletRed, sourceWidthLimit, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle13.X, rectangle13.Y, rectangle13.Width / 4, rectangle13.Height / 2), 1400, 1800, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle13.X, rectangle13.Y, rectangle13.Width * 2, rectangle13.Height / 4), 30, 400);
            enemy6.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            enemy6.AttackHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            enemy6.RayCastHitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.centipedeEnemies2C.Add(enemy6);
            Texture2D texture2D4 = base.Content.Load<Texture2D>("platform");
            this.platforms1A = new List<Platform>();
            Rectangle platSource = new Rectangle(0, 0, texture2D4.Width / 2, texture2D4.Height);
            Rectangle rectangle14 = new Rectangle(0, base.Window.ClientBounds.Height - 112, 1152, 128);
            Platform platform = new Platform(texture2D4, platSource, rectangle14, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle14.X, rectangle14.Y + 7, rectangle14.Width, rectangle14.Height / 2));
            platform.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms1A.Add(platform);
            Rectangle platSource2 = new Rectangle(texture2D4.Width / 2, 0, texture2D4.Width / 2, texture2D4.Height);
            Rectangle rectangle15 = new Rectangle(texture2D4.Width / 2 + 100, base.Window.ClientBounds.Height - 192, 1152, 128);
            Platform platform2 = new Platform(texture2D4, platSource2, rectangle15, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle15.X, rectangle15.Y + 7, rectangle15.Width, rectangle15.Height / 2));
            platform2.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms1A.Add(platform2);
            this.platforms1B = new List<Platform>();
            Rectangle platSource3 = new Rectangle(texture2D4.Width / 2, 0, texture2D4.Width / 3, texture2D4.Height);
            Rectangle rectangle16 = new Rectangle(0, base.Window.ClientBounds.Height - 192, 768, 128);
            Platform platform3 = new Platform(texture2D4, platSource3, rectangle16, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle16.X, rectangle16.Y + 7, rectangle16.Width, rectangle16.Height / 2));
            platform3.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms1B.Add(platform3);
            int num5 = 900;
            int num6 = 90;
            for (int m = 0; m < 3; m++)
            {
                Rectangle platSource4 = new Rectangle(0, 0, 128, texture2D4.Height);
                Rectangle rectangle17 = new Rectangle(num5, base.Window.ClientBounds.Height - num6, 128, 128);
                Platform platform4 = new Platform(texture2D4, platSource4, rectangle17, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle17.X, rectangle17.Y + 7, rectangle17.Width, rectangle17.Height / 2));
                platform4.HitBoxText.SetData<Color>(new Color[]
                {
                    Color.White
                });
                this.platforms1B.Add(platform4);
                num5 += 268;
                if (m == 0)
                {
                    num6 = 130;
                }
                else
                {
                    num6 = 90;
                }
            }
            Rectangle platSource5 = new Rectangle(texture2D4.Width / 2, 0, texture2D4.Width / 3, texture2D4.Height);
            Rectangle rectangle18 = new Rectangle(1684, base.Window.ClientBounds.Height - 142, 768, 128);
            Platform platform5 = new Platform(texture2D4, platSource5, rectangle18, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle18.X, rectangle18.Y + 7, rectangle18.Width, rectangle18.Height / 2));
            platform5.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms1B.Add(platform5);
            this.platforms1C = new List<Platform>();
            Rectangle platSource6 = new Rectangle(0, 0, 512, texture2D4.Height);
            Rectangle rectangle19 = new Rectangle(0, base.Window.ClientBounds.Height - 142, 512, 128);
            Platform platform6 = new Platform(texture2D4, platSource6, rectangle19, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle19.X, rectangle19.Y + 7, rectangle19.Width, rectangle19.Height / 2));
            platform6.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms1C.Add(platform6);
            Rectangle platSource7 = new Rectangle(128, 0, 128, texture2D4.Height);
            Rectangle rectangle20 = new Rectangle(655, base.Window.ClientBounds.Height - 142, 128, 128);
            Platform platform7 = new Platform(texture2D4, platSource7, rectangle20, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle20.X, rectangle20.Y + 7, rectangle20.Width, rectangle20.Height / 2));
            platform7.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms1C.Add(platform7);
            Rectangle platSource8 = new Rectangle(texture2D4.Width / 2, 0, 256, texture2D4.Height);
            Rectangle rectangle21 = new Rectangle(900, base.Window.ClientBounds.Height - 142, 256, 128);
            Platform platform8 = new Platform(texture2D4, platSource8, rectangle21, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle21.X, rectangle21.Y + 7, rectangle21.Width, rectangle21.Height / 2));
            platform8.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms1C.Add(platform8);
            Rectangle platSource9 = new Rectangle(0, 0, texture2D4.Width / 2, texture2D4.Height);
            Rectangle rectangle22 = new Rectangle(1256, base.Window.ClientBounds.Height - 200, texture2D4.Width / 2, 128);
            Platform platform9 = new Platform(texture2D4, platSource9, rectangle22, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle22.X, rectangle22.Y + 7, rectangle22.Width, rectangle22.Height / 2));
            platform9.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms1C.Add(platform9);
            Texture2D texture2D5 = base.Content.Load<Texture2D>("platformDeep");
            this.platforms2A = new List<Platform>();
            Rectangle platSource10 = new Rectangle(0, 0, texture2D5.Width, texture2D5.Height);
            Rectangle rectangle23 = new Rectangle(0, base.Window.ClientBounds.Height - 140, 2304, 128);
            Platform platform10 = new Platform(texture2D5, platSource10, rectangle23, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle23.X, rectangle23.Y + 35, rectangle23.Width, rectangle23.Height / 2));
            platform10.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms2A.Add(platform10);
            this.platforms2B = new List<Platform>();
            Rectangle platSource11 = new Rectangle(0, 0, 256, texture2D5.Height);
            Rectangle rectangle24 = new Rectangle(0, base.Window.ClientBounds.Height - 140, 256, 128);
            Platform platform11 = new Platform(texture2D5, platSource11, rectangle24, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle24.X, rectangle24.Y + 35, rectangle24.Width, rectangle24.Height / 2));
            platform11.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms2B.Add(platform11);
            int num7 = 356;
            int num8 = base.Window.ClientBounds.Height - 140;
            int num9 = 0;
            for (int n = 0; n < 3; n++)
            {
                Rectangle platSource12 = new Rectangle(num9, 0, 128, texture2D5.Height);
                Rectangle rectangle25 = new Rectangle(num7, num8, 128, 128);
                Platform platform12 = new Platform(texture2D5, platSource12, rectangle25, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle25.X, rectangle25.Y + 35, rectangle25.Width, rectangle25.Height / 2));
                platform12.HitBoxText.SetData<Color>(new Color[]
                {
                    Color.White
                });
                this.platforms2B.Add(platform12);
                num7 += 278;
                num8 -= 40;
                num9 += 128;
            }
            Rectangle platSource13 = new Rectangle(0, 0, 512, texture2D5.Height);
            Rectangle rectangle26 = new Rectangle(num7 + 50, num8, 512, 128);
            Platform platform13 = new Platform(texture2D5, platSource13, rectangle26, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle26.X, rectangle26.Y + 35, rectangle26.Width, rectangle26.Height / 2));
            platform13.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms2B.Add(platform13);
            Rectangle platSource14 = new Rectangle(1280, 0, 128, texture2D5.Height);
            Rectangle rectangle27 = new Rectangle(1900, base.Window.ClientBounds.Height / 2, 128, 128);
            Platform platform14 = new Platform(texture2D5, platSource14, rectangle27, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle27.X, rectangle27.Y + 35, rectangle27.Width, rectangle27.Height / 2));
            platform14.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms2B.Add(platform14);
            Rectangle platSource15 = new Rectangle(1408, 0, 128, texture2D5.Height);
            Rectangle rectangle28 = new Rectangle(2200, num8 + 40, 128, 128);
            Platform platform15 = new Platform(texture2D5, platSource15, rectangle28, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle28.X, rectangle28.Y + 35, rectangle28.Width, rectangle28.Height / 2));
            platform15.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms2B.Add(platform15);
            this.platforms2C = new List<Platform>();
            Rectangle platSource16 = new Rectangle(1408, 0, 256, texture2D5.Height);
            Rectangle rectangle29 = new Rectangle(0, num8 + 40, 256, 128);
            Platform platform16 = new Platform(texture2D5, platSource16, rectangle29, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle29.X, rectangle29.Y + 35, rectangle29.Width, rectangle29.Height / 2));
            platform16.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms2C.Add(platform16);
            Rectangle platSource17 = new Rectangle(0, 0, texture2D5.Width, texture2D5.Height);
            Rectangle rectangle30 = new Rectangle(462, base.Window.ClientBounds.Height / 2 + 120, texture2D5.Width, 128);
            Platform platform17 = new Platform(texture2D5, platSource17, rectangle30, Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(rectangle30.X, rectangle30.Y + 35, rectangle30.Width, rectangle30.Height / 2));
            platform17.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            this.platforms2C.Add(platform17);
            this.jungleBorder = base.Content.Load<Texture2D>("jungleBorder");
            this.deepBorder = base.Content.Load<Texture2D>("deepBorder");
            this.currentBorder = new Platform(this.jungleBorder, Rectangle.Empty, new Rectangle(0, 0, 2304, 544), Color.White, new Texture2D(base.GraphicsDevice, 1, 1), new Rectangle(2280, 0, 43, 544));
            this.currentBorder.HitBoxText.SetData<Color>(new Color[]
            {
                Color.White
            });
            base.Initialize();
        }

        // Token: 0x0600001D RID: 29 RVA: 0x000048EC File Offset: 0x00002AEC
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(base.GraphicsDevice);
            this.gameFont = base.Content.Load<SpriteFont>("GameFont");
            this.title = base.Content.Load<Texture2D>("title");
            this.boardUI = base.Content.Load<Texture2D>("boardUI");
            this.controls = base.Content.Load<Texture2D>("controls");
            this.sfxClick = base.Content.Load<SoundEffect>("buttonConfirm");
            this.sfxClickInstance = this.sfxClick.CreateInstance();
            this.sfxClickInstance.Volume = 0.2f;
            this.sfxCancel = base.Content.Load<SoundEffect>("buttonCancel");
            this.sfxCancelInstance = this.sfxCancel.CreateInstance();
            this.sfxCancelInstance.Volume = 0.3f;
            this.sfxAreaClear = base.Content.Load<SoundEffect>("clearAreaSFX");
            this.sfxAreaClearInst = this.sfxAreaClear.CreateInstance();
            this.sfxAreaClearInst.Volume = 1f;
            this.sfxCheckPoint = base.Content.Load<SoundEffect>("checkPointSFX");
            this.sfxCheckPInst = this.sfxCheckPoint.CreateInstance();
            this.sfxCheckPInst.Volume = 0.1f;
            this.sfxLevelCmpt = base.Content.Load<SoundEffect>("levelComplete");
            this.sfxLevelCmptInstance = this.sfxLevelCmpt.CreateInstance();
            this.sfxLevelCmptInstance.Volume = 0.3f;
            this.bgmMenu = base.Content.Load<SoundEffect>("heroBG");
            this.bgmMenuInstance = this.bgmMenu.CreateInstance();
            this.bgmMenuInstance.IsLooped = true;
            this.bgmMenuInstance.Volume = 0.3f;
            this.bgmJungle = base.Content.Load<SoundEffect>("jungleBGM");
            this.bgmJungleInstance = this.bgmJungle.CreateInstance();
            this.bgmJungleInstance.IsLooped = true;
            this.bgmJungleInstance.Volume = 0.1f;
            this.bgmJungleBoss = base.Content.Load<SoundEffect>("jungleBossBGM");
            this.bgmJungleBossInstance = this.bgmJungleBoss.CreateInstance();
            this.bgmJungleBossInstance.IsLooped = true;
            this.bgmJungleBossInstance.Volume = 0.1f;
            this.bgmDeepJungle = base.Content.Load<SoundEffect>("deepJungleBGM");
            this.bgmDeepJungleInstance = this.bgmDeepJungle.CreateInstance();
            this.bgmDeepJungleInstance.IsLooped = true;
            this.bgmDeepJungleInstance.Volume = 0.1f;
            this.bgmDeepBoss = base.Content.Load<SoundEffect>("deepJungleBossBGM");
            this.bgmDeepBossInstance = this.bgmDeepBoss.CreateInstance();
            this.bgmDeepBossInstance.IsLooped = true;
            this.bgmDeepBossInstance.Volume = 0.1f;
            this.sfxWalk = base.Content.Load<SoundEffect>("walking");
            this.sfxWalkInstance = this.sfxWalk.CreateInstance();
            this.sfxWalkInstance.Volume = 0.3f;
            this.sfxRun = base.Content.Load<SoundEffect>("run");
            this.sfxRunInstance = this.sfxRun.CreateInstance();
            this.sfxRunInstance.Volume = 0.3f;
            this.sfxSlash = base.Content.Load<SoundEffect>("slash");
            this.sfxSlashInstance = this.sfxSlash.CreateInstance();
            this.sfxSlashInstance.Volume = 0.1f;
            this.sfxWhoosh = base.Content.Load<SoundEffect>("whoosh");
            this.sfxWhooshInstance = this.sfxWhoosh.CreateInstance();
            this.sfxWhooshInstance.Volume = 0.4f;
            this.sfxJump = base.Content.Load<SoundEffect>("jump");
            this.sfxJumpInstance = this.sfxJump.CreateInstance();
            this.sfxJumpInstance.Volume = 0.1f;
            this.sfxHit = base.Content.Load<SoundEffect>("hit");
            this.sfxHitInstance = this.sfxHit.CreateInstance();
            this.sfxHitInstance.Volume = 0.3f;
            this.sfxGameOver = base.Content.Load<SoundEffect>("gameOver");
            this.sfxGameOverInstance = this.sfxGameOver.CreateInstance();
            this.sfxGameOverInstance.Volume = 0.3f;
            this.sfxbloatAtk = base.Content.Load<SoundEffect>("bloatAttack");
            this.sfxbloatAtkInstance = this.sfxbloatAtk.CreateInstance();
            this.sfxbloatAtkInstance.Volume = 0.1f;
            this.sfxbloatDeath = base.Content.Load<SoundEffect>("bloatDeath");
            this.sfxbloatDeathInstance = this.sfxbloatDeath.CreateInstance();
            this.sfxbloatDeathInstance.Volume = 0.7f;
            this.jungleBG = base.Content.Load<Texture2D>("jungleBG");
            this.deepJungleBG = base.Content.Load<Texture2D>("deepForestBG");
            this.SetLevel(this.platforms1A, this.bloatEnemies1A, this.bloatEnemies1A.Count, this.bloatedIcon, this.jungleBorder, this.jungleBG, this.bgmJungleInstance);
        }

        // Token: 0x0600001E RID: 30 RVA: 0x00004E14 File Offset: 0x00003014
        protected override void UnloadContent()
        {
        }

        // Token: 0x0600001F RID: 31 RVA: 0x00004E18 File Offset: 0x00003018
        protected override void Update(GameTime gameTime)
        {
            switch (this.gameManager.gameState)
            {
                case GameManager.GameStateEnum.MENU:
                    this.mainMenuUI.Update(this.buttonsUI, this.gameManager, this.bgmMenuInstance, this.sfxClickInstance, this.sfxCancelInstance);
                    break;
                case GameManager.GameStateEnum.START:
                    this.cameraPosX = (float)(-(float)this.player.PlayerDisplay.X + base.Window.ClientBounds.Width / 3);
                    if (this.cameraPosX >= 0f)
                    {
                        this.cameraPosX = 0f;
                    }
                    else if (this.cameraPosX <= -1500f)
                    {
                        this.cameraPosX = -1500f;
                    }
                    this.camera = Matrix.CreateTranslation(this.cameraPosX, this.camera.Translation.Y, this.camera.Translation.Z);
                    for (int i = 0; i < this.heart.Count; i++)
                    {
                        this.heart[i].Move(this.cameraPosX);
                    }
                    this.currentEnemyIcon.Move(this.cameraPosX);
                    this.objIcon.Move(this.cameraPosX);
                    this.pauseUI.Move(this.cameraPosX);
                    foreach (ListButtons listButtons in this.gameOverUIButtons)
                    {
                        listButtons.Move(this.cameraPosX);
                    }
                    if (!Game1.hasPaused)
                    {
                        this.player.PlayerUpdate(this.currentEnemies, this.sfxWalkInstance, this.sfxSlashInstance, this.sfxRunInstance, this.sfxWhooshInstance, this.sfxJumpInstance, this.sfxHitInstance, this.sfxGameOverInstance);
                        foreach (Platform platform in this.currentPlatforms)
                        {
                            if (platform.HitBoxDisplay.Intersects(this.player.PlayerHitBoxDisplay))
                            {
                                if (this.player.PlayerHitBoxDisplay.Bottom >= platform.HitBoxDisplay.Bottom)
                                {
                                    this.player.jumpState = Player.JumpState.Falling;
                                    break;
                                }
                                this.player.jumpState = Player.JumpState.Ground;
                                break;
                            }
                            else if (this.player.jumpState != Player.JumpState.Jumped)
                            {
                                this.player.jumpState = Player.JumpState.Falling;
                            }
                        }
                        this.currentBGM.Play();
                        if (Game1.currentEnemyCount == 0 && !this.isAreaClear)
                        {
                            this.sfxAreaClearInst.Play();
                            this.isAreaClear = true;
                        }
                        if (this.currentBorder.HitBoxDisplay.Intersects(this.player.PlayerHitBoxDisplay) && Game1.currentEnemyCount == 0)
                        {
                            this.isAreaClear = false;
                            this.player.PlayerDisplay = new Rectangle(-32, this.player.PlayerDisplay.Y, this.player.PlayerDisplay.Width, this.player.PlayerDisplay.Height);
                            switch (this.gameManager.gameLevel)
                            {
                                case GameManager.GameLevel.LEVEL1A:
                                    this.SetLevel(this.platforms1B, this.bloatEnemies1B, this.bloatEnemies1B.Count, this.bloatedIcon, this.jungleBorder, this.jungleBG, this.bgmJungleInstance);
                                    this.gameManager.gameLevel = GameManager.GameLevel.LEVEL1B;
                                    this.SaveLevel();
                                    break;
                                case GameManager.GameLevel.LEVEL1B:
                                    this.currentBGM.Stop();
                                    this.gameManager.gameLevel = GameManager.GameLevel.LEVEL1C;
                                    this.SetLevel(this.platforms1C, this.bloatEnemies1C, this.bloatEnemies1C.Count, this.bloatedIcon, this.jungleBorder, this.jungleBG, this.bgmJungleBossInstance);
                                    this.SaveLevel();
                                    break;
                                case GameManager.GameLevel.LEVEL1C:
                                    this.currentBGM.Stop();
                                    this.gameManager.gameLevel = GameManager.GameLevel.LEVEL2A;
                                    this.SetLevel(this.platforms2A, this.centipedeEnemies2A, this.centipedeEnemies2A.Count, this.centipedeIcon, this.deepBorder, this.deepJungleBG, this.bgmDeepJungleInstance);
                                    break;
                                case GameManager.GameLevel.LEVEL2A:
                                    this.SetLevel(this.platforms2B, this.centipedeEnemies2B, this.centipedeEnemies2B.Count, this.centipedeIcon, this.deepBorder, this.deepJungleBG, this.bgmDeepJungleInstance);
                                    this.gameManager.gameLevel = GameManager.GameLevel.LEVEL2B;
                                    this.SaveLevel();
                                    break;
                                case GameManager.GameLevel.LEVEL2B:
                                    this.currentBGM.Stop();
                                    this.gameManager.gameLevel = GameManager.GameLevel.LEVEL2C;
                                    this.SetLevel(this.platforms2C, this.centipedeEnemies2C, this.centipedeEnemies2C.Count, this.centipedeIcon, this.deepBorder, this.deepJungleBG, this.bgmDeepBossInstance);
                                    this.SaveLevel();
                                    break;
                                case GameManager.GameLevel.LEVEL2C:
                                    this.sfxLevelCmptInstance.Play();
                                    this.gameManager.gameLevel = GameManager.GameLevel.COMPLETE;
                                    Game1.hasPaused = true;
                                    break;
                            }
                        }
                        if (this.hasCheckPointReached || this.hasGameLoaded)
                        {
                            if (this.delay >= 120)
                            {
                                this.hasCheckPointReached = false;
                                this.hasGameLoaded = false;
                                this.delay = 0;
                            }
                            this.delay++;
                        }
                        foreach (Enemy enemy in this.currentEnemies)
                        {
                            enemy.EnemyUpdate(this.player, this.sfxbloatAtkInstance, this.sfxbloatDeathInstance);
                        }
                    }
                    foreach (ListButtons listButtons2 in this.gameOverUIButtons)
                    {
                        if (listButtons2.InitialPos.Contains(Mouse.GetState().Position))
                        {
                            listButtons2.HoverButton();
                            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !Game1.hasPressed)
                            {
                                Game1.hasPressed = true;
                                string fontText = listButtons2.FontText;
                                if (!(fontText == " MAIN MENU"))
                                {
                                    if (fontText == "         QUIT")
                                    {
                                        this.sfxCancelInstance.Play();
                                        base.Exit();
                                    }
                                }
                                else
                                {
                                    this.sfxClickInstance.Play();
                                    this.gameManager.gameState = GameManager.GameStateEnum.MENU;
                                    Game1.hasPressed = false;
                                    this.currentBGM.Stop();
                                    this.Initialize();
                                }
                            }
                            else if (Mouse.GetState().LeftButton == ButtonState.Released)
                            {
                                Game1.hasPressed = false;
                            }
                        }
                        else
                        {
                            listButtons2.NormalButton();
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !Game1.hasPressed && this.player.animState != Player.AnimState.Death && this.gameManager.gameLevel != GameManager.GameLevel.COMPLETE)
                    {
                        this.sfxClickInstance.Play();
                        Game1.hasPressed = true;
                        if (!Game1.hasPaused)
                        {
                            Game1.hasPaused = true;
                        }
                        else
                        {
                            Game1.hasPaused = false;
                        }
                    }
                    else if (Keyboard.GetState().IsKeyUp(Keys.Escape) && Game1.hasPressed)
                    {
                        Game1.hasPressed = false;
                    }
                    break;
                case GameManager.GameStateEnum.CONTINUE:
                    this.LoadLevel();
                    break;
                case GameManager.GameStateEnum.EXIT:
                    base.Exit();
                    break;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                this.sfxAreaClear.Play();
                foreach (Enemy enemy2 in this.currentEnemies)
                {
                    enemy2.CharLife = 1;
                }
            }
            base.Update(gameTime);
        }

        // Token: 0x06000020 RID: 32 RVA: 0x000055D8 File Offset: 0x000037D8
        protected override void Draw(GameTime gameTime)
        {
            base.GraphicsDevice.Clear(Color.Black);
            this.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, new Matrix?(this.camera));
            if (this.gameManager.gameState == GameManager.GameStateEnum.START)
            {
                this.spriteBatch.Draw(this.currentBG, new Rectangle(0, 0, 2304, 544), Color.White);
                foreach (Platform platform in this.currentPlatforms)
                {
                    this.spriteBatch.Draw(platform.PlatText, platform.PlatDisplay, new Rectangle?(platform.PlatSource), platform.PlatColor);
                    this.spriteBatch.Draw(platform.HitBoxText, platform.HitBoxDisplay, Color.Transparent);
                }
                foreach (Enemy enemy in this.currentEnemies)
                {
                    enemy.EnemyDraw(this.spriteBatch);
                }
                this.player.PlayerDraw(this.spriteBatch);
                this.spriteBatch.Draw(this.currentBorder.PlatText, this.currentBorder.PlatDisplay, this.currentBorder.PlatColor);
                this.spriteBatch.Draw(this.currentBorder.HitBoxText, this.currentBorder.HitBoxDisplay, Color.Transparent);
                for (int i = 0; i < this.player.PlayerLife; i++)
                {
                    this.spriteBatch.Draw(this.heart[i].UIText, this.heart[i].UIDisplay, this.heart[i].UIColor);
                }
                this.spriteBatch.Draw(this.objIcon.UIText, this.objIcon.UIDisplay, this.objIcon.UIColor);
                this.spriteBatch.Draw(this.currentEnemyIcon.UIText, this.currentEnemyIcon.UIDisplay, this.currentEnemyIcon.UIColor);
                string text;
                if (this.isAreaClear)
                {
                    text = "Area cleared!!!";
                }
                else
                {
                    text = "Cleanse the area \nof ravaging beasts.  X " + Game1.currentEnemyCount.ToString();
                }
                this.spriteBatch.DrawString(this.gameFont, text, this.objIcon.FontPos, this.objIcon.FontColor);
                if (this.hasCheckPointReached)
                {
                    this.spriteBatch.DrawString(this.gameFont, "Checkpoint reached...", new Vector2(10f, (float)(base.Window.ClientBounds.Height - 50)), Color.White);
                }
                else if (this.hasGameLoaded)
                {
                    this.spriteBatch.DrawString(this.gameFont, "Game successfully loaded...", new Vector2(10f, (float)(base.Window.ClientBounds.Height - 50)), Color.White);
                }
                if (Game1.hasPaused)
                {
                    if (this.gameManager.gameLevel != GameManager.GameLevel.COMPLETE)
                    {
                        this.spriteBatch.Draw(this.pauseUI.UIText, this.pauseUI.UIDisplay, this.pauseUI.UIColor);
                    }
                    foreach (ListButtons listButtons in this.gameOverUIButtons)
                    {
                        this.spriteBatch.Draw(listButtons.ButtonText, listButtons.ButtonRect, listButtons.ButtonColor);
                        this.spriteBatch.DrawString(this.gameFont, listButtons.FontText, listButtons.FontPos, listButtons.FontColor);
                    }
                    if (this.player.animState == Player.AnimState.Death)
                    {
                        this.spriteBatch.DrawString(this.gameFont, "GAME OVER", this.pauseUI.FontPos, this.pauseUI.FontColor);
                    }
                    else if (this.gameManager.gameLevel == GameManager.GameLevel.COMPLETE)
                    {
                        this.spriteBatch.Draw(this.pauseUI.UIText, new Rectangle(base.Window.ClientBounds.Width / 2 - 200, base.Window.ClientBounds.Height / 2 - 70, 391, 144), this.pauseUI.UIColor);
                        this.spriteBatch.DrawString(this.gameFont, "YOU ESCAPED THE DEEP FOREST!!\n      THANK YOU FOR PLAYING!!", new Vector2((float)(base.Window.ClientBounds.Width / 2 - 168), (float)(base.Window.ClientBounds.Height / 2 - 25)), Color.Black);
                    }
                    else
                    {
                        this.spriteBatch.DrawString(this.gameFont, "   PAUSED", this.pauseUI.FontPos, this.pauseUI.FontColor);
                    }
                }
            }
            else
            {
                this.mainMenuUI.Draw(this.spriteBatch, this.buttonsUI, this.gameFont, this.title, this.boardUI, this.controls);
            }
            this.spriteBatch.End();
            base.Draw(gameTime);
        }

        // Token: 0x06000021 RID: 33 RVA: 0x00005B30 File Offset: 0x00003D30
        private void SetLevel(List<Platform> platforms, List<Enemy> enemies, int enemyCount, Texture2D enemyIcon, Texture2D bgBorder, Texture2D backGround, SoundEffectInstance bgm)
        {
            this.currentBG = backGround;
            this.currentPlatforms = platforms;
            this.currentEnemies = enemies;
            Game1.currentEnemyCount = enemyCount;
            this.currentEnemyIcon.UIText = enemyIcon;
            this.currentBorder.PlatText = bgBorder;
            this.currentBGM = bgm;
        }

        // Token: 0x06000022 RID: 34 RVA: 0x00005B70 File Offset: 0x00003D70
        private void SaveLevel()
        {
            this.sfxCheckPInst.Play();
            this.player.CheckPoint = new Point(this.player.CheckPoint.X, this.player.PlayerDisplay.Y - 100);
            this.hasCheckPointReached = true;
            this.saveSystem.SaveData(this.player.PlayerDisplay, this.player.CheckPoint, this.player.PlayerLife, this.gameManager.gameLevel.ToString());
        }

        // Token: 0x06000023 RID: 35 RVA: 0x00005C04 File Offset: 0x00003E04
        private void LoadLevel()
        {
            this.sfxCheckPInst.Play();
            PlayerData playerData = this.saveSystem.LoadData();
            this.player.PlayerDisplay = playerData.playerDisplay;
            this.player.CheckPoint = playerData.playerCheckPoint;
            this.player.PlayerLife = playerData.playerLife;
            this.gameManager.gameLevel = (GameManager.GameLevel)Enum.Parse(typeof(GameManager.GameLevel), playerData.level);
            this.hasGameLoaded = true;
            this.LoadAreaLevel();
            this.gameManager.gameState = GameManager.GameStateEnum.START;
        }

        // Token: 0x06000024 RID: 36 RVA: 0x00005C9C File Offset: 0x00003E9C
        private void LoadAreaLevel()
        {
            switch (this.gameManager.gameLevel)
            {
                case GameManager.GameLevel.LEVEL1A:
                    this.currentBG = this.jungleBG;
                    this.currentPlatforms = this.platforms1A;
                    this.currentEnemies = this.bloatEnemies1A;
                    Game1.currentEnemyCount = this.bloatEnemies1A.Count;
                    this.currentEnemyIcon.UIText = this.bloatedIcon;
                    this.currentBorder.PlatText = this.jungleBorder;
                    this.currentBGM = this.bgmJungleInstance;
                    return;
                case GameManager.GameLevel.LEVEL1B:
                    this.currentBG = this.jungleBG;
                    this.currentPlatforms = this.platforms1B;
                    this.currentEnemies = this.bloatEnemies1B;
                    Game1.currentEnemyCount = this.bloatEnemies1B.Count;
                    this.currentEnemyIcon.UIText = this.bloatedIcon;
                    this.currentBorder.PlatText = this.jungleBorder;
                    this.currentBGM = this.bgmJungleInstance;
                    return;
                case GameManager.GameLevel.LEVEL1C:
                    this.currentBG = this.jungleBG;
                    this.currentPlatforms = this.platforms1C;
                    this.currentEnemies = this.bloatEnemies1C;
                    Game1.currentEnemyCount = this.bloatEnemies1C.Count;
                    this.currentEnemyIcon.UIText = this.bloatedIcon;
                    this.currentBorder.PlatText = this.jungleBorder;
                    this.currentBGM = this.bgmJungleBossInstance;
                    return;
                case GameManager.GameLevel.LEVEL2A:
                    this.currentBG = this.deepJungleBG;
                    this.currentPlatforms = this.platforms2A;
                    this.currentEnemies = this.centipedeEnemies2A;
                    Game1.currentEnemyCount = this.centipedeEnemies2A.Count;
                    this.currentEnemyIcon.UIText = this.centipedeIcon;
                    this.currentBorder.PlatText = this.deepBorder;
                    this.currentBGM = this.bgmDeepJungleInstance;
                    return;
                case GameManager.GameLevel.LEVEL2B:
                    this.currentBG = this.deepJungleBG;
                    this.currentPlatforms = this.platforms2B;
                    this.currentEnemies = this.centipedeEnemies2B;
                    Game1.currentEnemyCount = this.centipedeEnemies2B.Count;
                    this.currentEnemyIcon.UIText = this.centipedeIcon;
                    this.currentBorder.PlatText = this.deepBorder;
                    this.currentBGM = this.bgmDeepJungleInstance;
                    return;
                case GameManager.GameLevel.LEVEL2C:
                    this.currentBG = this.deepJungleBG;
                    this.currentPlatforms = this.platforms2C;
                    this.currentEnemies = this.centipedeEnemies2C;
                    Game1.currentEnemyCount = this.centipedeEnemies2C.Count;
                    this.currentEnemyIcon.UIText = this.centipedeIcon;
                    this.currentBorder.PlatText = this.deepBorder;
                    this.currentBGM = this.bgmDeepBossInstance;
                    return;
                default:
                    return;
            }
        }

        // Token: 0x04000020 RID: 32
        private GraphicsDeviceManager graphics;

        // Token: 0x04000021 RID: 33
        private SpriteBatch spriteBatch;

        // Token: 0x04000022 RID: 34
        private GameManager gameManager;

        // Token: 0x04000023 RID: 35
        private Matrix camera;

        // Token: 0x04000024 RID: 36
        private SaveLoadSystem saveSystem;

        // Token: 0x04000025 RID: 37
        private Player player;

        // Token: 0x04000026 RID: 38
        private List<Enemy> bloatEnemies1A;

        // Token: 0x04000027 RID: 39
        private List<Enemy> bloatEnemies1B;

        // Token: 0x04000028 RID: 40
        private List<Enemy> bloatEnemies1C;

        // Token: 0x04000029 RID: 41
        private List<Enemy> centipedeEnemies2A;

        // Token: 0x0400002A RID: 42
        private List<Enemy> centipedeEnemies2B;

        // Token: 0x0400002B RID: 43
        private List<Enemy> centipedeEnemies2C;

        // Token: 0x0400002C RID: 44
        private List<Platform> currentPlatforms;

        // Token: 0x0400002D RID: 45
        private Platform currentBorder;

        // Token: 0x0400002E RID: 46
        private Texture2D currentBG;

        // Token: 0x0400002F RID: 47
        private List<Enemy> currentEnemies;

        // Token: 0x04000030 RID: 48
        private SoundEffectInstance currentBGM;

        // Token: 0x04000031 RID: 49
        public static int currentEnemyCount;

        // Token: 0x04000032 RID: 50
        private List<Platform> platforms1A;

        // Token: 0x04000033 RID: 51
        private List<Platform> platforms1B;

        // Token: 0x04000034 RID: 52
        private List<Platform> platforms1C;

        // Token: 0x04000035 RID: 53
        private List<Platform> platforms2A;

        // Token: 0x04000036 RID: 54
        private List<Platform> platforms2B;

        // Token: 0x04000037 RID: 55
        private List<Platform> platforms2C;

        // Token: 0x04000038 RID: 56
        private Texture2D jungleBG;

        // Token: 0x04000039 RID: 57
        private Texture2D jungleBorder;

        // Token: 0x0400003A RID: 58
        private Texture2D deepBorder;

        // Token: 0x0400003B RID: 59
        private Texture2D deepJungleBG;

        // Token: 0x0400003C RID: 60
        private SpriteFont gameFont;

        // Token: 0x0400003D RID: 61
        private MainMenu mainMenuUI;

        // Token: 0x0400003E RID: 62
        private List<ListButtons> buttonsUI;

        // Token: 0x0400003F RID: 63
        private Texture2D title;

        // Token: 0x04000040 RID: 64
        private Texture2D boardUI;

        // Token: 0x04000041 RID: 65
        private Texture2D controls;

        // Token: 0x04000042 RID: 66
        private List<GameHUD> heart;

        // Token: 0x04000043 RID: 67
        private GameHUD currentEnemyIcon;

        // Token: 0x04000044 RID: 68
        private GameHUDwText objIcon;

        // Token: 0x04000045 RID: 69
        private Texture2D bloatedIcon;

        // Token: 0x04000046 RID: 70
        private Texture2D centipedeIcon;

        // Token: 0x04000047 RID: 71
        private GameHUDwText pauseUI;

        // Token: 0x04000048 RID: 72
        private List<ListButtons> gameOverUIButtons;

        // Token: 0x04000049 RID: 73
        private SoundEffect bgmMenu;

        // Token: 0x0400004A RID: 74
        private SoundEffect sfxClick;

        // Token: 0x0400004B RID: 75
        private SoundEffect sfxCancel;

        // Token: 0x0400004C RID: 76
        private SoundEffect sfxAreaClear;

        // Token: 0x0400004D RID: 77
        private SoundEffect sfxCheckPoint;

        // Token: 0x0400004E RID: 78
        private SoundEffectInstance bgmMenuInstance;

        // Token: 0x0400004F RID: 79
        private SoundEffectInstance sfxClickInstance;

        // Token: 0x04000050 RID: 80
        private SoundEffectInstance sfxCancelInstance;

        // Token: 0x04000051 RID: 81
        private SoundEffectInstance sfxAreaClearInst;

        // Token: 0x04000052 RID: 82
        private SoundEffectInstance sfxCheckPInst;

        // Token: 0x04000053 RID: 83
        private SoundEffect bgmJungle;

        // Token: 0x04000054 RID: 84
        private SoundEffect bgmJungleBoss;

        // Token: 0x04000055 RID: 85
        private SoundEffect bgmDeepJungle;

        // Token: 0x04000056 RID: 86
        private SoundEffect bgmDeepBoss;

        // Token: 0x04000057 RID: 87
        private SoundEffect sfxLevelCmpt;

        // Token: 0x04000058 RID: 88
        private SoundEffectInstance bgmJungleInstance;

        // Token: 0x04000059 RID: 89
        private SoundEffectInstance bgmJungleBossInstance;

        // Token: 0x0400005A RID: 90
        private SoundEffectInstance bgmDeepJungleInstance;

        // Token: 0x0400005B RID: 91
        private SoundEffectInstance bgmDeepBossInstance;

        // Token: 0x0400005C RID: 92
        private SoundEffectInstance sfxLevelCmptInstance;

        // Token: 0x0400005D RID: 93
        private SoundEffect sfxWalk;

        // Token: 0x0400005E RID: 94
        private SoundEffect sfxSlash;

        // Token: 0x0400005F RID: 95
        private SoundEffect sfxRun;

        // Token: 0x04000060 RID: 96
        private SoundEffect sfxWhoosh;

        // Token: 0x04000061 RID: 97
        private SoundEffect sfxJump;

        // Token: 0x04000062 RID: 98
        private SoundEffect sfxHit;

        // Token: 0x04000063 RID: 99
        private SoundEffect sfxGameOver;

        // Token: 0x04000064 RID: 100
        private SoundEffectInstance sfxWalkInstance;

        // Token: 0x04000065 RID: 101
        private SoundEffectInstance sfxSlashInstance;

        // Token: 0x04000066 RID: 102
        private SoundEffectInstance sfxRunInstance;

        // Token: 0x04000067 RID: 103
        private SoundEffectInstance sfxWhooshInstance;

        // Token: 0x04000068 RID: 104
        private SoundEffectInstance sfxJumpInstance;

        // Token: 0x04000069 RID: 105
        private SoundEffectInstance sfxHitInstance;

        // Token: 0x0400006A RID: 106
        private SoundEffectInstance sfxGameOverInstance;

        // Token: 0x0400006B RID: 107
        private SoundEffect sfxbloatAtk;

        // Token: 0x0400006C RID: 108
        private SoundEffect sfxbloatDeath;

        // Token: 0x0400006D RID: 109
        private SoundEffectInstance sfxbloatAtkInstance;

        // Token: 0x0400006E RID: 110
        private SoundEffectInstance sfxbloatDeathInstance;

        // Token: 0x0400006F RID: 111
        public static bool hasPaused;

        // Token: 0x04000070 RID: 112
        public static bool hasPressed;

        // Token: 0x04000071 RID: 113
        private bool hasCheckPointReached;

        // Token: 0x04000072 RID: 114
        private bool hasGameLoaded;

        // Token: 0x04000073 RID: 115
        private bool isAreaClear;

        // Token: 0x04000074 RID: 116
        private int delay;

        // Token: 0x04000075 RID: 117
        private float cameraPosX;
    }
}
