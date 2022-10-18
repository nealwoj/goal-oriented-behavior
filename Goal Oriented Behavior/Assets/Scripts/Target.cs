using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public static Vector3 position;
    public float speed, time;
    public Enemy enemyPrefab;
    public bool spawn;

    private int offset;

    // Start is called before the first frame update
    void Start()
    {
        spawn = true;
        offset = 2;
        speed = 15f;
        time = 0f;
        position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        //target movement
        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0f, 0f, -speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
        if (Input.GetKey(KeyCode.A))
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        if (Input.GetKey(KeyCode.S))
            transform.position += new Vector3(0f, 0f, speed * Time.deltaTime);

        if (time > 0f)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = 0f;
            spawn = true;
        }

        if (Input.GetKey(KeyCode.Tab) && spawn)
        {
            Instantiate(enemyPrefab, new Vector3(0f, 1f, 0f), transform.rotation);
            spawn = false;
            time = 2f;
        }
    }
}
