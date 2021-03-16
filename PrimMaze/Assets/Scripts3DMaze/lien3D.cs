using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lien3D : MonoBehaviour
{
    public Vector3Int noeudSuivant, noeudActuel;
    private noeud3D noeudSuivantScript, noeudActuelScript;
    public int weight;
    public int orientation;

    private bool used;

    public void Create(noeud3D noeudSuivant, noeud3D noeudActuel)
    {
        used = false;

        

        weight = Random.Range(1, 11);

        noeudSuivantScript = noeudSuivant;
        noeudActuelScript = noeudActuel;

        this.noeudActuel = noeudActuel.getPos();
        this.noeudSuivant = noeudSuivant.getPos();

        Vector3 spawnPos = (Vector3)(this.noeudActuel + this.noeudSuivant) / 2.0f;
        

        //Tourner l'objet dans la bonne direction
        orientation = globalScript.SUD;
        if(spawnPos.y % 1 != 0) {
            //upward link
            gameObject.transform.position = 6.0f * spawnPos + new Vector3(0, 6.0f, 0);
            //Make it unvisible unless we use it
            gameObject.SetActive(false);

            orientation = globalScript.BAS;
        }
        else {
            gameObject.transform.position = 6.0f * spawnPos + new Vector3(0, 3.0f, 0);


            if (Random.Range(1, 10) == 5)
                this.transform.GetChild(0).gameObject.SetActive(true);

            if (spawnPos.z % 1 != 0)
                transform.LookAt(transform.position + Vector3.forward);
            else if (spawnPos.x % 1 != 0) {
                transform.LookAt(transform.position + Vector3.right);
                orientation = globalScript.OUEST;
            }
        }


        //Ajouter le lien dans le tableau
        architect3D.liens[this.noeudActuel.x, this.noeudActuel.y, this.noeudActuel.z, orientation] = this;
    }

    //Retourne vrai si le lien n'est pas deja utilise et si le prochain noeud n'est pas visite
    public bool diponible() {
        return (!used && ( !noeudActuelScript.explored || !noeudSuivantScript.explored ));
    }

    public Vector3Int[] useLien() {
        if(orientation == globalScript.BAS)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
        used = true;
        return new Vector3Int[2] { noeudActuel, noeudSuivant };
    }
}
