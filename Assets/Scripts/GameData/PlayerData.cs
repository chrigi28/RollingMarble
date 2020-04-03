using System;
using System.Collections.Generic;
using UnityEditor;

namespace Assets.Scripts.GameData
{
    [Serializable]
    public class PlayerData
    {
        public List<LevelData> LevelDatas = new List<LevelData>();

        public void AddLevelData(int level, float time, byte stars)
        {
            LevelDatas.Add(new LevelData() {levelNumber = level, stars = stars, time = time});
            SaveData(this);
        }

        private static void SaveData(PlayerData data)
        {
            GameDataManager.SavePlayerData(data);
        }
    }
}