using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController S;
    
    [Header("Set Dynamically")]
    public int countDarkBlocks = 0;
    public int countLightBlocks = 0;

    private void Awake()
    {
        S = this;
    }

    private void Start()
    {
        print("dark blocks = " + countDarkBlocks);
    }

    private void LateUpdate()
    {
        if (countLightBlocks == countDarkBlocks)
        {
            Debug.Log("Level passed!");
        }
    }
}
