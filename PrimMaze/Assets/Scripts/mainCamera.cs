using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCamera : MonoBehaviour
{
    public Shader shader;
    private Transform timmy;
    void Start() {
        this.GetComponent<Camera>().SetReplacementShader(shader, "");
    }

    private void Update() {
        if(timmy == null)
            timmy = GameObject.Find("Timmy(Clone)").transform;

        transform.position = new Vector3(transform.position.x, timmy.position.y + 5.94f, transform.position.z);
    }
}
