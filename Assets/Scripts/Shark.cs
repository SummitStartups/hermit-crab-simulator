using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public Transform target;
    public AudioClip low, high;
    AudioSource audioSource;
    float timeCounter;
    float speed = 1;
    float width = 2;
    float length = 3;
    float direction = 1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("LowAudio");
    }

    IEnumerator LowAudio()
    {
        audioSource.clip = low;
        if (Vector3.Distance(target.position, transform.position) < 25)
        {
            audioSource.Play();
            yield return new WaitForSeconds(Vector3.Distance(target.position, transform.position) / 10);
        }
        else
        {
            yield return new WaitForSeconds(3);
        }
        StartCoroutine("HighAudio");
    }
    IEnumerator HighAudio()
    {
        audioSource.clip = high;
        if (Vector3.Distance(target.position, transform.position) < 25)
        {
            audioSource.Play();
            yield return new WaitForSeconds(Vector3.Distance(target.position, transform.position) / 10);
        }
        else
        {
            yield return new WaitForSeconds(3);
        }
        StartCoroutine("LowAudio");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= 5)
        {
            if (Mathf.Approximately(direction, 1))
            {
                transform.LookAt(target);
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(transform.position - target.position);
            }

            transform.position += direction * (target.position - transform.position).normalized / 50;
        }
        else
        {
            direction = 1;
            transform.forward = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"))); ;
            timeCounter += Time.deltaTime * speed;
            float x = Mathf.Cos(timeCounter) * width;
            float y = 0;
            float z = Mathf.Sin(timeCounter) * length;
            transform.rotation = Quaternion.LookRotation(new Vector3(x, y, z) * Time.deltaTime * speed);
            transform.position += new Vector3(x, y, z) * Time.deltaTime * speed;
        }

    }

    // Collide with Player
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.tag == "Player")
        {
            direction = -1;
        }
    }
}
