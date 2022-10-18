using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed, time;
    public Vector3 velocity;
    public GameObject target;
    public Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        time = 2f;
        targetPos = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += targetPos * Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);

        if (time > 0f)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = 0f;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            Destroy(gameObject);
        }
    }
}
