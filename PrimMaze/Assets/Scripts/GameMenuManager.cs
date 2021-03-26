using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public GameObject tooglingMenu;
    public Text timeLabel;
    public Slider volumeSlider;
    public Slider mapSizeSlider;
    public RectTransform minimap;

    private Text MazeDimsLb;
    private AudioSource ambiance;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        Transform panel = transform.Find("Panel");

        ambiance                = GameObject.Find("Ambiance").transform.GetChild(0).GetComponent<AudioSource>();
        volumeSlider.value = ambiance.volume = globalScript.MusicVolume;
        mapSizeSlider.value = globalScript.MinimapSize;
        sizeMinimap();

        MazeDimsLb = panel.Find("MazeDims").GetComponent<Text>();
        MazeDimsLb.text = "Largeur: " + globalScript.NbColonnes.ToString() + " Hauteur: " + globalScript.NbRangees.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            tooglingMenu.SetActive(!tooglingMenu.activeSelf);
        }

        if (tooglingMenu.activeSelf) {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            time += Time.deltaTime;
            timeLabel.text = "Time: " + time.ToString();
        }
    }


    public void toogleMusic() {
        ambiance.volume = volumeSlider.value;
        globalScript.MusicVolume = volumeSlider.value;
    }
    public void sizeMinimap() {
        minimap.localScale = Vector3.one * mapSizeSlider.value;
        minimap.anchoredPosition = new Vector2(-96.5f * mapSizeSlider.value, -95f * mapSizeSlider.value);

        globalScript.MinimapSize = mapSizeSlider.value;
    }


    public void restart() {
        globalScript.NbOperations = 0;
        globalScript.NbGenration = 0;
        SceneManager.LoadScene("Labyrinthe");
    }

    public void backToMenu() {
        SceneManager.LoadScene("Intro");
        globalScript.NbOperations = 0;
        globalScript.NbGenration = 0;
    }


}
