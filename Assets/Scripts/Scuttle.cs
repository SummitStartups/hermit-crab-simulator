using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scuttle : MonoBehaviour
{
   
    Vector3 basePos;
    float rand;


    // Use this for initialization
    void Start()
    {
        basePos = transform.localPosition;
        rand = Random.value;

    }

    // Update is called once per frame
    void Update()
    {
            transform.localPosition = basePos + new Vector3(0,
                Mathf.Cos(100 * (transform.position.magnitude / transform.lossyScale.y + rand)),
                Mathf.Sin(100 * (transform.position.magnitude / transform.lossyScale.z + rand))
                ) / 20;

    }
}
