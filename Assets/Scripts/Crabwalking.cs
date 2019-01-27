using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crabwalking : MonoBehaviour
{
    public Transform target;

    bool attacking = false;
    float timeCounter;
    public float speed = 0.5f;
    float width = 2;
    float length = 3;
    float direction = 1;

    void Start()
    {
    }

     // Update is called once per frame
    void Update()
    {
        target = FindClosestEnemy().transform;
        if (Vector3.Distance(target.position, transform.position) <= 10)
        {
            attacking = true;
            if (Mathf.Approximately(direction, 1))
            {
                transform.LookAt(target);
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(transform.position - target.position);
            }

            transform.position += direction * (target.position - transform.position).normalized / 50 * speed;
        }
        else
        {
            attacking = false;
            direction = 1;
            if (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) != new Vector3(0, 0, 0))
            {
                transform.forward = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))); ;
            }
            timeCounter += Time.deltaTime * speed;
            float x = Mathf.Cos(timeCounter) * width;
            float y = 0;
            float z = Mathf.Sin(timeCounter) * length;
            transform.rotation = Quaternion.LookRotation(new Vector3(x, y, z) * Time.deltaTime * speed);
            transform.position += new Vector3(x, y, z) * Time.deltaTime * speed;
        }

    }

          public GameObject FindClosestEnemy()
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Food");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            return closest;
        }
  

    // Collide with Player
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.tag == "Food")
        {
            Destroy(col.collider.gameObject);
        }
    }
}
