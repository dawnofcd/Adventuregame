using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerreceivedam : MonoBehaviour
{
    public List<GameObject> heath= new List<GameObject>();
    private void Start()
    {
            heath.Add(GameObject.Find("uiheath_1"));
            heath.Add(GameObject.Find("uiheath_2"));
            heath.Add(GameObject.Find("uiheath_3"));  
    }
    void CaculatorHeath()
    {
        if (PlayerCrl.instance.dataPlayer.currentHp<=0)
        {
             PlayerCrl.instance.dataPlayer.currentheath -= 1;
            if (PlayerCrl.instance.dataPlayer.currentheath==0)
            {
                Die();
            }   
        }    
        
    }
    void Die()
    {

    }    
}
