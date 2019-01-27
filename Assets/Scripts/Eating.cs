using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{

    private Movement movement;
    private float growthRate = 0.05f;
    private float maxGrowthRate = 0.05f;
    void Start()
    {
        movement = GetComponent<Movement>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Food")
        {
            Destroy(col.gameObject);
            float scale = Mathf.Min(growthRate * transform.localScale.x, maxGrowthRate);
            if (movement.shell != null)
            {
                movement.shell.localScale *= transform.localScale.x / (scale + transform.localScale.x);
            }
            transform.localScale += Vector3.one * scale;
            movement.speed += Mathf.Min(growthRate * scale);
            Narrator.instance.StartCoroutine("PlayEat");
        }
    }
}
