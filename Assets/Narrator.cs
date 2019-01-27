using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{

    public static Narrator instance;
    public AudioClip[] EatSmarm;
    public AudioClip EatSFX;
    public AudioClip[] DeathSmarm;
    public AudioClip[] PopOut;
    public AudioClip[] Welcome;
	int welcomeCount = 0;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
		StartCoroutine("PlayWelcome");
    }

    public IEnumerator PlayEat()
    {
        audioSource.clip = EatSFX;
        audioSource.Play();
        yield return new WaitForSeconds(EatSFX.length + 0.1f);
        if (Random.value > 0.5f && !audioSource.isPlaying)
        {
            audioSource.clip = EatSmarm[Mathf.FloorToInt(EatSmarm.Length * Random.value)];
            audioSource.Play();
        }
    }

    public IEnumerator PlayPop()
    {
        audioSource.clip = PopOut[Mathf.FloorToInt(PopOut.Length * Random.value)];
        audioSource.Play();
		yield return null;
    }

    public IEnumerator PlayDeath()
    {
        audioSource.clip = DeathSmarm[Mathf.FloorToInt(DeathSmarm.Length * Random.value)];
        audioSource.Play();
		yield return null;
    }

    public IEnumerator PlayWelcome()
    {
        audioSource.clip = Welcome[welcomeCount];
		welcomeCount++;
        audioSource.Play();
        yield return new WaitForSeconds(EatSFX.length + 1f);
		if(welcomeCount < Welcome.Length){
			StartCoroutine("PlayWelcome");
		}
    }
}
