using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Transform playerBody;
    public Transform cam;
    public float mouseSens = 100f;

    private float zoomSensitivity = 16f;
    private float zoom = 1.0f;
    private float initialCamDist;


    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        initialCamDist = (cam.position - playerBody.position).magnitude;
    }

    // Update is called once per frame
    void Update() {


        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;


        playerBody.RotateAround(playerBody.position, Vector3.up, mouseX);

        //Rotate cam up down around player
        //if (transform.rotation.x < 90f && transform.rotation.x > -90f)
            //transform.RotateAround(playerBody.position, playerBody.right, -mouseY);



        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity * Time.deltaTime;
        zoom = Mathf.Clamp(zoom, 0.5f, 4.0f);

        Vector3 camDist = (cam.position - playerBody.position).normalized * initialCamDist * zoom;


        cam.position = playerBody.position + camDist;
    }
}
