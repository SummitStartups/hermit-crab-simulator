﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public Transform target;
    public AudioClip low, high;
    AudioSource audioSource;
    Movement movement;

    public bool attacking = false;
    float timeCounter;
    public float speed = 0.5f;
    float width = 2;
    float length = 3;
    float direction = 1;

    void Start()
    {
        movement = target.GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("LowAudio");
    }

    IEnumerator LowAudio()
    {
        audioSource.clip = low;
        if (attacking && Vector3.Distance(target.position, transform.position) < 25)
        {
            audioSource.Play();
            yield return new WaitForSeconds(Vector3.Distance(target.position, transform.position) / 10);
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine("HighAudio");
    }
    IEnumerator HighAudio()
    {
        audioSource.clip = high;
        if (attacking && Vector3.Distance(target.position, transform.position) < 25)
        {
            audioSource.Play();
            yield return new WaitForSeconds(Vector3.Distance(target.position, transform.position) / 10);
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine("LowAudio");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= 6 && !movement.hiding)
        {
            attacking = true;
            if (Mathf.Approximately(direction, 1))
            {
                transform.LookAt(target);
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(transform.position - target.position);
            }

            transform.position += direction * (target.position - transform.position).normalized / 50 * speed;
        }
        else
        {
            attacking = false;
            direction = 1;
            if (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) != new Vector3(0, 0, 0))
            {
                transform.forward = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))); ;
            }
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
