using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainCamera : MonoBehaviour
{
    public Shader shader;
    public Text FloorLabel;
    public GameObject minimap;
    private Transform timmy;
    
    void Start() {
        this.GetComponent<Camera>().SetReplacementShader(shader, "");
        if (globalScript.Difficulty == globalScript.HARD)
            minimap.SetActive(false);
    }

    private void Update() {
        if (timmy == null) {
            timmy = GameObject.Find("Timmy(Clone)").transform;
            if(globalScript.Difficulty == globalScript.NORMAL) {

                this.GetComponent<Camera>().cullingMask = this.GetComponent<Camera>().cullingMask ^ ( (1 << LayerMask.NameToLayer("Timmy")) | (1 << LayerMask.NameToLayer("Plane")));
            }
        }

        transform.position = new Vector3(transform.position.x, timmy.position.y + 6.25f, transform.position.z);
        FloorLabel.text = "Etage: " + ( (int)(timmy.position.y / 6.0f) + 1).ToString();
    }
}
