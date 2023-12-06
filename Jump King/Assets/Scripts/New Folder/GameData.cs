using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public Dictionary<string, bool> itemsCollected;

    //public SerializableDictionary<string, bool> itemsCollected;

    // the values defined in this constructor will be default values
    // the game starts with when there's no data to load
    public GameData()
    {
        playerPosition = new Vector3(36.64f, 2.39f, -1.74f);
    }
}
