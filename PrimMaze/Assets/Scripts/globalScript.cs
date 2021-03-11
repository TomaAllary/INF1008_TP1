using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class globalScript
{
    public const int SUD = 0;
    public const int OUEST = 1;
    private static int nbRangees = 10;
    private static int nbColonnes = 20;

    public static int NbColonnes { get => nbColonnes; set => nbColonnes = value; }
    public static int NbRangees { get => nbRangees; set => nbRangees = value; }
}
