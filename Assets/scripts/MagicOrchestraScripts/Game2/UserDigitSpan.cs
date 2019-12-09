﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDigitSpan : MonoBehaviour
{
    //Singleton of the UserDigitSpan class
    public static UserDigitSpan singleton = null;

    //Private setting variables
    private int[] sequence;
    private bool isReverse;

    private int currentIndex;

    void Awake()
    {
        //Code to manage the singleton uniqueness
        if ((singleton != null) && (singleton != this))
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
    }

    public void Init(int[] sequence, bool isReverse)
    {
        this.sequence = sequence;
        this.isReverse = isReverse;

        if (isReverse)
            this.currentIndex = sequence.Length - 1;
        else
            this.currentIndex = 0;
    }

    public void SelectNumber(int number)
    {
        if (sequence[currentIndex] != number)
        {
            SequenceShower.singleton.ShowEndMessage(false);
            return;
        }

        if (isReverse)
        {
            currentIndex = currentIndex - 1;

            if (currentIndex < 0)
                SequenceShower.singleton.ShowEndMessage(true);
        }
        else
        {
            currentIndex = currentIndex + 1;

            if(currentIndex >= sequence.Length)
                SequenceShower.singleton.ShowEndMessage(true);
        }
    }

}