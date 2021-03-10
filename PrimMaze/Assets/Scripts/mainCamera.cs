using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCamera : MonoBehaviour
{
    public Shader shader;

    void Start() {
        this.GetComponent<Camera>().SetReplacementShader(shader, "");
    }
}
