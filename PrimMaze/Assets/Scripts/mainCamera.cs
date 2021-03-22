using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainCamera : MonoBehaviour
{
    public Shader shader;
    public Text FloorLabel;
    private Transform timmy;
    
    void Start() {
        this.GetComponent<Camera>().SetReplacementShader(shader, "");
    }

    private void Update() {
        if(timmy == null)
            timmy = GameObject.Find("Timmy(Clone)").transform;

        transform.position = new Vector3(transform.position.x, timmy.position.y + 6.25f, transform.position.z);
        FloorLabel.text = "Etage: " + ( (int)(timmy.position.y / 6.0f) + 1).ToString();
    }
}
