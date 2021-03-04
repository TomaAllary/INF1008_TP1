using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lienScript : MonoBehaviour
{
    public GameObject noeudPrecedent, noeudSuivant;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Create(int noeudsPrecedentsH, int noeudsSuivantsH, int noeudsPrecedentsV, int noeudsSuivantsV, bool vertical, GameObject noeudPrecedent)
    {
        this.noeudPrecedent = noeudPrecedent;
        Vector3 spawnPos = new Vector3(6 * noeudsPrecedentsH - 1, (float)0.5, 6 * noeudsPrecedentsV - 1);
        gameObject.transform.position = spawnPos;
        Instantiate(noeudSuivant);
        noeudSuivant.GetComponent<noeudScript>().Create(noeudsPrecedentsH, noeudsSuivantsH, noeudsPrecedentsV, noeudsSuivantsV);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
