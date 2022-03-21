using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isActive = false;
    private Vector3 finish;

    private float startTime = -1.0f;
    // Start is called before the first frame update
    void Start()
    {
        finish = transform.position + new Vector3(0, 3.0f, 0);
    }

    public void Open()
    {
        isActive = true;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                finish,
                (Time.time - startTime) * 0.01f
            );
        }
    }
}
