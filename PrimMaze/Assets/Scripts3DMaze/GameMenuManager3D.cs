using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager3D : MonoBehaviour
{
    public static int operationNodeInit;
    public static int operationLinkInit;
    public static int operationNbGenMaze;
    public static int operationNbReadMaze;

    public GameObject tooglingMenu;
    public Text timeLabel;

    private Text operationNodeInitLb;
    private Text operationLinkInitLb;
    private Text operationNbGenMazeLb;
    private Text operationNbReadMazeLb;
    private Text MazeDimsLb;
    public GameObject upHint;
    public GameObject downHint;
    public PlayerMovement3D player;
    private Text musicBtn;
    private GameObject ambiance;

    private float time;
    // Start is called before the first frame update
    void Start() {
        time = 0.0f;

        Transform panel = transform.Find("Panel");

        ambiance                = GameObject.Find("Ambiance");
        musicBtn                = panel.Find("MusicBtn").transform.GetChild(0).GetComponent<Text>();
        operationNodeInitLb     = panel.Find("operationNodeInit").GetComponent<Text>();
        operationLinkInitLb     = panel.Find("operationLinkInit").GetComponent<Text>();
        operationNbGenMazeLb    = panel.Find("operationNbGenMaze").GetComponent<Text>();
        operationNbReadMazeLb   = panel.Find("operationNbReadMaze").GetComponent<Text>();
        MazeDimsLb              = panel.Find("MazeDims").GetComponent<Text>();

        MazeDimsLb.text = "Largeur: " + globalScript.NbColonnes.ToString() + " Hauteur: " + globalScript.NbRangees.ToString();

        operationNodeInit = 0;
        operationLinkInit = 0;
        operationNbGenMaze = 0;
        operationNbReadMaze = 0;
        if (ambiance.activeSelf != globalScript.Music)
            toogleMusic();
    }

    // Update is called once per frame
    void Update() {
        if (player == null) {
            player = GameObject.Find("Timmy(Clone)").GetComponent<PlayerMovement3D>();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            tooglingMenu.SetActive(!tooglingMenu.activeSelf);

            //Refresh values
            operationNodeInitLb.text = "Nombre de noeud initialisés: " + operationNodeInit.ToString();
            operationLinkInitLb.text = "Nombre de liens initialisés: " + operationLinkInit.ToString();
            operationNbGenMazeLb.text = "Nombre d'opérations pour la génération: " + operationNbGenMaze.ToString();
            operationNbReadMazeLb.text = "Nombre d'opérations pour la lecture: " + operationNbReadMaze.ToString();
        }

        if (tooglingMenu.activeSelf) {
            upHint.SetActive(false);
            downHint.SetActive(false);

            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            upHint.SetActive(player.canGoUp);
            downHint.SetActive(player.canGoDown);

            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            time += Time.deltaTime;
            timeLabel.text = "Time: " + time.ToString();
        }



    }

    public void toogleMusic() {
        ambiance.SetActive(!ambiance.activeSelf);
        if (ambiance.activeSelf)
            musicBtn.text = "Musique: ouverte";
        else
            musicBtn.text = "Musique: fermée";

        globalScript.Music = ambiance.activeSelf;

    }

    public void restart() {
        SceneManager.LoadScene("Labyrinthe3D");
    }

    public void backToMenu() {
        SceneManager.LoadScene("Intro");
    }
}
