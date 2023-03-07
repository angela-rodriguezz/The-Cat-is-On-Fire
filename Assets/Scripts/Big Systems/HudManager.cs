using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudManager : MonoBehaviour
{
    #region Private Variables
    private float p_currentTime = 0f;
    #endregion

    #region Editor Variable
    [SerializeField]
    private TMP_Text p_countdownText;
    [SerializeField]
    private TMP_Text p_score;
    [SerializeField]
    private float p_startingTime;
    #endregion

    // Start is called before the first frame update
    void Start() {
        p_currentTime = p_startingTime;
    }

    // Update is called once per frame
    void Update() {
        p_currentTime += 1 * Time.deltaTime;
        string mins = ((int)p_currentTime / 60 / 10).ToString("0") + ((int)p_currentTime / 60 % 10).ToString("0");
        string secs = ((int)(p_currentTime % 60) / 10).ToString("0") + ((int)(p_currentTime % 60) % 10).ToString("0");

        p_countdownText.text = mins + ":" + secs;


        if (p_currentTime <= 0) {
            p_currentTime = 0;
        }
    }

    public void UpdateScore(float val) {
        p_score.text = "Score: " + val.ToString("0");
    }
}
