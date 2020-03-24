﻿using System;

namespace Assets.Scripts.GameData
{
    [Serializable]
    public struct KeyValuePair<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
    }
}