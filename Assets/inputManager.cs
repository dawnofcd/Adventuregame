using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{
    public static inputManager Instance;
    public float Move;

   
    
 
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Inputkey();
    }

    public void Inputkey()
    {
        Move = Input.GetAxis("Horizontal");
         
    }
}
