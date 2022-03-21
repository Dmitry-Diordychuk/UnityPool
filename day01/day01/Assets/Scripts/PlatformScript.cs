using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public float speed;



    // Update is called once per frame
    void Update()
    {
        if (pointA && pointB)
        {
            gameObject.transform.position = Vector3.Lerp(
                pointA.transform.position,
                pointB.transform.position,
                Mathf.PingPong(Time.time * speed, 1)
            );
            //Debug.Log(Mathf.Abs(Mathf.Sin(Time.time)));
        }
    }
}
