using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    public Animator animator;
    public float speed;
    private Rigidbody rb;

    public bool canGoUp;
    public bool canGoDown;
    public Vector3 logicPos;


    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        animator.SetFloat("speedMult", speed / 8.0f);
    }

    // Update is called once per frame
    void Update() {




    }

    private void FixedUpdate() {
        //look for down and up links
        int logicPosX = (int)((transform.position.x + 2.5f) / 6.0f);
        int logicPosY = (int)(transform.position.y / 6.0f);
        int logicPosZ = (int)((transform.position.z + 2.5f) / 6.0f);
        logicPos = new Vector3(logicPosX, logicPosY, logicPosZ);

        //look for upward
        try {
            lien3D up = architect3D.liens[logicPosX, logicPosY + 1, logicPosZ, globalScript.BAS];
            if (!up)
                throw new IndexOutOfRangeException("null link");
            canGoUp = up.isUsed();

        }
        catch (IndexOutOfRangeException e) {
            Console.WriteLine(e.Message);
            canGoUp = false;
        }

        //look for downward
        try {
            lien3D down = architect3D.liens[logicPosX, logicPosY, logicPosZ, globalScript.BAS];
            if (!down)
                throw new IndexOutOfRangeException("null link");
            canGoDown = down.isUsed();

        }
        catch (IndexOutOfRangeException e) {
            Console.WriteLine(e.Message);
            canGoDown = false;
        }

        if (canGoUp && Input.GetKeyDown(KeyCode.Space)) {
            transform.position = new Vector3(logicPosX * 6.0f, (logicPosY + 1) * 6.0f + 0.06f, logicPosZ * 6.0f);
        }
        if (canGoDown && Input.GetKeyDown(KeyCode.LeftShift)) {
            transform.position = new Vector3(logicPosX * 6.0f, (logicPosY - 1) * 6.0f + 0.06f, logicPosZ * 6.0f);
        }



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
