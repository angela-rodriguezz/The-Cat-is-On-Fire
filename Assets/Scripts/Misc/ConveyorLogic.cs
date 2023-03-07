using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorLogic : MonoBehaviour
{
    #region Editor vars
    [SerializeField]
    private float p_velocity;
    #endregion

    #region
    private Vector2 p_pushdir;
    #endregion

    #region
    private void Awake() {
        Transform tr = transform;
        float angleRAD = Mathf.Deg2Rad * tr.rotation.eulerAngles.z;
        p_pushdir = new Vector2(Mathf.Cos(angleRAD), Mathf.Sin(angleRAD));

        GetComponent<Animator>().SetFloat("Speed", Mathf.Max(p_velocity / 3, 1));
    }
    #endregion

    #region Collisions
    private void OnCollisionStay2D(Collision2D collision) {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Item")) {
            other.GetComponent<BasicItem>().ToConveyorVelocity(p_velocity, p_pushdir);
        }
    }
    #endregion
}
