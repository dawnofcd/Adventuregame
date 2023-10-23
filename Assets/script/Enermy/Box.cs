using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
     protected Animator anim;
    protected int facingDirection = -1;
    [HideInInspector] public bool invicinble;
    [SerializeField] private GameObject break1Prefab;
    [SerializeField] private GameObject break2Prefab;
    [SerializeField] private GameObject break3Prefab;
    [SerializeField] private GameObject break4Prefab;
    [SerializeField] private Transform break1Origin;
    [SerializeField] private Transform break2Origin;
    [SerializeField] private Transform break3Origin;
    [SerializeField] private Transform break4Origin;   

    [SerializeField] public float health;

    private void Start()
    {
        anim = GetComponent<Animator>();



    }

    public virtual void Damage()
    {
        health--;
        if(!invicinble)
        {
            anim.SetBool("gotHit",  true);
            if(health<=0)
            {
                DestroyMe();
            }
                

        }
    }
    private void StopGotHitAnim()
    {
        anim.SetBool("gotHit",  false);
    }

    
    public virtual void DestroyMe()
    {
        PlayerManager.instance.ScreenShake(-facingDirection);
        Destroy(gameObject);
        CreateBreak();

    }
    private void CreateBreak()
    {
        
        GameObject newBreak1 = Instantiate(break1Prefab,break1Origin.transform.position, break1Origin.transform.rotation );
        newBreak1.GetComponent<Box>();
        Destroy(newBreak1, 2.5f);        

        GameObject newBreak2 = Instantiate(break2Prefab,break2Origin.transform.position, break2Origin.transform.rotation );
        newBreak2.GetComponent<Box>();
        Destroy(newBreak2, 2.5f);
        GameObject newBreak3 = Instantiate(break3Prefab,break3Origin.transform.position, break3Origin.transform.rotation );
        newBreak3.GetComponent<Box>();
        Destroy(newBreak3, 2.5f);        

        GameObject newBreak4 = Instantiate(break4Prefab,break4Origin.transform.position, break4Origin.transform.rotation );
        newBreak4.GetComponent<Box>();
        Destroy(newBreak4, 2.5f);

    }
}
