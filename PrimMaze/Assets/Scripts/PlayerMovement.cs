using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;
    public float speed;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        animator.SetFloat("speedMult", speed / 8.0f);
    }

    // Update is called once per frame
    void Update()
    {

        
        

    }

    private void FixedUpdate()
    {
        //Move charater...
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z) * speed * Time.deltaTime;
        if (move.magnitude > 0)
            animator.SetBool("isRunning", true);
        else
            animator.SetBool("isRunning", false);

        rb.MovePosition(rb.position + move);


    }





}
