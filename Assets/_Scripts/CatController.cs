using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class CatController : MonoBehaviour {

    private Rigidbody rb;
    private Animation anim;

    public Button Text;
    public AudioClip sound;
    public Canvas yourcanvas;
    public bool isClicked = false;

    // Use this for initialization
    void Start()
    {
        //Acquire Rigidbody
        rb = GetComponent<Rigidbody>();
        //Acquire Animation
        anim = GetComponent<Animation>();


        //Acquire ButtonInfo
        Text = Text.GetComponent<Button>();
        yourcanvas.enabled = true;
    }

	// Update is called once per frame
	void Update () {


        //Acquire control info
        float x = CrossPlatformInputManager.GetAxis("Horizontal");
        float y = CrossPlatformInputManager.GetAxis("Vertical");
        //Set speed
        Vector3 movement = new Vector3(x, 0, y);
        rb.velocity = movement * 8f;
        //Let cat move based on input
        if (x != 0 && y != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(x, y) * Mathf.Rad2Deg, transform.eulerAngles.z);
        }
        //Swap animation state while control


        //Detect KeyboardInput
      
        if (Input.GetKey("1")) {
            anim.Play("Jump");
            Debug.Log("This is Jumping animation");
        }
        
        if (x != 0 || y != 0)
        {
            //Play walking animation
            anim.Play("Walk");
            Debug.Log("This is walk animation");
        } else {
            //Play static animation
            anim.Play("Idle");
            Debug.Log("This is Idle animation");
        }
      
    }
}
