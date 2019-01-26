using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{

    private Movement movement;
	private float growthRate = 0.1f;
	private float maxGrowthRate = 0.1f;
    void Start()
    {
        movement = GetComponent<Movement>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Food")
        {
            Destroy(col.gameObject);
            transform.localScale += Vector3.one * Mathf.Min(growthRate * transform.localScale.x, maxGrowthRate);
			movement.speed += Mathf.Min(growthRate * movement.speed, maxGrowthRate);
        }
    }
}
