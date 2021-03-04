﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noeudScript : MonoBehaviour
{
    public GameObject lienOuest;
    public GameObject lienSud;


    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }
    public void Create(int posX, int posZ, ref Dictionary<Vector3, GameObject> noeuds, ref Dictionary<Vector3, GameObject> liens)
    {
        pos = new Vector3(posX, 0.5f / 8.0f, posZ);

        Vector3 spawnPos = 8 * pos;

        gameObject.transform.position = spawnPos;

        noeuds[pos] = gameObject;
        if(posX > 0)
        {
            Instantiate(lienOuest);
            lienOuest.GetComponent<lienScript>().Create(noeuds[pos + Vector3.left], gameObject, ref liens);
        }
        if(posZ > 0)
        {
            Instantiate(lienSud);
            lienSud.GetComponent<lienScript>().Create(noeuds[pos + Vector3.back], gameObject, ref liens);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getPos() {
        return pos;
    }
}
