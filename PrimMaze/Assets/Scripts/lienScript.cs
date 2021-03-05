using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lienScript : MonoBehaviour
{
    public noeudScript noeudSuivant, noeudActuel;
    public int weight;
    private GameObject myself;

    private Vector2Int pos;
    private int orientation;

    // Start is called before the first frame update
    void Start()
    {
        weight = Random.Range(1, 11);
    }

    public void Create(noeudScript noeudSuivant, noeudScript noeudActuel, ref GameObject clone)
    {
        myself = clone;

        this.noeudActuel = noeudActuel;
        this.noeudSuivant = noeudSuivant;
        pos = noeudActuel.getPos();

        Vector2 spawnPos = (Vector2)(pos + noeudSuivant.getPos()) / 2.0f;

        gameObject.transform.position = 6.0f * new Vector3(spawnPos.x, 0.5f / 6.0f, spawnPos.y);

        int orientation = globalScript.SUD;
        if (spawnPos.x % 1 == 0)
            transform.LookAt(transform.position + Vector3.forward);
        else {
            transform.LookAt(transform.position + Vector3.right);
            orientation = globalScript.OUEST;
        }

        architect.liens[pos.x, pos.y, orientation] = this;

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

    public bool used() {
        return !myself.activeSelf;
    }

    public void useLien() {
        myself.SetActive(false);
    }
}
