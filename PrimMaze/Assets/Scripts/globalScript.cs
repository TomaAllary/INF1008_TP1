using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class globalScript
{
    public const int SUD = 0;
    public const int OUEST = 1;
    public const int BAS = 2;

    public const int HARD = 2;
    public const int NORMAL = 1;
    public const int EASY = 0;

    private static int nbRangees = 3;
    private static int nbColonnes = 3;
    private static int nbEtages = 5;
    private static string username = "guest";
    private static bool musicOn = true;
    private static int difficulty = EASY;
    private static int nbOperations = 0;

    public static int NbColonnes { get => nbColonnes; set => nbColonnes = value; }
    public static int NbEtages { get => nbEtages; set => nbEtages = value; }
    public static int NbRangees { get => nbRangees; set => nbRangees = value; }
    public static bool Music { get => musicOn; set => musicOn = value; }
    public static int Difficulty { get => difficulty; set => difficulty = value; }
    public static string Username { get => username; set => username = value; }
    public static int NbOperations { get => nbOperations; set => nbOperations = value; }
}
