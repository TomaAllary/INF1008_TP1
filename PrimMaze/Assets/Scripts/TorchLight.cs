using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLight : MonoBehaviour
{
    
    int torchTick = 0;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (torchTick == 15)
        {
            this.GetComponentInChildren<Light>().intensity = Random.Range(0.5f, 2);
            torchTick = 0;
        }
        else
            torchTick++;
    }


}
