using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class statsScript : MonoBehaviour
{

    public Text statistiques;
    // Start is called before the first frame update
    void Start()
    {
        string[] listeN = { "25", "50", "100", "1000", "10000" };        
        
        for (int i = 0; i < listeN.Length; i++)
        {
            string[] tableValeurs;
            int moyenne = 0, n = 0;
            string path = Application.dataPath + "/Stats_For_N_Equals_" + listeN[i] + ".txt";
            if (File.Exists(path))
            {
                tableValeurs = File.ReadAllLines(path);
                foreach (string line in tableValeurs)
                {
                    try
                    {
                        moyenne += int.Parse(line);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Format error");
                    }
                }
                n = tableValeurs.Length;
                moyenne = (moyenne / n);
            }
            else
            {
                moyenne = 0;
                n = 0;
            }
            statistiques.GetComponent<Text>().text += "pour n = " + listeN[i] + ": Moyenne de " + moyenne.ToString() + " Nombre d'essais effectués: " + n.ToString() + "\n\n";
        }
    }

    // Update is called once per frame
    public void retourIntro()
    {
        SceneManager.LoadScene("Intro");
    }
}
