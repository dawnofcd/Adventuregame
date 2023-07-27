using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrl : MonoBehaviour
{
    public dataPlayer dataPlayer;
    public inputManager inputManager;
    public static PlayerCrl instance;
    public Movemanager moveManager;
    
    private void Awake()
    {
        dataPlayer = GetComponent<dataPlayer>();
        inputManager = GetComponent<inputManager>();
        moveManager = GetComponent<Movemanager>();
        PlayerCrl.instance = this;
    }

}
