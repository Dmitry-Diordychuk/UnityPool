using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public float speed;
    public GameObject finish;

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        if (transform.position.y < finish.transform.position.y - 1)
            GameObject.Destroy(gameObject);
    }
}
