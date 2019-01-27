using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    public Transform cameraObject;
    public OVRScreenFade fade;
    public Transform shell;
    public float speed = 2;
    public bool dead = false; // when true, no input registered from player
    public bool hiding = false; // when true, character can't move
    public bool charge = false;
    public bool ExitShell = false; // when true, character is vulnerable from all sides
    // public bool jump = false; // when true, character can't attack
    public float size = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            Vector2 primaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

            hiding = (OVRInput.Get(OVRInput.Button.PrimaryTouchpad) && primaryTouchpad.y < -0.2f) || Input.GetKey(KeyCode.H);
            charge = (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)) || Input.GetKey(KeyCode.C);
            ExitShell = (OVRInput.Get(OVRInput.RawButton.Back) || Input.GetKey(KeyCode.E));
            // jump = (OVRInput.Get(OVRInput.Button.PrimaryTouchpad) && primaryTouchpad.y > 0.2f) || Input.GetKey(KeyCode.J); 

            if (hiding && shell != null)
            {
                if (shell.localPosition.y > 1.2f)
                {
                    shell.localPosition -= Vector3.up * Time.deltaTime * 4;
                }
            }
            else
            {
				if(Input.GetKey(KeyCode.LeftArrow)){
					transform.Rotate(0, -1, 0);
				}
				if(Input.GetKey(KeyCode.RightArrow)){
					transform.Rotate(0, 1, 0);
				}
                if (shell != null && shell.localPosition.y < 2f)
                {
                    shell.localPosition += Vector3.up * Time.deltaTime * 4;
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

                if (shell != null & ExitShell)
                {
                    shell.localPosition += Vector3.up * Time.deltaTime * 4;
                    shell.SetParent(null);
					shell = null;
                }

                // if (jump) {
                // 	// move up y-axis temporarily by height of character
                // }
            }
        }
    }

    // Collision logic
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.tag == "Shark" && hiding == false)
        {
            dead = true;
            fade.FadeOut();
            Invoke("ReloadGame", 3f);
        }

        if (shell == null && col.collider.transform.parent == null && col.collider.gameObject.tag == "Shell")
        { 
			shell = col.collider.transform;
            shell.SetParent(cameraObject);
        }
    }

    // Reload game when dead
    void ReloadGame()
    {
        SceneManager.LoadScene("Sand");
    }
}

/*
TODO: 
1. if no shell -> increased movement speed + disable hiding
2. defensive manuevers?? hide in shell, hide behind rock, dig underground (lose shell)
 */
