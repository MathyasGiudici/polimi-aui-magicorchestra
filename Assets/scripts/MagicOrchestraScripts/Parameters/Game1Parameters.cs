﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game1Parameters {

    private static int difficulty;
    private static float timeInShowing;
    private static bool isGestureMode, confirmSound;

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
    public static float TimeInShowing
    {
        get
        {
            return timeInShowing;
        }
        set
        {
            timeInShowing = value;
        }
    }

    public static bool ConfirmSound
    {
        get
        {
            return confirmSound;
        }
        set
        {
            confirmSound = value;
        }
    }

    public static string StringifyMe()
    {
        string toReturn = "";

        toReturn += ("Difficoltà: " + Difficulty + "\n");
        toReturn += ("Tempo proiezione frontale: " + TimeInShowing + " secondi\n");
        toReturn += ("Suono di conferma: " + MagicOrchestraUtils.TrueFalseConverter(ConfirmSound) + "\n");

        return toReturn;
    }


    public static void LogMe()
    {
        Debug.Log("Difficulty: " + Difficulty);
        Debug.Log("TimeInShowing: " + TimeInShowing);
        Debug.Log("Suono di conferma:" + ConfirmSound);
    }

}
