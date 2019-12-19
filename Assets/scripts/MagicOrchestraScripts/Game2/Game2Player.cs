﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Player : MonoBehaviour
{
    //Singleton of the SequenceShower class
    public static Game2Player singleton = null;
    private GameObject passaPorta = null;

    public bool isRecognitionEnabled = false;



    /* <summary>
    * The function is called when the component is instantiated
    * </summary>
    */
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

    /* <summary>
     * Start is called before the first frame update
     * </summary>
     */
    void Start()
    {
        // Looking for the magic ball
        this.passaPorta = GameObject.Find("Passaporta");

        if (this.passaPorta == null)
        {
            Debug.Log("Some problem with \"Passaporta\"");
            return;
        }

        // Enabling RFID reader
        MagicRoomSmartToyManager.instance.openEventChannelSmartToy("Passaporta");
        MagicRoomSmartToyManager.instance.openStreamSmartToy("Passaporta", 5.0f);
        passaPorta.GetComponent<SmartToy>().switchOnRFIDSensor();
    }

    /* <summary>
    * Update is called once per frame
    * </summary>
    */
    public void Update()
    {
        // Checking recognition status
        if (!(isRecognitionEnabled))
            return;

        // TODO: vediamo se arriva fino a qui        
        Debug.Log("Passaporta enabled?" + this.passaPorta.GetComponent<RFIDReader>().sensorEnabled);
        
        Debug.Log("Last read:" + passaPorta.GetComponent<RFIDReader>().lastread);
        // passaPorta.GetComponent<RFIDReader>().updateState();
        return;

        if (this.passaPorta.GetComponent<RFIDReader>().lastread != null)
            this.RecievedNumber(int.Parse(this.passaPorta.GetComponent<RFIDReader>().lastread));
    }

    /* <summary>
    * Sends the recieved number to the Game2 Controller
    * </summary>
    */
    void RecievedNumber(int number)
    {

        if (!(isRecognitionEnabled))
            return;

        UserDigitSpan.singleton.SelectNumber(number);
    }


    public void LightOfCube()
    {
        Debug.Log("FEEDBACK: Light of the cube");
    }
}