using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject start;
    public GameObject finish;
    public GameObject bird;
    private Bird birdScript;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        birdScript = bird.GetComponent<Bird>();
        speed = Time.time / 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (!birdScript.isGameOver)
        {
            transform.position -= new Vector3(speed, 0, 0);

            if (transform.position.x < finish.transform.position.x)
                transform.position = new Vector3(start.transform.position.x, transform.position.y, transform.position.z);
            speed = Time.time / 1000;
        }
    }
}
