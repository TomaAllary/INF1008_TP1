using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class architect : MonoBehaviour
{
    private int nbRangees, nbColonnes;
    public GameObject noeudPrefab;
    public GameObject murExterieur;
    public GameObject timmy;
    public Camera miniMapCam;
    public Material endPositionMat;
    public GameObject winLb;
    public GameMenuManager gameManager;
   
    private noeudScript[,] noeuds;
    private List<noeudScript> noeudsVisite;
    public static lienScript[,,] liens;

    private Transform timmyInstance;
    private Highscores highscores;

    // Start is called before the first frame update
    void Start()
    {
        nbRangees = globalScript.NbRangees;
        nbColonnes = globalScript.NbColonnes;


        noeudsVisite = new List<noeudScript>();
        noeuds = new noeudScript[nbColonnes, nbRangees];
        liens = new lienScript[nbColonnes, nbRangees, 2];
        GameObject premier = null;

        readHighscore();

        for (int z = 0; z < nbRangees; z++) {
            for (int x = 0; x < nbColonnes; x++) {
                
                GameObject temp = Instantiate(noeudPrefab);
                globalScript.NbGenration++;
                temp.GetComponent<noeudScript>().Create(x, z, ref noeuds);

                temp.name = "Noeud " + temp.GetComponent<noeudScript>().getPos().ToString();
                if (premier == null)
                    premier = temp;
            }
        }

        //Placement de Timmy
        timmyInstance = Instantiate(timmy, new Vector3(noeuds[0, 0].transform.position.x, noeuds[0, 0].transform.position.y + 0.06f, noeuds[0, 0].transform.position.z), Quaternion.Euler(0,0,0)).transform;
        timmyInstance.name = "Timmy(Clone)";

        //Génération des murs extérieurs
        GameObject cloneWallEast = Instantiate(murExterieur, noeuds[0,0].transform.position + new Vector3(-2.5f, 2.5f, nbRangees*3-2.5f), Quaternion.Euler(0, 90f, 0));
        cloneWallEast.transform.localScale =  new Vector3 (nbRangees*6, 6, 1);
        GameObject cloneWallSouth = Instantiate(murExterieur, noeuds[0, 0].transform.position + new Vector3(nbColonnes * 3 - 2.5f, 2.5f, -2.5f), Quaternion.Euler(0, 0, 0));
        cloneWallSouth.transform.localScale = new Vector3(nbColonnes * 6, 6, 1);
        GameObject cloneWallNorth = Instantiate(murExterieur, cloneWallSouth.transform.position + new Vector3(0, 0, nbRangees * 6 -0.5f), Quaternion.Euler(0, 0, 0));
        cloneWallNorth.transform.localScale = cloneWallSouth.transform.localScale;
        GameObject cloneWallWest = Instantiate(murExterieur, cloneWallEast.transform.position + new Vector3(nbColonnes * 6 -0.5f, 0, 0), Quaternion.Euler(0,90f,0));
        cloneWallWest.transform.localScale = cloneWallEast.transform.localScale;


        noeuds[nbColonnes - 1, nbRangees - 1].transform.GetChild(0).GetComponent<Renderer>().material = endPositionMat;

        noeudsVisite.Add(premier.GetComponent<noeudScript>());
        premier.GetComponent<noeudScript>().explore();

        CreatePrim();

        //Setting the miniMap
        miniMapCam.transform.position = new Vector3(nbColonnes * 3 - 2.5f, 30, nbRangees * 3 - 2.5f);
        if (nbRangees > nbColonnes)
            miniMapCam.orthographicSize = nbRangees * 3;
        else
            miniMapCam.orthographicSize = nbColonnes * 3;

    }

    private void Update() {
        if(Mathf.Abs( timmyInstance.position.x - noeuds[nbColonnes - 1, nbRangees - 1].transform.position.x) < 2 &&
            Mathf.Abs( timmyInstance.position.z - noeuds[nbColonnes - 1, nbRangees - 1].transform.position.z) < 2) {

            //Victory
            writeHighscore();

            globalScript.gameOver = true;
            winLb.SetActive(true);
            Time.timeScale = 0;
        }
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
                    globalScript.NbOperations++;
                }
            }
            globalScript.NbOperations++;
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
        else
        {           
            int n = globalScript.NbColonnes * globalScript.NbRangees;
            if (n == 25 || n == 50 || n == 100 || n == 400 || n == 1000)
            {
                string ligne = globalScript.NbOperations.ToString() + "," + globalScript.NbGenration.ToString();
                string path = Application.dataPath + "/Stats_For_N_Equals_" + n.ToString() + ".txt";
                File.AppendAllText(path, ligne + "\n");               
            }
        }
    }  

    public void startOutro() {
        SceneManager.LoadScene("Outro");
    }

    private void writeHighscore() {
        if (globalScript.Difficulty == globalScript.EASY) {
            if (globalScript.NbRangees == 5 && globalScript.NbColonnes == 5) {
                if (highscores.score5x5 > gameManager.getTimeElapsed()) {
                    highscores.score5x5 = gameManager.getTimeElapsed();
                    highscores.score5x5_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 5 && globalScript.NbColonnes == 10) {
                if (highscores.score5x10 > gameManager.getTimeElapsed()) {
                    highscores.score5x10 = gameManager.getTimeElapsed();
                    highscores.score5x10_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 10 && globalScript.NbColonnes == 10) {
                if (highscores.score10x10 > gameManager.getTimeElapsed()) {
                    highscores.score10x10 = gameManager.getTimeElapsed();
                    highscores.score10x10_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 20 && globalScript.NbColonnes == 20) {
                if (highscores.score20x20 > gameManager.getTimeElapsed()) {
                    highscores.score20x20 = gameManager.getTimeElapsed();
                    highscores.score20x20_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 40 && globalScript.NbColonnes == 25) {
                if (highscores.score40x25 > gameManager.getTimeElapsed()) {
                    highscores.score40x25 = gameManager.getTimeElapsed();
                    highscores.score40x25_User = globalScript.Username;
                }
            }
        }else if(globalScript.Difficulty == globalScript.NORMAL) {
            if (globalScript.NbRangees == 5 && globalScript.NbColonnes == 5) {
                if (highscores.normalScore5x5 > gameManager.getTimeElapsed()) {
                    highscores.normalScore5x5 = gameManager.getTimeElapsed();
                    highscores.normalScore5x5_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 5 && globalScript.NbColonnes == 10) {
                if (highscores.normalScore5x10 > gameManager.getTimeElapsed()) {
                    highscores.normalScore5x10 = gameManager.getTimeElapsed();
                    highscores.normalScore5x10_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 10 && globalScript.NbColonnes == 10) {
                if (highscores.normalScore10x10 > gameManager.getTimeElapsed()) {
                    highscores.normalScore10x10 = gameManager.getTimeElapsed();
                    highscores.normalScore10x10_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 20 && globalScript.NbColonnes == 20) {
                if (highscores.normalScore20x20 > gameManager.getTimeElapsed()) {
                    highscores.normalScore20x20 = gameManager.getTimeElapsed();
                    highscores.normalScore20x20_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 40 && globalScript.NbColonnes == 25) {
                if (highscores.normalScore40x25 > gameManager.getTimeElapsed()) {
                    highscores.normalScore40x25 = gameManager.getTimeElapsed();
                    highscores.normalScore40x25_User = globalScript.Username;
                }
            }
        }
        else if(globalScript.Difficulty == globalScript.HARD) {
            if (globalScript.NbRangees == 5 && globalScript.NbColonnes == 5) {
                if (highscores.hardScore5x5 > gameManager.getTimeElapsed()) {
                    highscores.hardScore5x5 = gameManager.getTimeElapsed();
                    highscores.hardScore5x5_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 5 && globalScript.NbColonnes == 10) {
                if (highscores.hardScore5x10 > gameManager.getTimeElapsed()) {
                    highscores.hardScore5x10 = gameManager.getTimeElapsed();
                    highscores.hardScore5x10_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 10 && globalScript.NbColonnes == 10) {
                if (highscores.hardScore10x10 > gameManager.getTimeElapsed()) {
                    highscores.hardScore10x10 = gameManager.getTimeElapsed();
                    highscores.hardScore10x10_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 20 && globalScript.NbColonnes == 20) {
                if (highscores.hardScore20x20 > gameManager.getTimeElapsed()) {
                    highscores.hardScore20x20 = gameManager.getTimeElapsed();
                    highscores.hardScore20x20_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 40 && globalScript.NbColonnes == 25) {
                if (highscores.hardScore40x25 > gameManager.getTimeElapsed()) {
                    highscores.hardScore40x25 = gameManager.getTimeElapsed();
                    highscores.hardScore40x25_User = globalScript.Username;
                }
            }
        }

        File.WriteAllText(Application.dataPath + "/2dHighscores.txt", JsonUtility.ToJson(highscores));
    }

    private void readHighscore() {
        if(File.Exists(Application.dataPath + "/2dHighscores.txt")) {
            string json = File.ReadAllText(Application.dataPath + "/2dHighscores.txt");
            highscores = JsonUtility.FromJson<Highscores>(json);
        }
        else {
            highscores = new Highscores();
        }
    }
}
