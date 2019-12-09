﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game2Parameters
{
    private static int difficulty;
    private static int[] sequence;
    private static bool isReverse;

    public static int Difficulty
    {
        get
        {
            return difficulty;
        }
        set
        {
            difficulty = value;
        }
    }

    public static int[] Sequence
    {
        get
        {
            return sequence;
        }
        set
        {
            sequence = value;
        }
    }

    public static bool IsReverse
    {
        get
        {
            return isReverse;
        }
        set
        {
            isReverse = value;
        }
    }

    public static string StringifyMe()
    {
        string toReturn = "";

        toReturn += ("Sequenza: " + MagicOrchestraUtils.StringifySequence(Sequence) + "\n");
        toReturn += ("Difficoltà: " + Difficulty + "\n");
        toReturn += ("Modalità Reverse: " + IsReverse + "\n");

        return toReturn;
    }


    public static void LogMe()
    {
        Debug.Log("Sequenza: " + MagicOrchestraUtils.StringifySequence(Sequence));
        Debug.Log("Difficulty: " + Difficulty);
        Debug.Log("Modalità Reverse: " + IsReverse);
    }
}