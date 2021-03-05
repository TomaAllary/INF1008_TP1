using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noeudScript : MonoBehaviour
{
    public GameObject lienOuest;
    public GameObject lienSud;

    public bool explored;

    private Vector2Int pos;
    // Start is called before the first frame update
    void Start()
    {
        explored = false;
    }
    public void Create(int posX, int posZ, ref noeudScript[,] noeuds)
    {
        pos = new Vector2Int(posX, posZ);

        Vector3 spawnPos = 6 * new Vector3(posX, 0.5f / 6.0f, posZ);

        gameObject.transform.position = spawnPos;

        noeuds[posX, posZ] = this;
        if(posX > 0)
        {
            Vector2Int next = pos + Vector2Int.left;
            GameObject clone = Instantiate(lienOuest);

            clone.name = "Lien " + pos.ToString() + " to west";

            clone.GetComponent<lienScript>().Create(noeuds[next.x, next.y], this, ref clone);
        }
        if(posZ > 0)
        {
            Vector2Int next = pos + Vector2Int.down;
            GameObject clone = Instantiate(lienSud);

            clone.name = "Lien " + pos.ToString() + " to south";

            clone.GetComponent<lienScript>().Create(noeuds[next.x, next.y], this, ref clone);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2Int getPos() {
        return pos;
    }

    public lienScript getMinWLien() {
        int minW = int.MaxValue;
        lienScript minWLien = null;

        //S
        lienScript link = architect.liens[pos.x, pos.y, globalScript.SUD];
        if (link != null) {
            if ((!link.used()) && (link.weight < minW)) {
                minW = link.weight;
                minWLien = link;
            }
        }
        //O
        link = architect.liens[pos.x, pos.y, globalScript.OUEST];
        if (link != null) {
            if ((!link.used()) && (link.weight < minW)) {
                minW = link.weight;
                minWLien = link;
            }
        }

        //N
        link = architect.liens[pos.x, pos.y + 1, globalScript.SUD];
        if (link != null) {
            if ((!link.used()) && (link.weight < minW)) {
                minW = link.weight;
                minWLien = link;
            }
        }
        //E
        link = architect.liens[pos.x + 1, pos.y, globalScript.OUEST];
        if (link != null) {
            if ((!link.used()) && (link.weight < minW)) {
                minW = link.weight;
                minWLien = link;
            }
        }


        return minWLien;
    }
}
