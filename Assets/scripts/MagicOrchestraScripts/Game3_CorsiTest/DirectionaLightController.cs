﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionaLightController : MonoBehaviour
{
    public Color color = new Color(255,255,255);
    public float intensity = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
		//ChangeColorAndIntensity();
    }

    // Update is called once per frame
    void Update()
    {
		//ChangeColorAndIntensity();
	}

    private void ChangeColorAndIntensity()
	{
        if (MagicOrchestraParameters.IsContext)
        {
            gameObject.GetComponent<Light>().color = new Color(255, 255, 255);
            gameObject.GetComponent<Light>().intensity = 1.0f;
        }
        else
        {
            gameObject.GetComponent<Light>().color = color;
            gameObject.GetComponent<Light>().intensity = intensity;
        }
	}
}