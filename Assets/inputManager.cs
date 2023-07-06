using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{
    public static inputManager Instance;
    public float Move;
    public float MoveJump;
    public int Numberjump;
    
 
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
       if( Input.GetKeyDown("space"))
        {
            MoveJump = 5;
            Numberjump++;
        }
        if (Input.GetKeyUp("space"))
        {
            MoveJump = 0f;
            Numberjump = 0;
        }
        
       
    }
}
