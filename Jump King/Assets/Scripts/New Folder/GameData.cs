using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

[System.Serializable]
public class GameData
{
    public float totalTimePlayed;
    public Vector3 playerPosition;
    public Dictionary<string, bool> itemsCollected;

    public int ID { get; set; }

    //public SerializableDictionary<string, bool> itemsCollected;

    // the values defined in this constructor will be default values
    // the game starts with when there's no data to load
    public GameData()
    {
        totalTimePlayed = 0;
        playerPosition = new Vector3(36.64f, 2.39f, -1.74f);
    }
    public float GetPosition()
    {
        return playerPosition.x;
    }

    public float GetTimePlayed()
    {
        return totalTimePlayed;
    }
}
