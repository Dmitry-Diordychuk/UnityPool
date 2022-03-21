using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(up) && transform.position.y < 3)
        {
            transform.Translate(Vector3.up * 1.0f);
        }
        else if (Input.GetKeyDown(down) && transform.position.y > -3)
        {
            transform.Translate(Vector3.down * 1.0f);
        }
    }
}
