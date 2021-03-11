using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class globalScript
{
    public const int SUD = 0;
    public const int OUEST = 1;
    public const int BAS = 2;
    private static int nbRangees = 10;
    private static int nbColonnes = 20;
    private static int nbEtages = 20;

    public static int NbColonnes { get => nbColonnes; set => nbColonnes = value; }
    public static int NbEtages { get => nbEtages; set => nbEtages = value; }
    public static int NbRangees { get => nbRangees; set => nbRangees = value; }
}
