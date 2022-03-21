using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex00 : MonoBehaviour
{
    public static GameObject currentCharacter;
    public float speed;
    public float jumpHight;
    Rigidbody2D m_rigidbody;
    [SerializeField] private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        currentCharacter = GameObject.Find("Claire");
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    float error = 0.05f;

    void OnCollisionStay2D(Collision2D other) {
        float playerBottomY = transform.position.y - transform.localScale.y / 2;
        float otherTopY = other.transform.position.y + other.transform.localScale.y / 2;

        if (Mathf.Abs(playerBottomY - otherTopY) < error)
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D other) {
        isGrounded = false;
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
