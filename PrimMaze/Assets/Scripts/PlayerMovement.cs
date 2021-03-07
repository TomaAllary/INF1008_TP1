using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 4.5f;
    public float xRange = 100.00f;
    public float zRange = 100.00f;
    //public Animator animator;

    public GameObject gameMenu, timmy;

    public bool menuActive;

    //public AudioSource timmyAudio;
    //public AudioClip timmyPas;

    private Vector3 direction;
    private Rigidbody rb;

    //private int tickPace;
    
    // Start is called before the first frame update
    void Start()
    {

        menuActive = false;
        timmy = GameObject.Find("Timmy");
        rb = this.GetComponent<Rigidbody>();
        //tickPace++;
    }

    // Update is called once per frame
    void Update()
    {


        

    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (horizontalInput != 0 || verticalInput != 0)
        {
            //animator.SetBool("isRunning", true);

            direction = (horizontalInput * Vector3.right + verticalInput * Vector3.forward).normalized;
            transform.LookAt(transform.position + direction);

            rb.MovePosition(transform.position + direction * Time.fixedDeltaTime * speed);
            /*if (tickPace == 17)
            {
                timmyAudio.PlayOneShot(timmyPas);
                tickPace = 0;
            }
            tickPace++;*/

        }
        /*else
        {
            animator.SetBool("isRunning", false);
        }*/
    }


}
