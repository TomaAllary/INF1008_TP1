using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class introScript : MonoBehaviour
{
    public GameObject panel2D, panel3D;
    public Image btn3dMode, btn2dMode;
    public Text dropdownHint;
    public Dropdown dropdown;
    public GameObject dmrButton;

    private InputField nbRangees, nbEtages, nbColonnes;
    private string settingsPath;
    private int clock;
    private int incrementeur;
 
    public InputField nameInput;
    public Button storyButton;
    public AudioSource mainAudio, storyAudio;
    public AudioClip audioClipStory;
    private bool storyOn;

    private void Start() {
        settingsPath = Application.dataPath + "/PlayerSettings.txt";

        if (globalScript.ApplicationJustStarted) {
            InitSettings();     //Init saved user's settings
            globalScript.ApplicationJustStarted = false;
        }
        setDifficulty();    //set default difficulty
        open2DMenu();       //open 2d menu by default

        nameInput.text = globalScript.Username;
    }

    private void FixedUpdate()
    {
        if (dmrButton.activeSelf && incrementeur < 9)
        {
            clock++;
            if(clock == 30)
            {
                incrementeur++;
                clock = 0;
                displayDmrButtonText(incrementeur);

            }
        }
    }


    //On passe les valeurs de rangées et de colonnes en variable globale pour permettre à la prochaine scene de les recevoir, et on lance la scène
    public void PlayGame()
    {
        if (panel2D.activeSelf) {

            if (nbRangees.text != "" && nbColonnes.text != "" && nameInput.text != "") {
                globalScript.NbRangees  = int.Parse(nbRangees.text);
                globalScript.NbColonnes = int.Parse(nbColonnes.text);
                globalScript.Username   = nameInput.text;

                globalScript.NbOperations = 0;
                SceneManager.LoadScene("Labyrinthe");
            }
            else {
                if (nbRangees.text == "")
                    nbRangees.image.color = Color.red;
                else
                    nbRangees.image.color = Color.white;

                if (nbColonnes.text == "")
                    nbColonnes.image.color = Color.red;
                else
                    nbColonnes.image.color = Color.white;

                if (nameInput.text == "")
                    nameInput.image.color = Color.red;
                else
                    nameInput.image.color = Color.white;
            }
        }
        else {
            if (nbRangees.text != "" && nbEtages.text != "" && nbColonnes.text != "" && nameInput.text != "") {
                globalScript.NbRangees  = int.Parse(nbRangees.text);
                globalScript.NbEtages   = int.Parse(nbEtages.text);
                globalScript.NbColonnes = int.Parse(nbColonnes.text);
                globalScript.Username   = nameInput.text;

                globalScript.NbOperations = 0;
                SceneManager.LoadScene("Labyrinthe3D");
            }
            else {
                if (nbRangees.text == "")
                    nbRangees.image.color = Color.red;
                else
                    nbRangees.image.color = Color.white;

                if (nbEtages.text == "")
                    nbEtages.image.color = Color.red;
                else
                    nbEtages.image.color = Color.white;

                if (nbColonnes.text == "")
                    nbColonnes.image.color = Color.red;
                else
                    nbColonnes.image.color = Color.white;

                if (nameInput.text == "")
                    nameInput.image.color = Color.red;
                else
                    nameInput.image.color = Color.white;
            }
        }
    }

    public void loadStats()
    {
        SceneManager.LoadScene("Stats");
    }

    public void open3DMenu() {
        btn3dMode.color = new Color32(255, 126, 0, 255);
        btn2dMode.color = new Color32(212, 108, 7, 255);

        panel3D.SetActive(true);
        panel2D.SetActive(false);

        nbRangees = panel3D.transform.Find("InputRangees").GetComponent<InputField>();
        nbEtages = panel3D.transform.Find("InputEtages").GetComponent<InputField>();
        nbColonnes = panel3D.transform.Find("InputColonnes").GetComponent<InputField>();
        nbRangees.text = globalScript.NbRangees.ToString();
        nbEtages.text = globalScript.NbRangees.ToString();
        nbColonnes.text = globalScript.NbRangees.ToString();
    }

    public void open2DMenu() {
        btn2dMode.color = new Color32(255, 126, 0, 255);
        btn3dMode.color = new Color32(212, 108, 7, 255);

        panel2D.SetActive(true);
        panel3D.SetActive(false);

        nbRangees = panel2D.transform.Find("InputRangees").GetComponent<InputField>();
        nbColonnes = panel2D.transform.Find("InputColonnes").GetComponent<InputField>();
        //nbRangees.text = globalScript.NbRangees.ToString();
        //nbColonnes.text = globalScript.NbRangees.ToString();
    }

    public void setDifficulty() {
        globalScript.Difficulty = dropdown.value;

        if (dropdown.value == globalScript.EASY)
            dropdownHint.text = "- Mini-map avec Timmy -";
        else if(dropdown.value == globalScript.NORMAL)
            dropdownHint.text = "- Mini-map sans Timmy -";
        else
            dropdownHint.text = "-  Sans Mini-map -";

    }

    public void quickLaunch(string dim) {
        string[] dims = dim.Split('x');
        globalScript.NbColonnes = int.Parse(dims[0]);
        globalScript.NbRangees = int.Parse(dims[1]);

        globalScript.NbOperations = 0;
        SceneManager.LoadScene("Labyrinthe");
    }

    public void Quit() {
        Application.Quit();
    }

    private void OnApplicationQuit() {
        string settings = globalScript.Username + "\n" +
                    globalScript.MusicVolume.ToString() + "\n" +
                    globalScript.MinimapSize.ToString();

        File.WriteAllText(settingsPath, settings);
    }

    private void InitSettings() {
        if (File.Exists(settingsPath)) {
            string[] userSetting = File.ReadAllLines(settingsPath);
            globalScript.Username = userSetting[0];
            globalScript.MusicVolume = float.Parse(userSetting[1]);
            globalScript.MinimapSize = float.Parse(userSetting[2]);

        }
    }

    public void downloadMoreRam()
    {
        Application.OpenURL("https://downloadmoreram.com/");
        for (int i = 2; i < 10; i++)
        {
            dmrButton.transform.GetChild(i).gameObject.SetActive(false);
        }
        dmrButton.SetActive(false);
    }

    public void dmrButtonShow()
    {
        dmrButton.SetActive(true);
        clock = 0;
        incrementeur = 1;
       
    }

    private void displayDmrButtonText(int textNumber)
    {        
        dmrButton.transform.GetChild(textNumber).gameObject.SetActive(true);
    }

    public void storyButtonClick()
    {
        if (!storyOn)
        {
            mainAudio.volume = .1f;
            storyAudio.PlayOneShot(audioClipStory);
            storyOn = true;
            StartCoroutine(WaitForStory());
        }
        else
        {
            storyAudio.Stop();
            mainAudio.volume = 1f;
            storyOn = false;
        }
    }

    //Waits for the story audio to be done and sets the main volume to 1
    IEnumerator WaitForStory()
    {
        yield return new WaitUntil(() => storyAudio.isPlaying == false);
        mainAudio.volume = 1.0f;
        storyOn = false;
    }

}
