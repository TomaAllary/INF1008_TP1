using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour
{

    public Text scoresTxt;
    public AudioSource music;

    private Highscores highscores;
    private Highscores3D highscores3d;
    // Start is called before the first frame update
    void Start() {
        music.volume = globalScript.MusicVolume;

        if (File.Exists(Application.dataPath + "/2dHighscores.txt")) {
            string json = File.ReadAllText(Application.dataPath + "/2dHighscores.txt");
            highscores = JsonUtility.FromJson<Highscores>(json);
        }
        else {
            highscores = new Highscores();
        }

        if (File.Exists(Application.dataPath + "/3dHighscores.txt")) {
            string json = File.ReadAllText(Application.dataPath + "/3dHighscores.txt");
            highscores3d = JsonUtility.FromJson<Highscores3D>(json);
        }
        else {
            highscores3d = new Highscores3D();
        }

        scoresTxt.GetComponent<Text>().text = highscores.ToString() + "\n-----------------------------------------\n" + highscores3d.ToString();
    }

    // Update is called once per frame
    public void retourIntro() {
        SceneManager.LoadScene("Intro");
    }
}
