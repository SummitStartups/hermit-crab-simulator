using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    public Transform cameraObject;
    public OVRScreenFade fade;
    public Transform shell;
    public GameObject droppedShell;
    Vector3 basePos;
    public float speed = 2;
    public float coolDown;
    public float coolDownPeriod = 1.5f;

    public bool dead = false; // when true, no input registered from player
    public bool hiding = false; // when true, character can't move
    public bool charge = false; // whjen true, character moves quicker
    public bool attack = false; // when true, character in attack frame
    public bool ExitShell = false; // when true, character is vulnerable from all sides
    public float currSize = 1;
    public float maxSize; 


    // Use this for initialization
    void Start()
    {
        maxSize = currSize * 2; // hermit grows in new shell 2x size of previous shell??
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            Vector2 primaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

            charge = (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)) || Input.GetKey(KeyCode.C);
            if (shell)
            {
                hiding = (OVRInput.Get(OVRInput.Button.PrimaryTouchpad) && primaryTouchpad.y < -0.2f) || Input.GetKey(KeyCode.H);
                ExitShell = (OVRInput.Get(OVRInput.RawButton.Back) || Input.GetKey(KeyCode.E));
            }
            if (coolDown <= Time.time)
            {
                attack = (OVRInput.Get(OVRInput.Button.PrimaryTouchpad) && primaryTouchpad.y > +0.2f) || Input.GetKey(KeyCode.X);
            }

            if (hiding || attack)
            {
                if (shell) { 
                    if (shell.localPosition.y > 1.2f)
                    {
                        shell.localPosition -= Vector3.up * Time.deltaTime * 4;
                    }
                }
                if (attack)
                {
                    //print(attack);
                    coolDown = Time.time + coolDownPeriod;
                    Invoke("RevertAttack", 1f);
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

                if (shell) { 
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

                if (ExitShell)
                {
                    // shell moves up and becomes detached from player

                    GameObject DroppedShell = Instantiate(droppedShell, transform.position, transform.rotation);
                    Vector3 droppedShellPos = DroppedShell.transform.position;
                    DroppedShell.transform.localScale = new Vector3(droppedShellPos.x*5, droppedShellPos.y*25, droppedShellPos.z*5);
                    speed++;
                    Destroy(shell.gameObject);
                    ExitShell = false;
                }
            
            }
        }
    }

    // Collision logic
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.tag == "Shark")
        {
            if (attack == true)
            {
                Destroy(col.collider.gameObject);
            }
            else if (hiding == false)
            {
                dead = true;
                fade.FadeOut();
                Invoke("ReloadGame", 3f);
            }
        }
        if (col.collider.transform.parent == null && col.collider.gameObject.tag == "Shell") { 
            col.collider.transform.SetParent(cameraObject); 
        }
    }

    // Reload game when dead
    void ReloadGame()
    {
        SceneManager.LoadScene("Sand");
    }

    // Revert attack mode
    void RevertAttack()
    {
        attack = false;
        print(attack);
    }
}
