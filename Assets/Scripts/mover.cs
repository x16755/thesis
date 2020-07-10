﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class mover : MonoBehaviour
{

    private Level2 Level2;
    private ResponseTime ResponseTime;
    private Variables Variables;
    public float DistanceZ = 0.0f; //This variable tells the stimulus how much to move.
    public float Stimpos = 0.0f; // This variable holds the PI number. You can change this in the inspector after adding it as a component for each stimulus.
    public float angle;
    public float Shoulder;
    public float DistanceY;
    Vector3 tempPos; // Needed for moving the stimulus.

    
    void Awake()
    {
        Variables = GameObject.FindGameObjectWithTag("UI").GetComponent<Variables>(); //This pulls the variables from the variables script. It's a handy way to call variables from objects without manualy telling unity where to look for the corisponding script every time a new scene loads. 
    }

    private void Start()
    {
        Shoulder = Variables.shoulder; 
        DistanceZ = Stimpos * Variables.arm;
       

    }

    void Update()
    {
        
        tempPos = transform.position;
        tempPos.z = DistanceZ;       
        tempPos.y = Shoulder + (DistanceZ * (Mathf.Tan(angle))/2);
        Debug.Log(tempPos.y);
        transform.position = tempPos; // This does the actual stimulus moving.

    }
}