using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSpawner : MonoBehaviour
{

    public GameObject[] array = new GameObject[4]; // shells with 4 different starting positions/orientations
    public int countShells;
    // Update is called once per frame
    void Start()
    {
        for (int i = 0; i < countShells; i++)
        {
            SpawnShells(array[Random.Range(1,array.Length)]);
        }
    }

    void SpawnShells(GameObject shellObj)
    {
        GameObject go = Instantiate(shellObj, transform.position + new Vector3(
            transform.localScale.x * (Random.value - 0.5f),
            0.6f,
            transform.localScale.z * (Random.value - 0.5f)
        ), Quaternion.identity);
        go.transform.Rotate(new Vector3(0, Random.value * 360, 0));
        go.transform.localScale *= Random.value * 3f + 0.05f;
    }
}
