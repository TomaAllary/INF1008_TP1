using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OutroScript : MonoBehaviour
{
    public List<Image> images;
    public float fadeTimePdf = 10f;
    public float fadeTimeMeme = 10f;
    public float scrollSpeed = 1.0f;
    public RectTransform pdf;
    public Image meme;
    public AudioSource outro;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        globalScript.gameOver = false;

        Time.timeScale = 1;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        //scroll
        pdf.position += Vector3.up * scrollSpeed * Time.deltaTime;

        //audio fade in
        if(time < 80f) {
            outro.volume = (time / 80f);
        }

        if (time < fadeTimePdf) {
            //fade pdf
            byte rgbValue = Convert.ToByte( (int)(200 * (time / fadeTimePdf)) );
            foreach (Image image in images) {
                image.color = new Color32(rgbValue, rgbValue, rgbValue, 255);
            }
        }
        if (time < fadeTimeMeme) {
            //fade meme
            byte rgbValue = Convert.ToByte((int)(200 * (time / fadeTimeMeme)));
            meme.color = new Color32(255, 255, 255, rgbValue);
        }
        if(pdf.anchoredPosition.y > 6400) {
            //end and change scene
            SceneManager.LoadScene("Intro");
        }
    }


    public void skip() {
        SceneManager.LoadScene("Intro");
    }
}
