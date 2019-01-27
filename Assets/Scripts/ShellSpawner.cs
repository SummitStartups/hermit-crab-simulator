using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSpawner : MonoBehaviour
{

    public GameObject[] array = new GameObject[4]; // shells with 4 different starting positions/orientations
    public int countShells;
    public GameObject starting;
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
        starting = Instantiate(shellObj, transform.position + new Vector3(
            transform.localScale.x * (Random.value - 0.5f),
            0.55f,
            transform.localScale.z * (Random.value - 0.5f)
        ), Quaternion.identity);
        starting.GetComponent<Rigidbody>().AddForce(100,100,100);
    }
}
