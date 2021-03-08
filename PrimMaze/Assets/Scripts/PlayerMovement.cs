using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    //public Animator animator;

    public GameObject gameMenu;
    public bool menuActive;
    public float speed;

    private Rigidbody rb;

    //Camera controls
    public float mouseSens = 100f;
    private float zoom = 1.0f;
    public Transform cam;
    private float zoomSensitivity = 16f;
    private float initialCamDist;
    public Transform camTarget;

    // Start is called before the first frame update
    void Start()
    {
        initialCamDist = (cam.position - transform.position).magnitude;
        cam.transform.LookAt(camTarget.position);

        menuActive = false;
        rb = this.GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {


        

    }

    private void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;


        transform.Rotate(0, mouseX, 0);



        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity * Time.deltaTime;
        zoom = Mathf.Clamp(zoom, 0.5f, 4.0f);

        Vector3 camDist = (cam.position - transform.position).normalized * initialCamDist * zoom;


        cam.position = transform.position + camDist;



        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z) * speed * Time.deltaTime;

        rb.MovePosition(rb.position + move);


        cam.transform.LookAt(camTarget.position);


    }


}
