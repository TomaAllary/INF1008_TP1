using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{

    public GameObject tooglingMenu;
    public Text timeLabel;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            tooglingMenu.SetActive(!tooglingMenu.activeSelf);

        if (!tooglingMenu.activeSelf) {
            time += Time.deltaTime;
            timeLabel.text = "Time: " + time.ToString();
        }
    }
}
