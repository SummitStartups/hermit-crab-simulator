using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSpawner : MonoBehaviour
{

    public GameObject shells;
    public int[] array = new int[4]; // shells with 4 different starting orientations

    // Update is called once per frame
    void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            SpawnShells();
        }
        InvokeRepeating("SpawnShells", 1f, 1f);
    }

    void SpawnShells()
    {
        Instantiate(shells, transform.position + new Vector3(
            transform.localScale.x * (Random.value - 0.5f),
            0.55f,
            transform.localScale.z * (Random.value - 0.5f)
        ), Quaternion.identity);
    }
}
