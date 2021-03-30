using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class architect3D : MonoBehaviour
{
    private int nbRangees, nbColonnes, nbEtages;
    public GameObject noeudPrefab;
    public GameObject murExterieur;
    public GameObject timmy;
    public Camera miniMapCam;
    public Material endPositionMat;
    public GameObject winLb;
   
    private noeud3D[,,] noeuds;
    private List<noeud3D> noeudsVisite;
    public static lien3D[,,,] liens;

    public GameMenuManager3D gameManager;

    private Transform timmyInstance;
    private Highscores3D highscores;

    // Start is called before the first frame update
    void Start()
    {
        nbRangees = globalScript.NbRangees;
        nbColonnes = globalScript.NbColonnes;
        nbEtages = globalScript.NbEtages;

        readHighscore();

        noeudsVisite = new List<noeud3D>();
        noeuds = new noeud3D[nbColonnes, nbEtages, nbRangees];
        liens = new lien3D[nbColonnes, nbEtages, nbRangees, 3];
        GameObject premier = null;

        for (int y = 0; y < nbEtages; y++) {
            for (int z = 0; z < nbRangees; z++) {
                for (int x = 0; x < nbColonnes; x++) {

                    GameObject temp = Instantiate(noeudPrefab);
                    temp.GetComponent<noeud3D>().Create(x, y, z, ref noeuds);

                    temp.name = "Noeud " + temp.GetComponent<noeud3D>().getPos().ToString();
                    if (premier == null)
                        premier = temp;
                }
            }
        }

        //Placement de Timmy
        timmyInstance = Instantiate(timmy, new Vector3(noeuds[0, 0, 0].transform.position.x, noeuds[0, 0, 0].transform.position.y + 0.06f, noeuds[0, 0, 0].transform.position.z), Quaternion.Euler(0,0,0)).transform;
        timmyInstance.name = "Timmy(Clone)";

        for (int etage = 0; etage < nbEtages; etage++) {
            //Génération des murs extérieurs
            GameObject cloneWallEast = Instantiate(murExterieur, noeuds[0, etage, 0].transform.position + new Vector3(-2.5f, 3, nbRangees * 3 - 2.5f), Quaternion.Euler(0, 90f, 0));
            cloneWallEast.transform.localScale = new Vector3(nbRangees * 6, 6, 1);
            GameObject cloneWallSouth = Instantiate(murExterieur, noeuds[0, etage, 0].transform.position + new Vector3(nbColonnes * 3 - 2.5f, 3, -2.5f), Quaternion.Euler(0, 0, 0));
            cloneWallSouth.transform.localScale = new Vector3(nbColonnes * 6, 6, 1);
            GameObject cloneWallNorth = Instantiate(murExterieur, cloneWallSouth.transform.position + new Vector3(0, 0, nbRangees * 6 - 0.5f), Quaternion.Euler(0, 0, 0));
            cloneWallNorth.transform.localScale = cloneWallSouth.transform.localScale;
            GameObject cloneWallWest = Instantiate(murExterieur, cloneWallEast.transform.position + new Vector3(nbColonnes * 6 - 0.5f, 0, 0), Quaternion.Euler(0, 90f, 0));
            cloneWallWest.transform.localScale = cloneWallEast.transform.localScale;
        }


        noeuds[nbColonnes - 1, nbEtages - 1, nbRangees - 1].transform.GetChild(0).GetComponent<Renderer>().material = endPositionMat;

        noeudsVisite.Add(premier.GetComponent<noeud3D>());
        premier.GetComponent<noeud3D>().explore();

        //StartCoroutine( CreatePrim() );
        CreateBacktracking(premier.GetComponent<noeud3D>());


        //Setting the miniMap
        miniMapCam.transform.position = new Vector3(nbColonnes * 3 - 2.5f, 30, nbRangees * 3 - 2.5f);
        if (nbRangees > nbColonnes)
            miniMapCam.orthographicSize = nbRangees * 3;
        else
            miniMapCam.orthographicSize = nbColonnes * 3;

    }

    private void Update() {
        if(Mathf.Abs( timmyInstance.position.x - noeuds[nbColonnes - 1, nbEtages - 1, nbRangees - 1].transform.position.x) < 2 &&
            Mathf.Abs( timmyInstance.position.y - noeuds[nbColonnes - 1, nbEtages - 1, nbRangees - 1].transform.position.y) < 0.1f &&
            Mathf.Abs(timmyInstance.position.z - noeuds[nbColonnes - 1, nbEtages - 1, nbRangees - 1].transform.position.z) < 2) {

            //Victory
            writeHighscore();

            globalScript.gameOver = true;
            winLb.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void CreateBacktracking(noeud3D node) {

        lien3D nodeMinWeightLien = node.getMinWLien();
        while(nodeMinWeightLien != null) {
            Vector3Int[] noeudsToAdd = nodeMinWeightLien.useLien();


            //Rendre les noeuds attacher au lien "expored"
            if (!noeuds[noeudsToAdd[0].x, noeudsToAdd[0].y, noeudsToAdd[0].z].explored) {

                noeudsVisite.Add(noeuds[noeudsToAdd[0].x, noeudsToAdd[0].y, noeudsToAdd[0].z]);
                noeuds[noeudsToAdd[0].x, noeudsToAdd[0].y, noeudsToAdd[0].z].explore();

                CreateBacktracking(noeuds[noeudsToAdd[0].x, noeudsToAdd[0].y, noeudsToAdd[0].z]);
            }

            if (!noeuds[noeudsToAdd[1].x, noeudsToAdd[1].y, noeudsToAdd[1].z].explored) {

                noeudsVisite.Add(noeuds[noeudsToAdd[1].x, noeudsToAdd[1].y, noeudsToAdd[1].z]);
                noeuds[noeudsToAdd[1].x, noeudsToAdd[1].y, noeudsToAdd[1].z].explore();


                CreateBacktracking(noeuds[noeudsToAdd[1].x, noeudsToAdd[1].y, noeudsToAdd[1].z]);
            }


            nodeMinWeightLien = node.getMinWLien();
        }


    }

    public void startOutro() {
        SceneManager.LoadScene("Outro");
    }

    private void writeHighscore() {
        if (globalScript.Difficulty == globalScript.EASY) {
            if (globalScript.NbRangees == 5 && globalScript.NbEtages == 5 && globalScript.NbColonnes == 5) {
                if (highscores.score5x5x5 > gameManager.getTimeElapsed()) {
                    highscores.score5x5x5 = gameManager.getTimeElapsed();
                    highscores.score5x5x5_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 5 && globalScript.NbEtages == 10 && globalScript.NbColonnes == 10) {
                if (highscores.score5x10x5 > gameManager.getTimeElapsed()) {
                    highscores.score5x10x5 = gameManager.getTimeElapsed();
                    highscores.score5x10x5_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 10 && globalScript.NbEtages == 5 && globalScript.NbColonnes == 10) {
                if (highscores.score10x5x10 > gameManager.getTimeElapsed()) {
                    highscores.score10x5x10 = gameManager.getTimeElapsed();
                    highscores.score10x5x10_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 10 && globalScript.NbEtages == 10 && globalScript.NbColonnes == 10) {
                if (highscores.score10x10x10 > gameManager.getTimeElapsed()) {
                    highscores.score10x10x10 = gameManager.getTimeElapsed();
                    highscores.score10x10x10_User = globalScript.Username;
                }
            }
        }
        else if (globalScript.Difficulty == globalScript.NORMAL) {
            if (globalScript.NbRangees == 5 && globalScript.NbEtages == 5 && globalScript.NbColonnes == 5) {
                if (highscores.normalScore5x5x5 > gameManager.getTimeElapsed()) {
                    highscores.normalScore5x5x5 = gameManager.getTimeElapsed();
                    highscores.normalScore5x5x5_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 5 && globalScript.NbEtages == 10 && globalScript.NbColonnes == 10) {
                if (highscores.normalScore5x10x5 > gameManager.getTimeElapsed()) {
                    highscores.normalScore5x10x5 = gameManager.getTimeElapsed();
                    highscores.normalScore5x10x5_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 10 && globalScript.NbEtages == 5 && globalScript.NbColonnes == 10) {
                if (highscores.normalScore10x5x10 > gameManager.getTimeElapsed()) {
                    highscores.normalScore10x5x10 = gameManager.getTimeElapsed();
                    highscores.normalScore10x5x10_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 10 && globalScript.NbEtages == 10 && globalScript.NbColonnes == 10) {
                if (highscores.normalScore10x10x10 > gameManager.getTimeElapsed()) {
                    highscores.normalScore10x10x10 = gameManager.getTimeElapsed();
                    highscores.normalScore10x10x10_User = globalScript.Username;
                }
            }
        }
        else if (globalScript.Difficulty == globalScript.HARD) {
            if (globalScript.NbRangees == 5 && globalScript.NbEtages == 5 && globalScript.NbColonnes == 5) {
                if (highscores.hardScore5x5x5 > gameManager.getTimeElapsed()) {
                    highscores.hardScore5x5x5 = gameManager.getTimeElapsed();
                    highscores.hardScore5x5x5_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 5 && globalScript.NbEtages == 10 && globalScript.NbColonnes == 10) {
                if (highscores.hardScore5x10x5 > gameManager.getTimeElapsed()) {
                    highscores.hardScore5x10x5 = gameManager.getTimeElapsed();
                    highscores.hardScore5x10x5_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 10 && globalScript.NbEtages == 5 && globalScript.NbColonnes == 10) {
                if (highscores.hardScore10x5x10 > gameManager.getTimeElapsed()) {
                    highscores.hardScore10x5x10 = gameManager.getTimeElapsed();
                    highscores.hardScore10x5x10_User = globalScript.Username;
                }
            }
            else if (globalScript.NbRangees == 10 && globalScript.NbEtages == 10 && globalScript.NbColonnes == 10) {
                if (highscores.hardScore10x10x10 > gameManager.getTimeElapsed()) {
                    highscores.hardScore10x10x10 = gameManager.getTimeElapsed();
                    highscores.hardScore10x10x10_User = globalScript.Username;
                }
            }
        }

        File.WriteAllText(Application.dataPath + "/3dHighscores.txt", JsonUtility.ToJson(highscores));
    }

    private void readHighscore() {
        if (File.Exists(Application.dataPath + "/3dHighscores.txt")) {
            string json = File.ReadAllText(Application.dataPath + "/3dHighscores.txt");
            highscores = JsonUtility.FromJson<Highscores3D>(json);
        }
        else {
            highscores = new Highscores3D();
        }
    }
}
