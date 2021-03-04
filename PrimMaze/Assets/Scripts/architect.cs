using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class architect : MonoBehaviour
{
    private int nbRangees, nbColonnes;
    public GameObject noeudPrefab;

    private Dictionary<Vector3, GameObject> noeuds;
    private Dictionary<Vector3, GameObject> liens;
    public int countLink = 0;

    // Start is called before the first frame update
    void Start()
    {
        noeuds = new Dictionary<Vector3, GameObject>();
        liens = new Dictionary<Vector3, GameObject>();

        //nbRangees = globalScript.NbRangees;
        //nbColonnes = globalScript.NbColonnes;
        nbRangees = 8;
        nbColonnes = 6;
        //Instantiate(premierNoeud);
        //premierNoeud.GetComponent<noeudScript>().Create(nbRangees - 1, nbColonnes - 1, ref noeuds, ref liens);

        for (int z = 0; z < nbRangees; z++) {
            for (int x = 0; x < nbColonnes; x++) {

                GameObject temp = Instantiate(noeudPrefab);
                temp.GetComponent<noeudScript>().Create(x, z, ref noeuds, ref liens);

            }
        }

        string a = "ssss";
        countLink = liens.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
