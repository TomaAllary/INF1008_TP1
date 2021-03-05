﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class architect : MonoBehaviour
{
    private int nbRangees, nbColonnes;
    public GameObject noeudPrefab;
    private GameObject premierNoeud;

    private noeudScript[,] noeuds;
    private List<noeudScript> noeudsVisite;
    public static lienScript[,,] liens;

    // Start is called before the first frame update
    void Start()
    {
        //nbRangees = globalScript.NbRangees;
        //nbColonnes = globalScript.NbColonnes;
        nbRangees = 8;
        nbColonnes = 6;

        noeudsVisite = new List<noeudScript>();
        noeuds = new noeudScript[nbColonnes, nbRangees];
        liens = new lienScript[nbColonnes, nbRangees, 2];
        GameObject premier = null;
        

        for (int z = 0; z < nbRangees; z++) {
            for (int x = 0; x < nbColonnes; x++) {
                
                GameObject temp = Instantiate(noeudPrefab);
                temp.GetComponent<noeudScript>().Create(x, z, ref noeuds);

                temp.name = "Noeud " + temp.GetComponent<noeudScript>().getPos().ToString();
                if (premier == null)
                    premier = temp;
            }
        }

        noeudsVisite.Add(premier.GetComponent<noeudScript>());

        CreatePrim();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CreatePrim() {
        int minWeight = int.MaxValue;
        lienScript minWeightLien = null;

        //Trouver le lien dispo avec le poids moindre parmi les noeuds visite
        foreach (noeudScript node in noeudsVisite) {
            lienScript nodeMinWeightLien = node.getMinWLien();
            if (nodeMinWeightLien != null) {
                if (nodeMinWeightLien.weight < minWeight) {
                    minWeight = nodeMinWeightLien.weight;
                    minWeightLien = nodeMinWeightLien;
                }
            }
        }

        Vector2Int[] noeudsToAdd = minWeightLien.useLien();

        //Rendre les noeuds attacher au lien "expored"
        if (!noeuds[noeudsToAdd[0].x, noeudsToAdd[0].y].explored) {

            noeudsVisite.Add(noeuds[noeudsToAdd[0].x, noeudsToAdd[0].y]);
            noeuds[noeudsToAdd[0].x, noeudsToAdd[0].y].explored = true;
        }

        if (!noeuds[noeudsToAdd[1].x, noeudsToAdd[1].y].explored) {

            noeudsVisite.Add(noeuds[noeudsToAdd[1].x, noeudsToAdd[1].y]);
            noeuds[noeudsToAdd[1].x, noeudsToAdd[1].y].explored = true;
        }

        //Si des noeuds ne sont pas visite -> continuer
        if(noeudsVisite.Count < (noeuds.GetLength(0) * noeuds.GetLength(1)))
            CreatePrim();


    }
}
