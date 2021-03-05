using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lienScript : MonoBehaviour
{
    public Vector2Int noeudSuivant, noeudActuel;
    public int weight;
    private GameObject myself;

    private int orientation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Create(noeudScript noeudSuivant, noeudScript noeudActuel, ref GameObject clone)
    {
        myself = clone;
        weight = Random.Range(1, 11);

        this.noeudActuel = noeudActuel.getPos();
        this.noeudSuivant = noeudSuivant.getPos();

        Vector2 spawnPos = (Vector2)(this.noeudActuel + this.noeudSuivant) / 2.0f;

        gameObject.transform.position = 6.0f * new Vector3(spawnPos.x, 0.5f / 6.0f, spawnPos.y);

        orientation = globalScript.SUD;
        if (spawnPos.x % 1 == 0)
            transform.LookAt(transform.position + Vector3.forward);
        else {
            transform.LookAt(transform.position + Vector3.right);
            orientation = globalScript.OUEST;
        }

        architect.liens[this.noeudActuel.x, this.noeudActuel.y, orientation] = this;



    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public bool used() {
        return !myself.activeSelf;
    }

    public Vector2Int[] useLien() {
        myself.SetActive(false);

        return new Vector2Int[2] { myself.GetComponent<lienScript>().noeudActuel, myself.GetComponent<lienScript>().noeudSuivant };
    }
}
