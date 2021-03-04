using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class introScript : MonoBehaviour
{
    public InputField nbRangees, nbColonnes;

    //On passe les valeurs de rangées et de colonnes en variable globale pour permettre à la prochaine scene de les recevoir, et on lance la scène
    public void PlayGame()
    {
        if (nbRangees.text != "" && nbColonnes.text != "")
        {
            globalScript.NbRangees = int.Parse(nbRangees.text);
            globalScript.NbColonnes = int.Parse(nbColonnes.text);
            SceneManager.LoadScene(1);
        }
        else
        {
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
}
