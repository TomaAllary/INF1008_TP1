using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Highscores3D {
    public float score5x5x5;
    public float score5x10x5;
    public float score10x5x10;
    public float score10x10x10;

    public float normalScore5x5x5;
    public float normalScore5x10x5;
    public float normalScore10x5x10;
    public float normalScore10x10x10;

    public float hardScore5x5x5;
    public float hardScore5x10x5;
    public float hardScore10x5x10;
    public float hardScore10x10x10;

    public string score5x5x5_User;
    public string score5x10x5_User;
    public string score10x5x10_User;
    public string score10x10x10_User;

    public string normalScore5x5x5_User;
    public string normalScore5x10x5_User;
    public string normalScore10x5x10_User;
    public string normalScore10x10x10_User;

    public string hardScore5x5x5_User;
    public string hardScore5x10x5_User;
    public string hardScore10x5x10_User;
    public string hardScore10x10x10_User;

    public Highscores3D() {


        score5x5x5          = score5x10x5       = score10x5x10          = score10x10x10         = 
        normalScore5x5x5    = normalScore5x10x5 = normalScore10x5x10    = normalScore10x10x10   = 
        hardScore5x5x5      = hardScore5x10x5   = hardScore10x5x10      = hardScore10x10x10     = float.MaxValue;

        score5x5x5_User         = score5x10x5_User          = score10x5x10_User         = score10x10x10_User        =
        normalScore5x5x5_User   = normalScore5x10x5_User    = normalScore10x5x10_User   = normalScore10x10x10_User  =
        hardScore5x5x5_User     = hardScore5x10x5_User      = hardScore10x5x10_User     = hardScore10x10x10_User    = "valeur max";
    }

    public override string ToString() {
        return
            "Scores mode 3D EASY: \n" +
            "5x5x5 - Temps: " + score5x5x5.ToString() + "   Fait par " + score5x5x5_User + "\n" +
            "5x10x5 - Temps: " + score5x10x5.ToString() + "   Fait par " + score5x10x5_User + "\n" +
            "10x5x10 - Temps: " + score10x5x10.ToString() + "   Fait par " + score10x5x10_User + "\n" +
            "10x10x10 - Temps: " + score10x10x10.ToString() + "   Fait par " + score10x10x10_User + "\n" +

            "\nScores mode 3D NORMAL: \n" +
            "5x5x5 - Temps: " + normalScore5x5x5.ToString() + "   Fait par " + normalScore5x5x5_User + "\n" +
            "5x10x5 - Temps: " + normalScore5x10x5.ToString() + "   Fait par " + normalScore5x10x5_User + "\n" +
            "10x5x10 - Temps: " + normalScore10x5x10.ToString() + "   Fait par " + normalScore10x5x10_User + "\n" +
            "10x10x10 - Temps: " + normalScore10x10x10.ToString() + "   Fait par " + normalScore10x10x10_User + "\n" +

            "\nScores mode 3D HARD: \n" +
            "5x5x5 - Temps: " + hardScore5x5x5.ToString() + "   Fait par " + hardScore5x5x5_User + "\n" +
            "5x10x5 - Temps: " + hardScore5x10x5.ToString() + "   Fait par " + hardScore5x10x5_User + "\n" +
            "10x5x10 - Temps: " + hardScore10x5x10.ToString() + "   Fait par " + hardScore10x5x10_User + "\n" +
            "10x10x10 - Temps: " + hardScore10x10x10.ToString() + "   Fait par " + hardScore10x10x10_User + "\n";
    }
}
