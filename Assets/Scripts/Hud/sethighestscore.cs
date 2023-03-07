using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sethighestscore : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI txtfield;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Highscore")) {
            txtfield.text = "High score: " + PlayerPrefs.GetFloat("Highscore");
        }
    }
}
