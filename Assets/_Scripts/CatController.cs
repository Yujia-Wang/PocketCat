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
    public bool isContact = false;
    public float x;
    public float y;

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
        x = CrossPlatformInputManager.GetAxis("Horizontal");
        y = CrossPlatformInputManager.GetAxis("Vertical");
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
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                isClicked = true;
            }

            if (isContact == false && (x != 0 || y != 0))
            {
                //Play walking animation
                anim.Play("Walk");
                Debug.Log("This is walk animation");
            }
            else if (x == 0 && y == 0 && isClicked == false && isContact == false)
            {
                //Play static animation
                anim.Play("Idle");
                Debug.Log("This is Idle animation");
            }
            else if (isClicked == true)
            {
                anim.Play("Jump");
                Debug.Log("This is Jumping animation");
                Debug.Log(anim["Jump"].normalizedTime);

                if (anim.IsPlaying("Jump") && anim["Jump"].normalizedTime > 0.99f)
                {
                    isClicked = false;
                    Debug.Log("fuck");
                }
            }
        }

        
      
    }

    //开始接触
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("开始接触");
    }

    //接触结束
    void OnTriggerExit(Collider collider)
    {
        isContact = false;
        Debug.Log("接触结束");
    }

    // 接触持续中
    void OnTriggerStay(Collider collider)
    {
        if (x == 0 && y == 0)
        {
            isContact = true;
            anim.Play("Eat");
            Debug.Log("This is Eating animation");
            Debug.Log("接触持续中");
        }
        
    }

}
