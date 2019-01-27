using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceRender : MonoBehaviour
{

    // Use this for initialization
    float distance = 20;
    public MeshRenderer mr;
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        StartCoroutine("CheckRender");
    }
    IEnumerator CheckRender()
    {
        yield return null;
        if (mr.enabled && Vector3.Distance(Movement.instance.transform.position, transform.position) > distance)
        {
            mr.enabled = false;
        }
		if(!mr.enabled && Vector3.Distance(Movement.instance.transform.position, transform.position) < distance)
        {
            mr.enabled = true;
        }
        yield return new WaitForSeconds(Random.value + 1);
        StartCoroutine("CheckRender");
    }
}
