using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cube;
    public GameObject start;
    public GameObject finish;
    public List<GameObject> lines;

    public float maxSpeed;
    public float minSpeed;
    public float spawnRate;

    public Queue<GameObject>[] cubesByLines = { new Queue<GameObject>(), new Queue<GameObject>(), new Queue<GameObject>() };

    void spawnCube() {
        int lineIndex = UnityEngine.Random.Range(0, 3);

        Vector3 position = new Vector3(lines[lineIndex].transform.position.x, start.transform.position.y, 0);
        GameObject cubeInstance = GameObject.Instantiate(cube, position, Quaternion.identity);
        Cube cubeScript = cubeInstance.GetComponent<Cube>();

        float maxSpeed = 5.0f;
        foreach (GameObject cube in cubesByLines[lineIndex])
        {
            Cube script = cube.GetComponent<Cube>();
            maxSpeed = script.speed < maxSpeed ? script.speed : maxSpeed;
        }
        cubeScript.speed = UnityEngine.Random.Range(1.0f, maxSpeed);
        cubeScript.finish = finish;

        cubesByLines[lineIndex].Enqueue(cubeInstance);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnCube", 0.0f, spawnRate);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject firstCube = cubesByLines[0].Dequeue();
            Debug.Log("Precision: " + (firstCube.transform.position.y - finish.transform.position.y));
            GameObject.Destroy(firstCube);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject firstCube = cubesByLines[1].Dequeue();
            Debug.Log("Precision: " + (firstCube.transform.position.y - finish.transform.position.y));
            GameObject.Destroy(firstCube);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            GameObject firstCube = cubesByLines[2].Dequeue();
            Debug.Log("Precision: " + (firstCube.transform.position.y - finish.transform.position.y));
            GameObject.Destroy(firstCube);
        }
    }
}
