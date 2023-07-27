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
    void CaculatorHp(int currentheath)
    {
        if (PlayerCrl.instance.dataPlayer.currentHp<=0)
        {
            PlayerCrl.instance.dataPlayer.currentheath -= 1;
            PlayerCrl.instance.dataPlayer.currentHp = 100;
        }

        heathstate(currentheath);
            
    }

    void heathstate(int currentheath)
    {
        if (currentheath==1)
        {
            this.heath[0].SetActive(true);
            this.heath[1].SetActive(false);
            this.heath[2].SetActive(false);

        }
        else if (currentheath == 2)
        {
            this.heath[0].SetActive(true);
            this.heath[1].SetActive(true);
            this.heath[2].SetActive(false);
        }
        else if (currentheath == 3)
        {
            this.heath[0].SetActive(true);
            this.heath[1].SetActive(true);
            this.heath[2].SetActive(true);
        }
        else if (currentheath == 0)
        {
            Die();
        }
    }    
    void Die()
    {

    }
    private void Update()
    {
        CaculatorHp(PlayerCrl.instance.dataPlayer.currentheath);
    }

}
