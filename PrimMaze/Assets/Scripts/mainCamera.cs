using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCamera : MonoBehaviour
{
    public Light mainLight;


    private void OnPreCull()
    {
        mainLight.enabled = true;
    }

    private void onPostRendre()
    {
        mainLight.enabled = false;
    }
}
