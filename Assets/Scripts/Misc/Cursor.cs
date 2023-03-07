using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    #region Private Vars
    private bool p_grabbing = false;
    #endregion

    #region Cached Components
    private Camera cc_mainCam;
    private BasicItem cc_grabbedItem;
    #endregion

    #region Init
    private void Start() {
        cc_mainCam = Camera.main;
    }
    #endregion

    #region Updates
    void Update()
    {
        if (p_grabbing && Input.GetMouseButtonUp(0)) {
            p_grabbing = false;
            cc_grabbedItem.setGrabbed(false);
        } else if (p_grabbing) {
            if (cc_grabbedItem == null) {
                p_grabbing = false;
            } else {
                cc_grabbedItem.Move(cc_mainCam.ScreenPointToRay(Input.mousePosition).origin);
            }
        } else if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(cc_mainCam.ScreenPointToRay(Input.mousePosition).origin, new Vector2(0, 0));
            if (hit.collider != null && hit.collider.CompareTag("Item")) {
                p_grabbing = true;
                cc_grabbedItem = hit.collider.gameObject.GetComponent<BasicItem>();
                cc_grabbedItem.setGrabbed(true);
            }
        }

    }
    #endregion
}
