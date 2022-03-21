using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class BallonScript : MonoBehaviour
{
    public GameObject gameOverText;
    public GameObject ballonLifetimeText;
    public GameObject secondsText;

    public float maxSize;
    public GameObject staminaIndicator;
    public float staminaMinYPosition;
    private float staminaMaxYScale;
    private float currentStaminaPercent;
    private bool isGameOver;
    private DateTime startTime;

    void handleStaminaIndicator(float percent) {
        staminaIndicator.transform.position = new Vector3(
            staminaIndicator.transform.position.x,
            staminaMinYPosition * (1 - (percent / 100)),
            0
        );
        staminaIndicator.transform.localScale = new Vector3(
            staminaIndicator.transform.localScale.x,
            staminaMaxYScale * (percent / 100),
            0
        );
    }

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        staminaMaxYScale = staminaIndicator.transform.localScale.y;

        currentStaminaPercent = 1.0f;
        handleStaminaIndicator(currentStaminaPercent);

        startTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentStaminaPercent < 100)
                {
                    transform.localScale += new Vector3(0.2f, 0.2f, 0.0f);

                    currentStaminaPercent += 3;
                    handleStaminaIndicator(currentStaminaPercent);
                }
            }
            else if (transform.localScale.x > 0.2)
            {
                transform.localScale -= new Vector3(0.2f, 0.2f, 0.0f) * Time.deltaTime;

                if (currentStaminaPercent > 1)
                {
                    currentStaminaPercent -= 5.0f * Time.deltaTime;
                    handleStaminaIndicator(currentStaminaPercent);
                }
            }
            else
            {
                isGameOver = true;
                gameOverText.SetActive(true);
                ballonLifetimeText.SetActive(true);
                secondsText.SetActive(true);
                Debug.Log("Balloon life time: " + (DateTime.Now - startTime).Seconds + 's');
                secondsText.GetComponent<UnityEngine.UI.Text>().text = (DateTime.Now - startTime).Seconds.ToString() + 's';
            }

            if (transform.localScale.x > maxSize)
            {
                GameObject.Destroy(gameObject);
                Debug.Log("Balloon life time: " + (DateTime.Now - startTime).Seconds + 's');
                secondsText.GetComponent<UnityEngine.UI.Text>().text = (DateTime.Now - startTime).Seconds.ToString() + 's';

                isGameOver = true;
                gameOverText.SetActive(true);
                ballonLifetimeText.SetActive(true);
                secondsText.SetActive(true);
            }
        }
    }
}
