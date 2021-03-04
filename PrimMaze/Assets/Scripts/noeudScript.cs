using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noeudScript : MonoBehaviour
{
    public GameObject lienOuest;
    public GameObject lienSud;
    public bool explored;

    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        explored = false;
    }
    public void Create(int posX, int posZ, ref Dictionary<Vector3, GameObject> noeuds)
    {
        pos = new Vector3(posX, 0.5f / 6.0f, posZ);

        Vector3 spawnPos = 6 * pos;

        gameObject.transform.position = spawnPos;

        noeuds[pos] = gameObject;
        if(posX > 0)
        {
            GameObject clone = Instantiate(lienOuest);
            lienOuest.GetComponent<lienScript>().Create(noeuds[pos + Vector3.left], gameObject, ref clone);
        }
        if(posZ > 0)
        {
            GameObject clone = Instantiate(lienSud);
            lienSud.GetComponent<lienScript>().Create(noeuds[pos + Vector3.back], gameObject, ref clone);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getPos() {
        return pos;
    }

    public lienScript getMinWLien() {
        int minW = int.MaxValue;
        lienScript minWLien = null;

        //S
        if (architect.liens.ContainsKey(pos + (Vector3.back / 2))) {
            lienScript link = architect.liens[pos + (Vector3.back / 2)].GetComponent<lienScript>();
            if ((!link.used()) && (link.weight < minW)) {
                minW = link.weight;
                minWLien = link;
            }
        }
        //N
        if (architect.liens.ContainsKey(pos + (Vector3.forward / 2))) {
            lienScript link = architect.liens[pos + (Vector3.forward / 2)].GetComponent<lienScript>();
            if ((!link.used()) && (link.weight < minW)) {
                minW = link.weight;
                minWLien = link;
            }
        }
        //E
        if (architect.liens.ContainsKey(pos + (Vector3.right / 2))) {
            lienScript link = architect.liens[pos + (Vector3.right / 2)].GetComponent<lienScript>();
            if ((!link.used()) && (link.weight < minW)) {
                minW = link.weight;
                minWLien = link;
            }
        }
        //W
        if (architect.liens.ContainsKey(pos + (Vector3.left / 2))) {
            lienScript link = architect.liens[pos + (Vector3.left / 2)].GetComponent<lienScript>();
            if ((!link.used()) && (link.weight < minW)) {
                minW = link.weight;
                minWLien = link;
            }
        }

        return minWLien;
    }
}
