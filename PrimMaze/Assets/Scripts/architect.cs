using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class architect : MonoBehaviour
{
    private int nbRangees, nbColonnes;
    public GameObject premierNoeud;
  
    // Start is called before the first frame update
    void Start()
    {
        //nbRangees = globalScript.NbRangees;
        //nbColonnes = globalScript.NbColonnes;
        nbRangees = 5;
        nbColonnes = 5;
        Instantiate(premierNoeud);
        premierNoeud.GetComponent<noeudScript>().Create(0, nbRangees - 1, 0, nbColonnes - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
