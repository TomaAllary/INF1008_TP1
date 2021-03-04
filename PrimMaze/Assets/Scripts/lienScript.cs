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

    public void Create(GameObject noeudSuivant, GameObject noeudPrecedent, ref Dictionary<Vector3, GameObject> liens)
    {
        this.noeudPrecedent = noeudPrecedent;
        this.noeudSuivant = noeudSuivant;
        Vector3 pos = (noeudPrecedent.GetComponent<noeudScript>().getPos() + noeudSuivant.GetComponent<noeudScript>().getPos()) / 2;

        Vector3 spawnPos = 8 * pos;

        gameObject.transform.position = spawnPos;

        if (pos.x % 1 == 0)
            transform.LookAt(transform.position + Vector3.right);
        else {
            transform.LookAt(transform.position + Vector3.forward);
        }

        liens[pos] = gameObject;

        /*if (!noeuds.ContainsKey(nextNoeudPos)) {
            Instantiate(noeudSuivant);
            noeudSuivant.GetComponent<noeudScript>().Create((int)nextNoeudPos.x, (int)nextNoeudPos.z, ref noeuds, ref liens);
        }
        else {
        
        }*/


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
