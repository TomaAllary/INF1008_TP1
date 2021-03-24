using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Transform Player, Target;
    public float mouseSens = 100f;

    private float mouseX, mouseY;
    private List<GameObject> obstructions;

    /*private float zoomSensitivity = 16f;
    private float zoom = 1.0f;
    private float initialCamDist;*/

    // Start is called before the first frame update
    void Start() {
        obstructions = new List<GameObject>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        //Move camera & rotate character...
        mouseX += Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.Rotate(0, mouseX, 0);
        transform.LookAt(Target.position);

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);

        CheckViewObstruction();


        //Zoom camera...
        /*zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity * Time.deltaTime;
        zoom = Mathf.Clamp(zoom, 0.5f, 4.0f);

        Vector3 camDist = (cam.position - transform.position).normalized * initialCamDist * zoom;

        cam.position = transform.position + camDist;*/

    }

    private void CheckViewObstruction() {

        //reset layer of object
        foreach (GameObject obj in obstructions) {
            obj.layer = 0; //defeult layer
        }

        //Make object between timmy and cam invisible..
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Target.position, transform.position - Target.position, Mathf.Infinity, ~(1 << LayerMask.NameToLayer("Timmy")));
        Debug.DrawRay(Target.position, transform.position - Target.position, Color.green);

        foreach (RaycastHit hit in hits) {
            hit.transform.gameObject.layer = LayerMask.NameToLayer("MiniMap");
            obstructions.Add(hit.transform.gameObject);
        }
    }
}
