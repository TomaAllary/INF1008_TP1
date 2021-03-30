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
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        music.volume = globalScript.MusicVolume;

        string[] listeN = { "25", "50", "100", "1000"};        
        
        for (int i = 0; i < listeN.Length; i++)
        {
            string[] tableValeurs;
            int moyenneInstantiation = 0, moyenneGeneration = 0, n = 0;
            string[] ligneSeparee;
            string path = Application.dataPath + "/Stats_For_N_Equals_" + listeN[i] + ".txt";
            if (File.Exists(path))
            {
                tableValeurs = File.ReadAllLines(path);
                foreach (string line in tableValeurs)
                {
                    try
                    {
                        ligneSeparee = line.Split(',');
                        moyenneInstantiation += int.Parse(ligneSeparee[0]);
                        moyenneGeneration += int.Parse(ligneSeparee[1]);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Format error");
                    }
                }
                n = tableValeurs.Length;
                moyenneInstantiation = (moyenneInstantiation / n);
                moyenneGeneration = (moyenneGeneration / n);
               
            }
            else
            {
                moyenneGeneration = 0;
                moyenneInstantiation = 0;
                n = 0;
            }
            statistiques.GetComponent<Text>().text += "n: " + listeN[i] + "\t\t\tMoyenne génération: "+ moyenneGeneration.ToString() + "\t\t\tMoyenne instantiation: " + moyenneInstantiation.ToString() + "\t\t\tNombre d'essais effectués: " + n.ToString() + "\n\n";
        }
    }

    // Update is called once per frame
    public void retourIntro()
    {
        SceneManager.LoadScene("Intro");
    }
}
