using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Playerreceivedam : MonoBehaviour
{
    public List<GameObject> heath= new List<GameObject>();
    public static Playerreceivedam instance;
    public Player player;
    private void Start()
    {      instance=this;
            heath.Add(GameObject.Find("uiheath_1"));
            heath.Add(GameObject.Find("uiheath_2"));
            heath.Add(GameObject.Find("uiheath_3"));  
    }
   public void CaculatorHp(ref int currentheath, int maxheath,ref int Hp, int maxhp)
    {
       
        if (Hp < 0)
    {
        currentheath--;
        Hp = maxhp;
    }
    else if (currentheath >= 3)
    {
        currentheath = maxheath;
    }
    else if (currentheath < 0)
    {
        Hp = 0;
    }

    heathstate(currentheath);
    }

    void heathstate(int heathClamp)
    {
        if (heathClamp==1)
        {
            this.heath[0].SetActive(true);
            this.heath[1].SetActive(false);
            this.heath[2].SetActive(false);

        }
        else if (heathClamp == 2)
        {
            this.heath[0].SetActive(true);
            this.heath[1].SetActive(true);
            this.heath[2].SetActive(false);
        }
        else if (heathClamp == 3)
        {
            this.heath[0].SetActive(true);
            this.heath[1].SetActive(true);
            this.heath[2].SetActive(true);
        }
        else if (heathClamp <= 0)
        {

         gameObject.GetComponent<Player>().Die();
            
            this.heath[0].SetActive(false);
            this.heath[1].SetActive(false);
            this.heath[2].SetActive(false);
        }
        
       
    }    
   
    
}
