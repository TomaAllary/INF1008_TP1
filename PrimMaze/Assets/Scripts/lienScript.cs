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

    public void Create(Vector3 nextNoeudPos, bool vertical, GameObject noeudPrecedent, ref Dictionary<Vector3, GameObject> noeuds, ref Dictionary<Vector3, GameObject> liens)
    {
        this.noeudPrecedent = noeudPrecedent;
        Vector3 pos;
        if (vertical)
            pos = nextNoeudPos + (Vector3.forward / 2);
        else
            pos = nextNoeudPos + (Vector3.right / 2);

        Vector3 spawnPos = 8 * pos;

        gameObject.transform.position = spawnPos;


        liens[pos] = gameObject;

        if (!noeuds.ContainsKey(nextNoeudPos)) {
            Instantiate(noeudSuivant);
            noeudSuivant.GetComponent<noeudScript>().Create((int)nextNoeudPos.x, (int)nextNoeudPos.z, ref noeuds, ref liens);
        }
        else {

        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
