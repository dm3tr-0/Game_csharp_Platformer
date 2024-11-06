using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_csharp_Platformer
{
    // Token: 0x02000009 RID: 9
    internal class MainMenu
    {
        // Token: 0x0600003B RID: 59 RVA: 0x00006181 File Offset: 0x00004381
        public MainMenu(Texture2D menuBGText, Rectangle menuBGDis, Color menuBGCol)
        {
            this.menuBGText = menuBGText;
            this.menuBGDis = menuBGDis;
            this.menuBGCol = menuBGCol;
            this.checkMenu = true;
            this.count = 0;
            this.buttonCount = 4;
        }

        // Token: 0x0600003C RID: 60 RVA: 0x000061B4 File Offset: 0x000043B4
        public void Update(List<ListButtons> buttonsUI, GameManager gameManager, SoundEffectInstance bgmMenuInstance, SoundEffectInstance sfxClick, SoundEffectInstance sfxCancel)
        {
            bgmMenuInstance.Play();
            for (int i = 0; i < 4; i++)
            {
                if (buttonsUI[i].ButtonRect.Contains(Mouse.GetState().Position) && this.checkMenu)
                {
                    buttonsUI[i].HoverButton();
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && !Game1.hasPressed)
                    {
                        Game1.hasPressed = true;
                        string fontText = buttonsUI[i].FontText;
                        if (!(fontText == "       START"))
                        {
                            if (!(fontText == "   CONTINUE"))
                            {
                                if (!(fontText == "   CONTROLS"))
                                {
                                    if (fontText == "        EXIT")
                                    {
                                        sfxCancel.Play();
                                        gameManager.gameState = GameManager.GameStateEnum.EXIT;
                                    }
                                }
                                else
                                {
                                    sfxClick.Play();
                                    this.checkMenu = false;
                                    this.checkOption = true;
                                }
                            }
                            else
                            {
                                sfxClick.Play();
                                gameManager.gameState = GameManager.GameStateEnum.CONTINUE;
                                Game1.hasPressed = false;
                                bgmMenuInstance.Stop();
                            }
                        }
                        else
                        {
                            sfxClick.Play();
                            gameManager.gameState = GameManager.GameStateEnum.START;
                            Game1.hasPressed = false;
                            bgmMenuInstance.Stop();
                        }
                    }
                    else if (Mouse.GetState().LeftButton == ButtonState.Released)
                    {
                        Game1.hasPressed = false;
                    }
                }
                else
                {
                    buttonsUI[i].NormalButton();
                }
            }
            if (buttonsUI[4].ButtonRect.Contains(Mouse.GetState().Position) && this.checkOption)
            {
                buttonsUI[4].HoverButton();
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && !Game1.hasPressed && buttonsUI[4].FontText == "        BACK")
                {
                    Game1.hasPressed = true;
                    sfxCancel.Play();
                    this.checkMenu = true;
                    this.checkOption = false;
                }
                else if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    Game1.hasPressed = false;
                }
            }
            else
            {
                buttonsUI[4].NormalButton();
            }
            if (this.checkMenu)
            {
                this.count = 0;
                this.buttonCount = 4;
            }
            if (this.checkOption)
            {
                this.count = 4;
                this.buttonCount = buttonsUI.Count;
            }
        }

        // Token: 0x0600003D RID: 61 RVA: 0x000063D8 File Offset: 0x000045D8
        public void Draw(SpriteBatch spriteBatch, List<ListButtons> buttonsUI, SpriteFont gameFont, Texture2D title, Texture2D boardUI, Texture2D controls)
        {
            spriteBatch.Draw(this.menuBGText, this.menuBGDis, this.menuBGCol);
            if (this.checkOption)
            {
                spriteBatch.Draw(boardUI, new Rectangle(39, 83, 721, 317), Color.White);
                spriteBatch.Draw(controls, new Rectangle(80, 126, 640, 225), Color.White);
            }
            else
            {
                spriteBatch.Draw(title, new Rectangle(150, 30, 500, 161), Color.White);
            }
            while (this.count < this.buttonCount)
            {
                spriteBatch.Draw(buttonsUI[this.count].ButtonText, buttonsUI[this.count].ButtonRect, buttonsUI[this.count].ButtonColor);
                spriteBatch.DrawString(gameFont, buttonsUI[this.count].FontText, buttonsUI[this.count].FontPos, buttonsUI[this.count].FontColor);
                this.count++;
            }
        }

        // Token: 0x0400008D RID: 141
        private Texture2D menuBGText;

        // Token: 0x0400008E RID: 142
        private Rectangle menuBGDis;

        // Token: 0x0400008F RID: 143
        private Color menuBGCol;

        // Token: 0x04000090 RID: 144
        private bool checkMenu;

        // Token: 0x04000091 RID: 145
        private bool checkOption;

        // Token: 0x04000092 RID: 146
        private int count;

        // Token: 0x04000093 RID: 147
        private int buttonCount;
    }
}
