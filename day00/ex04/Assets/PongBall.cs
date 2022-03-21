using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PongBall : MonoBehaviour
{
    public GameObject playerOneScore;
    public GameObject playerTwoScore;
    private float currentForce;
    private Quaternion currentDirection;
    public List<GameObject> walls;
    public void hit(float hitForce, Quaternion direction)
    {
        currentForce = hitForce;
        currentDirection = direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentForce = 5.0f;
        currentDirection = Quaternion.AngleAxis(45.0f, Vector3.forward);
    }


    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x > 7.5f)
        {
            int currentScore = Convert.ToInt32(playerOneScore.GetComponent<Text>().text);
            playerOneScore.GetComponent<Text>().text = (++currentScore).ToString();
            transform.position = new Vector3(0, 0, 0);
        }
        else if (gameObject.transform.position.x < -7.5f)
        {
            int currentScore = Convert.ToInt32(playerTwoScore.GetComponent<Text>().text);
            playerTwoScore.GetComponent<Text>().text = (++currentScore).ToString();
            transform.position = new Vector3(0, 0, 0);
        }

        if (currentForce > 0.0f)
        {
            foreach (GameObject wall in walls) {
                float wallHalfScaleX = wall.transform.localScale.x / 2;
                float wallHalfScaleY = wall.transform.localScale.y / 2;
                float wallMinX = wall.transform.position.x - wallHalfScaleX;
                float wallMaxX = wall.transform.position.x + wallHalfScaleX;
                float wallMinY = wall.transform.position.y - wallHalfScaleY;
                float wallMaxY = wall.transform.position.y + wallHalfScaleY;

                float ballHalfScaleX = transform.localScale.x / 2;
                float ballHalfScaleY = transform.localScale.y / 2;
                float ballMinX = transform.position.x - ballHalfScaleX;
                float ballMaxX = transform.position.x + ballHalfScaleX;
                float ballMinY = transform.position.y - ballHalfScaleY;
                float ballMaxY = transform.position.y + ballHalfScaleY;

                bool collisionX = ballMaxX >= wallMinX && wallMaxX >= ballMinX;

                bool collisionY = ballMaxY >= wallMinY && wallMaxY >= ballMinY;

                if (collisionX && collisionY) {
                    float deltaXMin = Mathf.Abs(ballMaxX - wallMinX);
                    float deltaXMax = Mathf.Abs(ballMinX - wallMaxX);
                    float deltaYMin = Mathf.Abs(ballMaxY - wallMinY);
                    float deltaYMax = Mathf.Abs(ballMinY - wallMaxY);

                    float gap = 0.1f;
                    if (Mathf.Min(deltaYMin, deltaYMax) < Mathf.Min(deltaXMin, deltaXMax)) {
                        currentDirection = Quaternion.Inverse(currentDirection);

                        if (deltaYMin < deltaYMax) {
                            transform.position -= new Vector3(0, deltaYMin + gap, 0);
                        } else {
                            transform.position += new Vector3(0, deltaYMax + gap, 0);
                        }


                    } else {
                        currentDirection = Quaternion.AngleAxis(180, Vector3.forward) * Quaternion.Inverse(currentDirection);

                        if (deltaXMin < deltaXMax) {
                            transform.position -= new Vector3(deltaXMin + gap, 0, 0);
                        } else {
                            transform.position += new Vector3(deltaXMax + gap, 0, 0);
                        }
                    }
                }
            }

            float angle = currentDirection.eulerAngles.z;

            gameObject.transform.position += new Vector3(
                -currentForce * Time.deltaTime * Mathf.Cos(Mathf.Deg2Rad * angle),
                -currentForce * Time.deltaTime * Mathf.Sin(Mathf.Deg2Rad * angle),
                0
            );
        }
    }
}
