using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtomScript : MonoBehaviour
{
    public GameObject obj;
    private Vector3 finish;
    private float journeyLength;
    private float startTime = -1;

    // Start is called before the first frame update
    void Start()
    {
        finish = transform.position - new Vector3(0, 0.2f, 0);
        journeyLength = Vector3.Distance(transform.position, finish);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = 0.0f;
        if (startTime >= 0)
        {
            distCovered = (Time.time - startTime) * 0.1f;
            float fractionOfJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(
                transform.position,
                finish,
                distCovered
            );

            if (obj.tag == "Door")
            {
                Debug.Log(obj.tag);
                DoorScript script = obj.GetComponent<DoorScript>();
                script.isActive = true;
            }
        }
    }
}
