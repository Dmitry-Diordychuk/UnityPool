using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public GameObject connectedTeleport;

    private static bool isTeleported;

    private void Start() {
        isTeleported = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!isTeleported)
        {
            List<GameObject> childs = new List<GameObject>();
            foreach (Transform childTransform in other.transform)
            {
                childs.Add(childTransform.gameObject);
            }

            foreach (GameObject child in childs)
            {
                child.transform.parent = null;
            }

            other.transform.position = connectedTeleport.transform.position;
            isTeleported = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        isTeleported = false;
    }
}
