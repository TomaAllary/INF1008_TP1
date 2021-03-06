using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noeudScript : MonoBehaviour
{
    public GameObject lienOuest;
    public GameObject lienSud;
    public GameObject murExterieur;
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

        //Creer les deux liens pour ce noeud
        if(posX > 0)
        {
            Vector2Int next = pos + Vector2Int.left;
            GameObject clone = Instantiate(lienOuest);
            clone.name = "Lien " + pos.ToString() + " to west";

            clone.GetComponent<lienScript>().Create(noeuds[next.x, next.y], this);
        }
        if(posZ > 0)
        {
            Vector2Int next = pos + Vector2Int.down;
            GameObject clone = Instantiate(lienSud);
            clone.name = "Lien " + pos.ToString() + " to south";

            clone.GetComponent<lienScript>().Create(noeuds[next.x, next.y], this);
        }

        //Removing pillars from nodes touching an exterior wall. 
       if(posX == globalScript.NbColonnes-1)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        if (posZ == globalScript.NbRangees - 1)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void explore() {
        explored = true;
    }

    public void setColor(Color couleur) {
        gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetColor("_Color", couleur);
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
            if (link.diponible() && (link.weight < minW)) {
                minW = link.weight;
                minWLien = link;
            }
        }
        //O
        link = architect.liens[pos.x, pos.y, globalScript.OUEST];
        if (link != null) {
            if (link.diponible() && (link.weight < minW)) {
                minW = link.weight;
                minWLien = link;
            }
        }

        //N
        if (pos.y + 1 < architect.liens.GetLength(1)) {
            link = architect.liens[pos.x, pos.y + 1, globalScript.SUD];
            if (link != null) {
                if (link.diponible() && (link.weight < minW)) {
                    minW = link.weight;
                    minWLien = link;
                }
            }
        }
        //E
        if (pos.x + 1 < architect.liens.GetLength(0)) {
            link = architect.liens[pos.x + 1, pos.y, globalScript.OUEST];
            if (link != null) {
                if (link.diponible() && (link.weight < minW)) {
                    minW = link.weight;
                    minWLien = link;
                }
            }
        }


        return minWLien;
    }
}
