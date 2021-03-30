using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Highscores
{
    public float score5x5;
    public float score5x10;
    public float score10x10;
    public float score20x20;
    public float score40x25;

    public float normalScore5x5;
    public float normalScore5x10;
    public float normalScore10x10;
    public float normalScore20x20;
    public float normalScore40x25;

    public float hardScore5x5;
    public float hardScore5x10;
    public float hardScore10x10;
    public float hardScore20x20;
    public float hardScore40x25;

    public string score5x5_User;
    public string score5x10_User;
    public string score10x10_User;
    public string score20x20_User;
    public string score40x25_User;

    public string normalScore5x5_User;
    public string normalScore5x10_User;
    public string normalScore10x10_User;
    public string normalScore20x20_User;
    public string normalScore40x25_User;

    public string hardScore5x5_User;
    public string hardScore5x10_User;
    public string hardScore10x10_User;
    public string hardScore20x20_User;
    public string hardScore40x25_User;

    public Highscores() {
        score5x5        = score5x10         = score20x20        = score10x10        = score40x25        =
        normalScore5x5  = normalScore5x10   = normalScore20x20  = normalScore10x10  = normalScore40x25  = 
        hardScore5x5    = hardScore5x10     = hardScore20x20    = hardScore10x10    = hardScore40x25    = float.MaxValue;

        score5x5_User       = score5x10_User        = score20x20_User       = score10x10_User       = score40x25_User       =
        normalScore5x5_User = normalScore5x10_User  = normalScore20x20_User = normalScore10x10_User = normalScore40x25_User =
        hardScore5x5_User   = hardScore5x10_User    = hardScore20x20_User   = hardScore10x10_User   = hardScore40x25_User   = "valeur max";
    }

    public override string ToString() {
        return
            "Scores mode 2D EASY: \n" +
            "5x5 - Temps: " + score5x5.ToString() + "   Fait par " + score5x5_User + "\n" +
            "5x10 - Temps: " + score5x10.ToString() + "   Fait par " + score5x10_User + "\n" +
            "10x10 - Temps: " + score10x10.ToString() + "   Fait par " + score10x10_User + "\n" +
            "20x20 - Temps: " + score20x20.ToString() + "   Fait par " + score20x20_User + "\n" +
            "20x25 - Temps: " + score40x25.ToString() + "   Fait par " + score40x25_User + "\n" +

            "\nScores mode 2D NORMAL: \n" +
            "5x5 - Temps: " + normalScore5x5.ToString() + "   Fait par " + normalScore5x5_User + "\n" +
            "5x10 - Temps: " + normalScore5x10.ToString() + "   Fait par " + normalScore5x10_User + "\n" +
            "10x10 - Temps: " + normalScore10x10.ToString() + "   Fait par " + normalScore10x10_User + "\n" +
            "20x20 - Temps: " + normalScore20x20.ToString() + "   Fait par " + normalScore20x20_User + "\n" +
            "20x25 - Temps: " + normalScore40x25.ToString() + "   Fait par " + normalScore40x25_User + "\n" +

            "\nScores mode 2D HARD: \n" +
            "5x5 - Temps: " + hardScore5x5.ToString() + "   Fait par " + hardScore5x5_User + "\n" +
            "5x10 - Temps: " + hardScore5x10.ToString() + "   Fait par " + hardScore5x10_User + "\n" +
            "10x10 - Temps: " + hardScore10x10.ToString() + "   Fait par " + hardScore10x10_User + "\n" +
            "20x20 - Temps: " + hardScore20x20.ToString() + "   Fait par " + hardScore20x20_User + "\n" +
            "20x25 - Temps: " + hardScore40x25.ToString() + "   Fait par " + hardScore40x25_User + "\n";
    }

}
