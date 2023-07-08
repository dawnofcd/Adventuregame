using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrl : MonoBehaviour
{
    public inputManager inputManager;
      public static PlayerCrl instance;
    public Movemanager moveManager;
    private void Awake()
    {
         inputManager = GetComponent<inputManager>();
         moveManager = GetComponent<Movemanager>();
        PlayerCrl.instance = this;
    }

}
