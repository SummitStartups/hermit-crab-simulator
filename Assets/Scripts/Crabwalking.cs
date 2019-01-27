using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crabwalking : MonoBehaviour
{
    public Transform target;

    float timeCounter;
    private float speed = 0.6f;
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

        Vector3 targetPostition = new Vector3( target.position.x, 
                                        this.transform.position.y, 
                                        target.position.z ) ;
        this.transform.LookAt( targetPostition ) ;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        // transform.position +=  (targetPostition - transform.position).normalized/10 * speed;
    
     Vector3 diff = target.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            Debug.Log(curDistance);
            if (curDistance <= 0.3f)
            {
                Destroy (target.gameObject);
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
  

    // Collide with Food
    void OnCollisionEnter(Collision col)
    {
        print("colliding");
        if (col.collider.gameObject.tag == "Food")
        {
            Destroy(col.collider.gameObject);    
        }
    }
}
