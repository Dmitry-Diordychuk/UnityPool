using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Club : MonoBehaviour
{
    public Text text;
    private int currentScore;
    public float hitForce;
    public float maxHitForce;
    public float rotationSpeed;
    public GameObject ball;
    private float currentHitForce;

    void hit()
    {
        Ball ballScript = ball.GetComponent<Ball>();
        ballScript.hit(currentHitForce, gameObject.transform.rotation);
        currentScore += 5;
        text.text = currentScore.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHitForce = 0.0f;
        currentScore = -15;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (ball) {
            gameObject.transform.position = new Vector3(
                ball.transform.position.x,
                ball.transform.position.y,
                0
            );

            if (!ball.GetComponent<Ball>().checkForce())
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    gameObject.transform.rotation = Quaternion.AngleAxis(rotationSpeed, Vector3.forward) * transform.rotation;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    gameObject.transform.rotation = Quaternion.AngleAxis(-rotationSpeed, Vector3.forward) * transform.rotation;
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    if (currentHitForce < maxHitForce)
                        currentHitForce += hitForce * Time.deltaTime;
                    else
                        currentHitForce = maxHitForce;
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    hit();
                    currentHitForce = 0.0f;
                }
            }
        }
    }
}
