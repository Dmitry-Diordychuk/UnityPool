using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject gameOverText;
    private float currentForce;
    private Quaternion currentDirection;
    public List<GameObject> walls;
    public GameObject hole;
    public void hit(float hitForce, Quaternion direction)
    {
        currentForce = hitForce;
        currentDirection = direction;
    }

    public bool checkForce()
    {
        return currentForce > 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void move()
    {
        float angle = currentDirection.eulerAngles.z;

        gameObject.transform.position += new Vector3(
            -currentForce * Time.deltaTime * Mathf.Cos(Mathf.Deg2Rad * angle),
            -currentForce * Time.deltaTime * Mathf.Sin(Mathf.Deg2Rad * angle),
            0
        );
    }

    // Update is called once per frame
    void Update()
    {
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

            float distance = Mathf.Sqrt(
                Mathf.Pow(hole.transform.position.x - transform.position.x, 2) +
                Mathf.Pow(hole.transform.position.y - transform.position.y, 2)
            );
            if (distance < hole.transform.localScale.x / 2 && currentForce < 1.0f) {
                gameOverText.SetActive(true);
                GameObject.Destroy(gameObject);
            }

            currentForce -= Time.deltaTime;
        }
    }
}
