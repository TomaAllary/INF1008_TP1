using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager3D : MonoBehaviour
{
    public GameObject tooglingMenu;
    public Text timeLabel;
    public Slider volumeSlider;
    public Slider mapSizeSlider;
    public RectTransform minimap;

    public GameObject upHint;
    public GameObject downHint;

    private Text MazeDimsLb;
    private PlayerMovement3D player;
    private AudioSource ambiance;

    private float time;
    // Start is called before the first frame update
    void Start() {
        time = 0.0f;

        Transform panel = transform.Find("Panel");

        ambiance = GameObject.Find("Ambiance").transform.GetChild(0).GetComponent<AudioSource>();
        volumeSlider.value = ambiance.volume = globalScript.MusicVolume;
        mapSizeSlider.value = globalScript.MinimapSize;
        sizeMinimap();

        MazeDimsLb              = panel.Find("MazeDims").GetComponent<Text>();
        MazeDimsLb.text = "Largeur: " + globalScript.NbColonnes.ToString() + " Hauteur: " + globalScript.NbRangees.ToString();
    }

    // Update is called once per frame
    void Update() {
        if (player == null) {
            player = GameObject.Find("Timmy(Clone)").GetComponent<PlayerMovement3D>();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            tooglingMenu.SetActive(!tooglingMenu.activeSelf);
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
        ambiance.volume = volumeSlider.value;
        globalScript.MusicVolume = volumeSlider.value;
    }

    public void sizeMinimap() {
        minimap.localScale = Vector3.one * mapSizeSlider.value;
        minimap.anchoredPosition = new Vector2(-96.5f * mapSizeSlider.value, -95f * mapSizeSlider.value);

        globalScript.MinimapSize = mapSizeSlider.value;
    }

    public void restart() {
        SceneManager.LoadScene("Labyrinthe3D");
    }

    public void backToMenu() {
        SceneManager.LoadScene("Intro");
    }
}
