using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lienScript : MonoBehaviour
{
    public Vector2Int noeudSuivant, noeudActuel;
    private noeudScript noeudSuivantScript, noeudActuelScript;
    public int weight;
    private int orientation;

    public void Create(noeudScript noeudSuivant, noeudScript noeudActuel)
    {
        weight = Random.Range(1, 11);

        noeudSuivantScript = noeudSuivant;
        noeudActuelScript = noeudActuel;

        this.noeudActuel = noeudActuel.getPos();
        this.noeudSuivant = noeudSuivant.getPos();

        Vector2 spawnPos = (Vector2)(this.noeudActuel + this.noeudSuivant) / 2.0f;
        gameObject.transform.position = 6.0f * new Vector3(spawnPos.x, 2.6f / 6.0f, spawnPos.y);

        //Tourner l'objet dans la bonne direction
        orientation = globalScript.SUD;
        if (spawnPos.x % 1 == 0)
            transform.LookAt(transform.position + Vector3.forward);
        else {
            transform.LookAt(transform.position + Vector3.right);
            orientation = globalScript.OUEST;
        }

        //Ajouter le lien dans le tableau
        architect.liens[this.noeudActuel.x, this.noeudActuel.y, orientation] = this;
    }

    //Retourne vrai si le lien n'est pas deja utilise et si le prochain noeud n'est pas visite
    public bool diponible() {
        return (gameObject.activeSelf && ( !noeudActuelScript.explored || !noeudSuivantScript.explored ));
    }

    public Vector2Int[] useLien() {
        gameObject.SetActive(false);

        return new Vector2Int[2] { noeudActuel, noeudSuivant };
    }
}
