using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class architect : MonoBehaviour
{
    private int nbRangees, nbColonnes;
    public GameObject noeudPrefab;
    private GameObject premierNoeud;

    private Dictionary<Vector3, GameObject> noeuds;
    private List<noeudScript> noeudsVisite;
    public static Dictionary<Vector3, GameObject> liens;

    // Start is called before the first frame update
    void Start()
    {
        noeudsVisite = new List<noeudScript>();
        noeuds = new Dictionary<Vector3, GameObject>();
        liens = new Dictionary<Vector3, GameObject>();
        GameObject premier = null;
        //nbRangees = globalScript.NbRangees;
        //nbColonnes = globalScript.NbColonnes;
        nbRangees = 8;
        nbColonnes = 6;

        for (int z = 0; z < nbRangees; z++) {
            for (int x = 0; x < nbColonnes; x++) {
                
                GameObject temp = Instantiate(noeudPrefab);
                temp.GetComponent<noeudScript>().Create(x, z, ref noeuds);
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


        if (!noeuds.ContainsKey(minWeightLien.noeudPrecedent.GetComponent<noeudScript>().getPos()))
            noeudsVisite.Add(noeuds[minWeightLien.noeudPrecedent.GetComponent<noeudScript>().getPos()].GetComponent<noeudScript>());
        if (!noeuds.ContainsKey(minWeightLien.noeudSuivant.GetComponent<noeudScript>().getPos()))
            noeudsVisite.Add(noeuds[minWeightLien.noeudSuivant.GetComponent<noeudScript>().getPos()].GetComponent<noeudScript>());


    }
}
