using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Game_csharp_Platformer
{
    // Token: 0x0200000E RID: 14
    internal class SaveLoadSystem
    {
        // Token: 0x06000063 RID: 99 RVA: 0x00006F28 File Offset: 0x00005128
        public void SaveData(Rectangle playerDisplay, Point playerCheckPoint, int playerLife, string gameLevel)
        {
            PlayerData playerData = new PlayerData();
            playerData.playerDisplay = playerDisplay;
            playerData.playerCheckPoint = playerCheckPoint;
            playerData.playerLife = playerLife;
            playerData.level = gameLevel;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PlayerData));
            StreamWriter streamWriter = new StreamWriter("saveData.txt");
            xmlSerializer.Serialize(streamWriter, playerData);
            streamWriter.Close();
        }

        // Token: 0x06000064 RID: 100 RVA: 0x00006F84 File Offset: 0x00005184
        public PlayerData LoadData()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PlayerData));
            StreamReader streamReader = new StreamReader("saveData.txt");
            PlayerData result = (PlayerData)xmlSerializer.Deserialize(streamReader);
            streamReader.Close();
            return result;
        }
    }
}
