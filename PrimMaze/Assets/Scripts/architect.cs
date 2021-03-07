using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class architect : MonoBehaviour
{
    private int nbRangees, nbColonnes;
    public GameObject noeudPrefab;
    public GameObject murExterieur;
    public GameObject timmy;
    public Camera miniMapCam;

    private noeudScript[,] noeuds;
    private List<noeudScript> noeudsVisite;
    public static lienScript[,,] liens;

    // Start is called before the first frame update
    void Start()
    {
        nbRangees = globalScript.NbRangees;
        nbColonnes = globalScript.NbColonnes;
      

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

        //Placement de Timmy
        Instantiate(timmy, new Vector3(noeuds[0, 0].transform.position.x, noeuds[0, 0].transform.position.y + 3, noeuds[0, 0].transform.position.z), Quaternion.Euler(0,0,0));

        //Génération des murs extérieurs
        GameObject cloneWallEast = Instantiate(murExterieur, noeuds[0,0].transform.position + new Vector3(-2.5f, 4, nbRangees*3-2.5f), Quaternion.Euler(0, 90f, 0));
        cloneWallEast.transform.localScale =  new Vector3 (nbRangees*6, 10, 1);
        GameObject cloneWallSouth = Instantiate(murExterieur, noeuds[0, 0].transform.position + new Vector3(nbColonnes * 3 - 2.5f, 4, -2.5f), Quaternion.Euler(0, 0, 0));
        cloneWallSouth.transform.localScale = new Vector3(nbColonnes * 6, 10, 1);
        GameObject cloneWallNorth = Instantiate(murExterieur, cloneWallSouth.transform.position + new Vector3(0, 0, nbRangees * 6 -0.5f), Quaternion.Euler(0, 0, 0));
        cloneWallNorth.transform.localScale = cloneWallSouth.transform.localScale;
        GameObject cloneWallWest = Instantiate(murExterieur, cloneWallEast.transform.position + new Vector3(nbColonnes * 6 -0.5f, 0, 0), Quaternion.Euler(0,90f,0));
        cloneWallWest.transform.localScale = cloneWallEast.transform.localScale;


        noeuds[0, 0].setColor(Color.blue);
        noeuds[nbColonnes - 1, nbRangees - 1].setColor(Color.cyan);

        noeudsVisite.Add(premier.GetComponent<noeudScript>());
        premier.GetComponent<noeudScript>().explore();

        //StartCoroutine( CreatePrim() );
        CreatePrim();


        //Setting the miniMap
        miniMapCam.transform.position = new Vector3(nbColonnes * 3 - 2.5f, 30, nbRangees * 3 - 2.5f);
        if (nbRangees > nbColonnes)
            miniMapCam.orthographicSize = nbRangees * 3;
        else
            miniMapCam.orthographicSize = nbColonnes * 3;

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
            noeuds[noeudsToAdd[0].x, noeudsToAdd[0].y].explore();
        }

        if (!noeuds[noeudsToAdd[1].x, noeudsToAdd[1].y].explored) {

            noeudsVisite.Add(noeuds[noeudsToAdd[1].x, noeudsToAdd[1].y]);
            noeuds[noeudsToAdd[1].x, noeudsToAdd[1].y].explore();
        }

        //Si des noeuds ne sont pas visite -> continuer
        if (noeudsVisite.Count < (noeuds.GetLength(0) * noeuds.GetLength(1)))
            CreatePrim();
    }
}
