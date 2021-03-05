using System.Collections;
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
                if (premier == null)
                    premier = temp;
            }
        }

        noeudsVisite.Add(premier.GetComponent<noeudScript>());

        //CreatePrim();

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

        minWeightLien.useLien();

        //Rendre les noeuds attacher au lien "expored"
        Vector2Int noeudAVerifier = minWeightLien.noeudActuel.getPos();
        if (!noeuds[noeudAVerifier.x, noeudAVerifier.y].explored) {

            noeudsVisite.Add(noeuds[noeudAVerifier.x, noeudAVerifier.y]);
            noeuds[noeudAVerifier.x, noeudAVerifier.y].explored = true;
        }

        noeudAVerifier = minWeightLien.noeudSuivant.getPos();
        if (!noeuds[noeudAVerifier.x, noeudAVerifier.y].explored) {

            noeudsVisite.Add(noeuds[noeudAVerifier.x, noeudAVerifier.y]);
            noeuds[noeudAVerifier.x, noeudAVerifier.y].explored = true;
        }

        //Si des noeuds ne sont pas visite -> continuer
        if(noeudsVisite.Count < (noeuds.GetLength(0) * noeuds.GetLength(1)))
            CreatePrim();


    }
}
