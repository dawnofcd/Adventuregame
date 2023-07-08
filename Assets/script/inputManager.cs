using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{
    
    public float MoveX;
    public bool Jumpkeydown;
    public bool Jumkey;
 
    

    private void Update()
    {
        InputkeyX();
        InputJump();
    }

    public void InputkeyX()
    {
        MoveX = Input.GetAxis("Horizontal");
         
    }
    public void InputJump()
    {
        Jumpkeydown = Input.GetKeyDown(KeyCode.Space);
        Jumkey = Input.GetKey(KeyCode.Space);
    }
}
