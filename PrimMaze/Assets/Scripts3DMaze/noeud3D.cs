using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noeud3D : MonoBehaviour
{
    public GameObject lienOuest;
    public GameObject lienSud;
    public GameObject murExterieur;
    public bool explored;

    private Vector3Int pos;

    // Start is called before the first frame update
    void Start()
    {
        explored = false;
    }
    public void Create(int posX, int posY, int posZ, ref noeud3D[,,] noeuds)
    {
        pos = new Vector3Int(posX, posY, posZ);
        Vector3 spawnPos = 6 * pos;
        gameObject.transform.position = spawnPos;
        noeuds[posX, posY, posZ] = this;

        //Creer les deux liens pour ce noeud
        if(posX > 0)
        {
            Vector3Int next = pos + Vector3Int.left;
            GameObject clone = Instantiate(lienOuest);
            clone.name = "Lien " + pos.ToString() + " to west";

            clone.GetComponent<lien3D>().Create(noeuds[next.x, next.y, next.z], this);

            GameMenuManager.operationLinkInit++;
        }
        if(posZ > 0)
        {
            Vector3Int next = pos + Vector3Int.down;
            GameObject clone = Instantiate(lienSud);
            clone.name = "Lien " + pos.ToString() + " to south";

            clone.GetComponent<lien3D>().Create(noeuds[next.x, next.y, next.z], this);

            GameMenuManager.operationLinkInit++;
        }

        //Removing pillars from nodes touching an exterior wall. 
        if (posX == globalScript.NbColonnes-1)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        if (posZ == globalScript.NbRangees - 1)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (Random.Range(1, 20) != 10)
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
    }

    public void explore() {
        explored = true;
    }

    public void setColor(Color couleur) {
        gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetColor("_Color", couleur);
    }

    public Vector3Int getPos() {
        return pos;
    }

    public lien3D getMinWLien() {
        int minW = int.MaxValue;
        lien3D minWLien = null;

        //S
        lien3D link = architect3D.liens[pos.x, pos.y, globalScript.SUD];
        if (link != null) {
            if (link.diponible() && (link.weight < minW)) {
                minW = link.weight;
                minWLien = link;
            }
        }
        //O
        link = architect3D.liens[pos.x, pos.y, globalScript.OUEST];
        if (link != null) {
            if (link.diponible() && (link.weight < minW)) {
                minW = link.weight;
                minWLien = link;
            }
        }

        //N
        if (pos.y + 1 < architect3D.liens.GetLength(1)) {
            link = architect3D.liens[pos.x, pos.y + 1, globalScript.SUD];
            if (link != null) {
                if (link.diponible() && (link.weight < minW)) {
                    minW = link.weight;
                    minWLien = link;
                }
            }
        }
        //E
        if (pos.x + 1 < architect3D.liens.GetLength(0)) {
            link = architect3D.liens[pos.x + 1, pos.y, globalScript.OUEST];
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
