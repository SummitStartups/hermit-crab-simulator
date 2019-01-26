using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    Vector3 basePos;
    float rand;
    bool attack;
    public GameObject movement;
    public float timeCounter;

    // Use this for initialization
    void Start()
    {
        basePos = transform.localPosition;
        rand = Random.value;

    }

    // Update is called once per frame
    void Update()
    {
        attack = movement.GetComponent<Movement>().attack;
        if (attack)
        {
            timeCounter += Time.deltaTime;
            transform.localPosition = 20*new Vector3(0,
                Mathf.Sin(200*timeCounter),
                Mathf.Cos(200*timeCounter)
                );
        }
        else
        {
            timeCounter = 0;
        }
    }
}
