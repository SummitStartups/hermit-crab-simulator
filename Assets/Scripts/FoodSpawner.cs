using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{

    public GameObject food;

    // Update is called once per frame
    void Start()
    {
        for (int i = 0; i < 40; i++)
        {
            SpawnFood();
        }
        InvokeRepeating("SpawnFood", 1f, 1f);
    }

    void SpawnFood()
    {
        Instantiate(food, transform.position + new Vector3(
            transform.localScale.x * (Random.value - 0.5f),
            0.55f,
            transform.localScale.z * (Random.value - 0.5f)
        ), Quaternion.identity);
    }
}
