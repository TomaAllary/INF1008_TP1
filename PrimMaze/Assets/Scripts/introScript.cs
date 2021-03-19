﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class introScript : MonoBehaviour
{
    public GameObject panel2D, panel3D;
    public Image btn3dMode, btn2dMode;

    private InputField nbRangees, nbEtages, nbColonnes;

    private void Start() {
        open2DMenu();
    }

    //On passe les valeurs de rangées et de colonnes en variable globale pour permettre à la prochaine scene de les recevoir, et on lance la scène
    public void PlayGame()
    {
        if (panel2D.activeSelf) {

            if (nbRangees.text != "" && nbColonnes.text != "") {
                globalScript.NbRangees = int.Parse(nbRangees.text);
                globalScript.NbColonnes = int.Parse(nbColonnes.text);
                SceneManager.LoadScene("Labyrinthe");
            }
            else {
                if (nbRangees.text == "")
                    nbRangees.image.color = Color.red;
                else
                    nbRangees.image.color = Color.white;

                if (nbColonnes.text == "")
                    nbColonnes.image.color = Color.red;
                else
                    nbColonnes.image.color = Color.white;
            }
        }
        else {
            if (nbRangees.text != "" && nbEtages.text != "" && nbColonnes.text != "") {
                globalScript.NbRangees = int.Parse(nbRangees.text);
                globalScript.NbEtages = int.Parse(nbEtages.text);
                globalScript.NbColonnes = int.Parse(nbColonnes.text);
                SceneManager.LoadScene("Labyrinthe3D");
            }
            else {
                if (nbRangees.text == "")
                    nbRangees.image.color = Color.red;
                else
                    nbRangees.image.color = Color.white;

                if (nbEtages.text == "")
                    nbEtages.image.color = Color.red;
                else
                    nbEtages.image.color = Color.white;

                if (nbColonnes.text == "")
                    nbColonnes.image.color = Color.red;
                else
                    nbColonnes.image.color = Color.white;
            }
        }
    }

    public void open3DMenu() {
        btn3dMode.color = new Color(255, 126, 0);
        btn2dMode.color = new Color(212, 108, 7);

        panel3D.SetActive(true);
        panel2D.SetActive(false);

        nbRangees = panel3D.transform.Find("InputRangees").GetComponent<InputField>();
        nbEtages = panel3D.transform.Find("InputEtages").GetComponent<InputField>();
        nbColonnes = panel3D.transform.Find("InputColonnes").GetComponent<InputField>();
    }

    public void open2DMenu() {
        btn2dMode.color = new Color(255, 126, 0);
        btn3dMode.color = new Color(212, 108, 7);

        panel2D.SetActive(true);
        panel3D.SetActive(false);

        nbRangees = panel2D.transform.Find("InputRangees").GetComponent<InputField>();
        nbColonnes = panel2D.transform.Find("InputColonnes").GetComponent<InputField>();
    }
}
