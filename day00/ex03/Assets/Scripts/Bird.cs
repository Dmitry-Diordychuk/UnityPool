using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    public GameObject gameOverText;
    public List<GameObject> objects;
    public float jumpHight;
    public float gConst;
    private float v;
    public bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        v = 0.0f;
        isGameOver = false;
    }

    bool IsCollided(GameObject a, GameObject b)
    {
        float aHalfScaleX = a.transform.localScale.x / 2;
        float aHalfScaleY = a.transform.localScale.y / 2;
        float aMinX = a.transform.position.x - aHalfScaleX;
        float aMaxX = a.transform.position.x + aHalfScaleX;
        float aMinY = a.transform.position.y - aHalfScaleY;
        float aMaxY = a.transform.position.y + aHalfScaleY;

        float bHalfScaleX = b.transform.localScale.x / 2;
        float bHalfScaleY = b.transform.localScale.y / 2;
        float bMinX = b.transform.position.x - bHalfScaleX;
        float bMaxX = b.transform.position.x + bHalfScaleX;
        float bMinY = b.transform.position.y - bHalfScaleY;
        float bMaxY = b.transform.position.y + bHalfScaleY;

        bool collisionX = bMaxX >= aMinX && aMaxX >= bMinX;
        bool collisionY = bMaxY >= aMinY && aMaxY >= bMinY;
        return collisionX && collisionY;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGameOver)
        {
            transform.position += new Vector3(0, jumpHight, 0);
            v = 0.0f;
        }
        else
        {
            if (transform.position.y > -4.0f) {
                transform.position -= new Vector3(0, v * Time.deltaTime - (gConst * Mathf.Pow(Time.deltaTime, 2)) / 2, 0);
                v += gConst * Time.deltaTime;
            }
        }
        if (!isGameOver)
        {
            foreach (GameObject obj in objects)
            {
                if (IsCollided(obj, gameObject))
                {
                    isGameOver = true;
                    gameOverText.SetActive(true);
                }
            }
        }
    }
}
