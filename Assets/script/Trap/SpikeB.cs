using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  SpikeB : MonoBehaviour
{   
    [SerializeField] int damMage;
    [SerializeField] float idleTime=2;
    [SerializeField] float countTime;
    
    [SerializeField] bool isWorking;
    BoxCollider2D lightning;

    [SerializeField] Animator anim;


    void Start()
    {
        lightning = this.GetComponent<BoxCollider2D>();
    }
    


    public void On()
    {
         
            lightning.enabled = true;
            isWorking = true;
            anim.SetBool("isWorking",true);
         
    }

    public void Off()
    {
       
         lightning.enabled = false;
         isWorking=false;
         anim.SetBool("isWorking",false);
    }

    void Update()
    {
      
      if(countTime>0f)
      {
        countTime -= Time.deltaTime;
       Off();
      }
      else
      {
        countTime=idleTime;
       On();
      }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Player>()!=null && isWorking)
        {
            other.GetComponent<Player>().KnockBack(transform);
            PlayerReceiver.instance.TakeDame(damMage);
        }
    }

}
