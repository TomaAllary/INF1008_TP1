using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
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

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;

        Transform panel = transform.Find("Panel");

        operationNodeInitLb     = panel.Find("operationNodeInit").GetComponent<Text>();
        operationLinkInitLb     = panel.Find("operationLinkInit").GetComponent<Text>();
        operationNbGenMazeLb    = panel.Find("operationNbGenMaze").GetComponent<Text>();
        operationNbReadMazeLb   = panel.Find("operationNbReadMaze").GetComponent<Text>();
        MazeDimsLb              = panel.Find("MazeDims").GetComponent<Text>();

        MazeDimsLb.text = "Largeur: " + globalScript.NbColonnes.ToString() + " Hauteur: " + globalScript.NbRangees.ToString();

        operationNodeInit = 0 ;
        operationLinkInit = 0;
        operationNbGenMaze = 0;
        operationNbReadMaze = 0;
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            tooglingMenu.SetActive(!tooglingMenu.activeSelf);

            //Refresh values
            operationNodeInitLb.text    = "Nombre de noeud initialisés: " + operationNodeInit.ToString();
            operationLinkInitLb.text    = "Nombre de liens initialisés: " + operationLinkInit.ToString();
            operationNbGenMazeLb.text   = "Nombre d'opérations pour la génération: " + operationNbGenMaze.ToString();
            operationNbReadMazeLb.text  = "Nombre d'opérations pour la lecture: " + operationNbReadMaze.ToString();
        }

        if (tooglingMenu.activeSelf) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
            time += Time.deltaTime;
            timeLabel.text = "Time: " + time.ToString();
        }
    }
}
