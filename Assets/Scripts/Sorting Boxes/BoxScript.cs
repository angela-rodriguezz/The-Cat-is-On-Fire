using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoxScript : MonoBehaviour
{
    #region Editor Vars
    [SerializeField]
    private string[] AcceptanceTypes;

    private string p_boxType;
    #endregion

    #region Cached Components
    private LevelManager cc_LevelManager;

    #endregion


    #region Init
    private void Start() {
        cc_LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    #endregion


    #region Collisiom
    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Item")) {
            BasicItem item = other.GetComponent<BasicItem>();
            if (AcceptanceTypes.Contains(item.Type)) {
                cc_LevelManager.IncreasePoints(item, p_boxType);
            } else if (item.Type == "instakill") {
                cc_LevelManager.LoseGame();
            } else {
                cc_LevelManager.DecreasePoints(item, p_boxType);
            }     
            Destroy(other);
        }
    }
    #endregion
}
