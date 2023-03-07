using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region editor Vars    
    [SerializeField]
    private float m_levelDuration;

    [SerializeField]
    private float[] StarReqs = new float[3];
    #endregion

    #region Private vars
    private float p_curScore;
    private float p_curTime;
    #endregion

    #region Cached Components
    List<SpawnManager> cc_SpawnManagers = new List<SpawnManager>();
    HudManager cc_hud;
    #endregion

    #region Init
    private void Start() {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spwner in spawners) {
            cc_SpawnManagers.Add(spwner.GetComponent<SpawnManager>());
        }
        cc_hud = GetComponent<HudManager>();
    }

    internal void LoseGame() {
        if (!PlayerPrefs.HasKey("Highscore") || p_curScore > PlayerPrefs.GetFloat("Highscore", p_curScore)) {
            Debug.Log("set");
            PlayerPrefs.SetFloat("Highscore", p_curScore);
        }
        GameManager.Instance.MainMenu();
    }
    #endregion

    #region PtsWork
    public void IncreasePoints(BasicItem item, string boxType) {
        p_curScore += item.Points;

        foreach (int itemToSpawn in item.AfterSpawns) {
            foreach (SpawnManager spawner in cc_SpawnManagers) {
                spawner.AddAfterSpawn(itemToSpawn);
            }
        }
        cc_hud.UpdateScore(p_curScore);
    }

    public void DecreasePoints(BasicItem item, string boxType) {
        p_curScore += item.NegativePoints;
        cc_hud.UpdateScore(p_curScore);
        if (p_curScore < 0) {
            LoseGame();
        }
    }
    #endregion

    #region Updates

    #endregion
}
