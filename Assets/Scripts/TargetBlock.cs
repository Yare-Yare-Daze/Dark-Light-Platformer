using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBlock : MonoBehaviour
{
    public enum eState
    {
        dark,
        light,
        freez
    }

    [Header("Set Dynamically")]
    public bool isLight = false;

    private eState _eState;
    
    private GameObject _darkGO;
    private GameObject _lightGO;
    

    private void Start()
    {
        GameController.S.countDarkBlocks += 1;
        _darkGO = transform.GetChild(0).gameObject;
        _lightGO = transform.GetChild(1).gameObject;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isLight = true;
            GameController.S.countLightBlocks += 1;
            _darkGO.SetActive(false);
            _lightGO.SetActive(true);
        }
    }
}
