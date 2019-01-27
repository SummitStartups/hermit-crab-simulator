using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    public static Movement instance;
    public Transform cameraObject;
    public OVRScreenFade fade;
    public Transform shell;
    public GameObject droppedShell;
    Vector3 basePos;
    public float speed = 0.5f;
    public float coolDown;
    public float coolDownPeriod = 1.5f;
    public GameObject deathText;

    public bool dead = false; // when true, no input registered from player
    public bool hiding = false; // when true, character can't move
    public bool charge = false; // whjen true, character moves quicker
    private Transform exitShell; // when true, character is vulnerable from all sides
    public float currSize = 1;
    public float maxSize;
    Shark[] sharks;
    public AudioSource music;


    // Use this for initialization
    void Start()
    {
        instance = this;
        sharks = FindObjectsOfType<Shark>();
        maxSize = currSize * 2; // hermit grows in new shell 2x size of previous shell??
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (shell && !shell.GetComponent<Shell>().isGood)
            {
                exitShell = shell;
                exitShell.GetComponent<Collider>().enabled = true;
                exitShell.GetComponent<Rigidbody>().isKinematic = false;
                shell.SetParent(null);
                shell = null;
                Narrator.instance.StartCoroutine("PlayPop");
                StartCoroutine("ResetShell");
            }
            if (shell && shell.localPosition.x + shell.localPosition.z > 0.1f)
            {
                shell.localPosition -= new Vector3(
                    shell.localPosition.normalized.x,
                    0,
                    shell.localPosition.normalized.z);
            }
            bool targeted = false;
            foreach (Shark s in sharks)
            {
                if (s.attacking)
                {
                    targeted = true;
                    break;
                }
            }
            if (targeted)
            {
                music.volume = 0;
            }
            else
            {
                music.volume = 0.5f;
            }
            Vector2 primaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

            charge = (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)) || Input.GetKey(KeyCode.C);
            if (shell)
            {
                hiding = (OVRInput.Get(OVRInput.Button.PrimaryTouchpad)) || Input.GetKey(KeyCode.DownArrow);
                if (OVRInput.Get(OVRInput.RawButton.Back) || Input.GetKey(KeyCode.E))
                {
                    exitShell = shell;
                    exitShell.GetComponent<Collider>().enabled = true;
                    exitShell.GetComponent<Rigidbody>().isKinematic = false;
                    shell.SetParent(null);
                    shell = null;
                    StartCoroutine("ResetShell");
                }
            }

            if (hiding)
            {
                if (shell)
                {
                    if (shell.localPosition.y > 1.2f)
                    {
                        shell.localPosition -= Vector3.up * Time.deltaTime * 4;
                    }
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Rotate(0, -1, 0);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Rotate(0, 1, 0);
                }

                if (shell)
                {
                    if (shell.localPosition.y < 2f)
                    {
                        shell.localPosition += Vector3.up * Time.deltaTime * 4;
                    }
                }
                if (primaryTouchpad.y > 0.2f || Input.GetKey(KeyCode.W))
                {
                    transform.position += cameraObject.forward * speed / 100
                    * (primaryTouchpad.y == 0 ? 1 : primaryTouchpad.y);
                }
                if (primaryTouchpad.y < -0.2f || Input.GetKey(KeyCode.S))
                {
                    transform.position += cameraObject.forward * speed / 100
                    * (primaryTouchpad.y == 0 ? -1 : primaryTouchpad.y);
                }
                if (primaryTouchpad.x > 0.2f || Input.GetKey(KeyCode.D))
                {
                    transform.position += cameraObject.right * speed / 100
                    * (primaryTouchpad.x == 0 ? 1 : primaryTouchpad.x);
                }
                if (primaryTouchpad.x < -0.2f || Input.GetKey(KeyCode.A))
                {
                    transform.position += cameraObject.right * speed / 100
                    * (primaryTouchpad.x == 0 ? -1 : primaryTouchpad.x);
                }

                if (charge)
                {
                    transform.position += cameraObject.forward * speed / 100;
                }

            }
        }
    }

    // Collision logic
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.tag == "Shark")
        {
            if (!hiding)
            {
                dead = true;
                deathText.SetActive(true);
                deathText.GetComponent<TextMesh>().text = "You have Died! \n Your Score is \n " + (transform.localScale.x * 1000);
                //fade.FadeOut();
                Narrator.instance.StartCoroutine("PlayDeath");
                Invoke("ReloadGame", 5f);
            }
        }
        if (shell == null && col.collider.transform != exitShell && col.collider.transform.parent == null && col.collider.gameObject.tag == "Shell")
        {
            Shell s = col.collider.GetComponent<Shell>();
            if (s.isGood)
            {
                shell = col.collider.transform;
                shell.SetParent(cameraObject);
                shell.localEulerAngles = new Vector3(0, -90, 0);
                shell.GetComponent<Collider>().enabled = false;
                shell.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    // Reload game when dead
    void ReloadGame()
    {
        SceneManager.LoadScene("Sand");
    }

    IEnumerator ResetShell()
    {
        yield return new WaitForSeconds(1);
        exitShell = null;
    }
}
