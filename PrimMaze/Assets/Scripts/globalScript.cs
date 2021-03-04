using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class globalScript
{
    private static int nbRangees;
    private static int nbColonnes;

    public static int NbColonnes { get => nbColonnes; set => nbColonnes = value; }
    public static int NbRangees { get => nbRangees; set => nbRangees = value; }
}
