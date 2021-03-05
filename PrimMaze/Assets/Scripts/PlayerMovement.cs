using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 20.00f;
    public float xRange = 100.00f;
    public float zRange = 100.00f;
    public Animator animator;

    public GameObject gameMenu;

    public bool menuActive;

    public AudioSource karenAudio;
    public AudioClip karenPas;

    private int tickPace;
    
    // Start is called before the first frame update
    void Start()
    {

        menuActive = false;
        tickPace++;
    }

    // Update is called once per frame
    void Update()
    {


        

    }

    private void FixedUpdate()
    {

    }


}
