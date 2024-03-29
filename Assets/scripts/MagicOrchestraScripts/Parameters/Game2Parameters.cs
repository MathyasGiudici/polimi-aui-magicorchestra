﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game2Parameters
{
    private static int difficulty;
    private static int[] sequence;
    private static bool isReverse, isHintMode, isShuffle, confirmSound;
    private static float timeInShowing;

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

    public static bool IsHintMode
    {
        get
        {
            return isHintMode;
        }
        set
        {
            isHintMode = value;
        }
    }

    public static bool IsShuffle
    {
        get
        {
            return isShuffle;
        }
        set
        {
            isShuffle = value;
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

        toReturn += ("Sequenza: " + MagicOrchestraUtils.StringifySequence(Sequence) + "\n");
        toReturn += ("Difficoltà: " + Difficulty + "\n");
        toReturn += ("Modalità ordine inverso: " + MagicOrchestraUtils.TrueFalseConverter(IsReverse) + "\n");
        toReturn += ("Tempo proiezione frontale: " + TimeInShowing + MagicOrchestraUtils.SecondsTextItalianSuffix(TimeInShowing) + "\n");
        if (MagicOrchestraParameters.IsContext)
        {
            toReturn += ("Modalità aiuto: " + MagicOrchestraUtils.TrueFalseConverter(isHintMode) + "\n");

            if (IsHintMode)
                toReturn += ("Modalità carte randomiche: " + MagicOrchestraUtils.TrueFalseConverter(isShuffle) + "\n");
        }
        toReturn += ("Suono di conferma: " + MagicOrchestraUtils.TrueFalseConverter(ConfirmSound) + "\n");

        return toReturn;
    }


    public static void LogMe()
    {
        Debug.Log("Sequenza: " + MagicOrchestraUtils.StringifySequence(Sequence));
        Debug.Log("Difficulty: " + Difficulty);
        Debug.Log("Modalità ordine inverso: " + IsReverse);
        Debug.Log("TimeInShowing: " + TimeInShowing);
        if (MagicOrchestraParameters.IsContext)
        {
            Debug.Log("Modalità Aiuto: " + isHintMode);
            Debug.Log("Modalità Shuffle: " + isShuffle);
        }
        Debug.Log("Suono di conferma:" + ConfirmSound);
    }
}
