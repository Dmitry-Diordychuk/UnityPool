using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    public GameObject claireExit;
    public GameObject johnExit;
    public GameObject thomasExit;
    public float exitError;
    GameObject currentCharacter;
    GameObject claire;
    GameObject john;
    GameObject thomas;

    // Start is called before the first frame update
    void Start()
    {
        claire = GameObject.Find("Claire");
        john = GameObject.Find("John");
        thomas = GameObject.Find("Thomas");
        currentCharacter = claire;
        gameObject.transform.Translate(
            currentCharacter.transform.position.x,
            currentCharacter.transform.position.y,
            -10
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (
            Mathf.Abs(claire.transform.position.x - claireExit.transform.position.x) < exitError &&
            Mathf.Abs(claire.transform.position.y - claireExit.transform.position.y) < exitError &&
            Mathf.Abs(john.transform.position.x - johnExit.transform.position.x) < exitError &&
            Mathf.Abs(john.transform.position.x - johnExit.transform.position.x) < exitError &&
            Mathf.Abs(thomas.transform.position.x - thomasExit.transform.position.x) < exitError &&
            Mathf.Abs(thomas.transform.position.x - thomasExit.transform.position.x) < exitError
        ) {
            Debug.Log("Win!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentCharacter = claire;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentCharacter = john;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentCharacter = thomas;
        }
        gameObject.transform.position = new Vector3(
            currentCharacter.transform.position.x,
            currentCharacter.transform.position.y,
            -10
        );
    }
}
