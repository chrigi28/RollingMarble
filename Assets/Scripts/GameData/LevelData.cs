using System;
using System.Collections.Generic;

namespace Assets.Scripts.GameData
{
    [Serializable]
    public class LevelData
    {
        public int levelNumber;
        public bool generated;
        public int? seed;
        public int numberOfObstacles;
        public List<KeyValuePair<Type, int>> obstacleDictionary;
        public float timeForOneStar;
        public float timeForTwoStar;
        public float timeForThreeStar;
        
        // user specific might be extracted to own class
        public bool levelCompleted;
        public float bestTime;
    }
}