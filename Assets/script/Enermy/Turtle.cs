using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Enemy
{
     
    [SerializeField] private float aggroTime;
                   
    Collider2D SpikeIn;
    protected override void Start()
    {
        base.Start();
        SpikeIn = this.GetComponent<BoxCollider2D>();
    }

    public void On()
    {
        SpikeIn.enabled = true;

    }

    public void Off()
    {
        SpikeIn.enabled = false;
    }

    protected override void  OnTriggerEnter2D(Collider2D other)  
  {
    base.OnTriggerEnter2D(other);
    if (other.gameObject.GetComponent<Player>()!=null)
    {   
       PlayerReceiver.instance.TakeDame(Damemage);
    }
  }

}
