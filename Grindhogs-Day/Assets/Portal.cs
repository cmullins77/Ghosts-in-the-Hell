using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    public Portal connectingPortal;
    public float x;
    public float y;

    void OnTriggerEnter2D(Collider2D collision) {
        GameObject player = collision.gameObject;
        player.transform.position = new Vector3(connectingPortal.x, connectingPortal.y, player.transform.position.z);
    }
}
