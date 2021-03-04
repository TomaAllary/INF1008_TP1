using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class architect : MonoBehaviour
{
    private int nbRangees, nbColonnes;
    public GameObject premierNoeud;

    private Dictionary<Vector3, GameObject> noeuds;
    private Dictionary<Vector3, GameObject> liens;

    // Start is called before the first frame update
    void Start()
    {
        noeuds = new Dictionary<Vector3, GameObject>();
        liens = new Dictionary<Vector3, GameObject>();

        //nbRangees = globalScript.NbRangees;
        //nbColonnes = globalScript.NbColonnes;
        nbRangees = 5;
        nbColonnes = 5;
        Instantiate(premierNoeud);
        premierNoeud.GetComponent<noeudScript>().Create(nbRangees - 1, nbColonnes - 1, ref noeuds, ref liens);

        string a = "ssss";

        /*for(int x = 0; x < nbColonnes; x++) {
            for(int z = 0; z < nbRangees; z++) {

            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
