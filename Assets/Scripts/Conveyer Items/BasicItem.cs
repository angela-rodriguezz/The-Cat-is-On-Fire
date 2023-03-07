using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicItem : MonoBehaviour
{
    #region Editor Vars
    [SerializeField]
    private float p_mass = 1;

    [SerializeField]
    private string p_type;

    public string Type {
        get {
            return p_type;
        }
    }

    [SerializeField]
    private float p_pts;

    public float Points {
        get {
            return p_pts;
        }
    }

    [SerializeField]
    private float p_negPts;

    public float NegativePoints {
        get {
            return p_negPts;
        }
    }

    [SerializeField]
    private int[] p_afterSpawns;

    public int[] AfterSpawns {
        get {
            return p_afterSpawns;
        }
    }

    [SerializeField]
    private string[] p_BoxTypesAffected;

    [SerializeField]
    public float p_intervalChange=0;
    #endregion

    #region Cached Components
    private Rigidbody2D cc_rb;
    private bool p_is_grabbed;
    #endregion

    #region Init
    private void Awake() {
        cc_rb = GetComponent<Rigidbody2D>();
        cc_rb.mass = p_mass;
    }

    private void Start() {
        BoxCollider2D coll = GetComponent<BoxCollider2D>();
        float new_size_y = coll.size.y * (Random.Range(80, 105) / (float)100);
        float new_offset_y = coll.offset.y + (coll.size.y - new_size_y) / 2;

        coll.size = new Vector2(coll.size.x, new_size_y);
        coll.offset = new Vector2(coll.offset.x, new_offset_y);
    }
    #endregion

    #region Moving Funcs
    public void ToConveyorVelocity(float amnt, Vector2 dir) {
        if (cc_rb.velocity.magnitude < amnt/p_mass) {
            cc_rb.velocity = dir * (amnt / p_mass);
        }
    }

    public void setGrabbed(bool grabbed) {
        p_is_grabbed = grabbed;
        if (p_is_grabbed) {
            gameObject.layer = 10;
        } else {
            gameObject.layer = 9;
        }
    }

    public bool isGrabbed() {
        return p_is_grabbed;
    }

    public void Move(Vector2 pos) {
        cc_rb.velocity = Vector2.zero;
        cc_rb.position = pos;
    }
    #endregion
}
