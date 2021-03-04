using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noeudScript : MonoBehaviour
{
    public GameObject lienEst;
    public GameObject lienSud;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Create(int noeudsPrecedentsH, int noeudsSuivantsH, int noeudsPrecedentsV, int noeudsSuivantsV)
    {
        Vector3 spawnPos = new Vector3(6*noeudsPrecedentsH, (float)0.5, 6*noeudsPrecedentsV);
        noeudsPrecedentsH++;
        noeudsPrecedentsV++;
        gameObject.transform.position = spawnPos;
        if(noeudsSuivantsH > 0)
        {
            Instantiate(lienEst);
            lienEst.GetComponent<lienScript>().Create(noeudsPrecedentsH + 1, noeudsSuivantsH -1, noeudsPrecedentsV , noeudsSuivantsV, true, gameObject);
        }
        if(noeudsSuivantsV > 0)
        {
            Instantiate(lienSud);
            lienSud.GetComponent<lienScript>().Create(noeudsPrecedentsH , noeudsSuivantsH , noeudsPrecedentsV + 1, noeudsSuivantsV -1, false, gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
