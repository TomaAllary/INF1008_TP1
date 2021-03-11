using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lien3D : MonoBehaviour
{
    public Vector3Int noeudSuivant, noeudActuel;
    private noeud3D noeudSuivantScript, noeudActuelScript;
    public int weight;
    private int orientation;

    public void Create(noeud3D noeudSuivant, noeud3D noeudActuel)
    {
        weight = Random.Range(1, 11);

        noeudSuivantScript = noeudSuivant;
        noeudActuelScript = noeudActuel;

        this.noeudActuel = noeudActuel.getPos();
        this.noeudSuivant = noeudSuivant.getPos();

        Vector3 spawnPos = (Vector3)(this.noeudActuel + this.noeudSuivant) / 2.0f;
        gameObject.transform.position = 6.0f * spawnPos;

        //Tourner l'objet dans la bonne direction
        orientation = globalScript.SUD;
        if (spawnPos.x % 1 == 0)
            transform.LookAt(transform.position + Vector3.forward);
        else {
            transform.LookAt(transform.position + Vector3.right);
            orientation = globalScript.OUEST;
        }
        if (Random.Range(1, 10) == 5)
            this.transform.GetChild(0).gameObject.SetActive(true);
        //Ajouter le lien dans le tableau
        architect3D.liens[this.noeudActuel.x, this.noeudActuel.y, orientation] = this;
    }

    //Retourne vrai si le lien n'est pas deja utilise et si le prochain noeud n'est pas visite
    public bool diponible() {
        return (gameObject.activeSelf && ( !noeudActuelScript.explored || !noeudSuivantScript.explored ));
    }

    public Vector3Int[] useLien() {
        gameObject.SetActive(false);

        return new Vector3Int[2] { noeudActuel, noeudSuivant };
    }
}
