using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour {
    #region Editor vars
    [SerializeField]
    private ItemSpawnInfo[] itemsToSpawn;

    [SerializeField]
    private float spawnInterval;

    [SerializeField]
    private ItemSpawnInfo[] p_afterSpawns;

    [SerializeField]
    private float p_dualSpawnInterval;
    #endregion

    #region Private vars
    private List<ItemSpawnInfo> activeSpawns;

    private bool p_safeToAdd = true;

    private IEnumerator p_spawnProcess;

    private Vector3 p_spawnPos;
    #endregion


    #region Init
    private void Awake() {
        activeSpawns = new List<ItemSpawnInfo>();
        foreach (ItemSpawnInfo item in itemsToSpawn) {
            item.Chance = item.OGchance;
            StartCoroutine(AddRemoveItem(item));
        }
        p_spawnPos = transform.position;
    }
    
    private void makeActiveSpawnsFair() {
        float totalChance = 0;
        float curInterval = 0;
        foreach (ItemSpawnInfo item in activeSpawns) {
            if (item.Chance > 0)
                totalChance += item.OGchance;
        }
        
        foreach (ItemSpawnInfo item in activeSpawns) {
            curInterval += Mathf.Min(item.OGchance / (float) totalChance, 1);
            item.Chance = curInterval;
        }

        activeSpawns.Sort(new ItemComparer());
    }

    private IEnumerator AddRemoveItem(ItemSpawnInfo item) {
        yield return new WaitForSeconds(item.FirstSeen);
        while (true) {
            if (p_safeToAdd) {
                activeSpawns.Add(item);
                break;
            }
            yield return false;
        }
        makeActiveSpawnsFair();
        yield return new WaitForSeconds(item.LastSeen - item.FirstSeen);
        activeSpawns.Remove(item);
        makeActiveSpawnsFair();
    }


    public void AddAfterSpawn(int index) {
        p_afterSpawns[index].RefreshNumSpawned();
        StartCoroutine(AddRemoveItem(p_afterSpawns[index]));
    }

    private void Start() {
        p_spawnProcess = SpawnLoop();
        StartCoroutine(p_spawnProcess);
    }

    private IEnumerator SpawnLoop() {
        while (true) {
            float val = Random.value;
            foreach (ItemSpawnInfo item in activeSpawns) {
                p_safeToAdd = false;
                if (val < item.Chance) {
                    Spawn(item);
                    break;
                }
            }
            p_safeToAdd = true;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Spawn(ItemSpawnInfo item) {
        Instantiate(item.GO, p_spawnPos, Quaternion.identity);
        spawnInterval = Mathf.Max(spawnInterval - item.intervalChange, (float)0.7);
        item.incNumSpawned();
    }
    #endregion
}


public class ItemComparer : IComparer<ItemSpawnInfo> {
    public int Compare(ItemSpawnInfo x, ItemSpawnInfo y) {
        return x.CompareTo(y);
    }
}