using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSpawnInfo
{

    #region Editor Vars
    [SerializeField]
    private float p_firstSeen;

    public float FirstSeen {
        get {
            return p_firstSeen;
        }
    }

    [SerializeField]
    private float p_lastSeen;

    public float LastSeen {
        get {
            return p_lastSeen;
        }
    }

    [SerializeField]
    private float p_chance;

    public float Chance {
        get {
            if (p_spawned < p_numToSpawn)
                return p_actualChance;
            else
                return 0;
        }
        set {
            p_actualChance = value;
        }
    }

    public float OGchance {
        get {
            return p_chance;
        }
    }

    [SerializeField]
    private GameObject p_GO;

    public GameObject GO {
        get {
            return p_GO;
        }
    }

    [SerializeField]
    private int p_numToSpawn;
    #endregion

    #region private vars
    private int p_spawned = 0;
    private float p_actualChance;

    public void incNumSpawned() {
        p_spawned += 1;
    }

    public void RefreshNumSpawned() {
        p_spawned = p_spawned - p_numToSpawn;
    }

    [SerializeField]
    private float p_intervalChange = 0;
    public float intervalChange {
        get {
            return p_intervalChange;
        }
    }

    #endregion

    public int CompareTo(ItemSpawnInfo other) {
        if (other.Chance == p_chance) {
            return 0;
        } else if (other.Chance < p_chance) {
            return 1;
        } else if (other.Chance > p_chance) {
            return -1;
        }
        throw new System.Exception("Comparing fucked up");
    }
}