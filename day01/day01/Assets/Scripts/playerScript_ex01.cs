using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex01 : MonoBehaviour
{
    public static GameObject currentCharacter;
    public float speed;
    public float jumpHight;
    Rigidbody2D m_rigidbody;
    [SerializeField] private bool isGrounded;

    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        currentCharacter = GameObject.Find("Claire");
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // Debug.Log(gameObject.name + " collied with " + other.gameObject.name);

        Renderer otherRend = other.gameObject.GetComponent<Renderer>();
        Vector3 thisMin = rend.bounds.min;
        Vector3 otherMax = otherRend.bounds.max;

        // Debug.Log(Mathf.Abs(thisMin.y - otherMax.y));

        if (Mathf.Abs(thisMin.y - otherMax.y) < error)
        {
            isGrounded = true;

            gameObject.transform.SetParent(other.transform, true);
        }
    }

    float error = 0.05f;
    void OnCollisionStay2D(Collision2D other) {
        Renderer otherRend = other.gameObject.GetComponent<Renderer>();
        Vector3 thisMin = rend.bounds.min;
        Vector3 otherMax = otherRend.bounds.max;

        if (Mathf.Abs(thisMin.y - otherMax.y) < error)
        {
            isGrounded = true;

            gameObject.transform.SetParent(other.transform, true);
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        isGrounded = false;

        gameObject.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCharacter == gameObject)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                float v0 = Mathf.Sqrt(jumpHight * 2.0f * 9.81f);
                float m = m_rigidbody.mass;
                float f = (v0 * m) / Time.fixedDeltaTime;
                m_rigidbody.AddForce(Vector2.up * f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentCharacter = GameObject.Find("Claire");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentCharacter = GameObject.Find("John");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentCharacter = GameObject.Find("Thomas");
        }
    }
}
