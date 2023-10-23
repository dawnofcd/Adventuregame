using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CellingTrap2 : MonoBehaviour
{
    [SerializeField] private float ValueY;
    [SerializeField] private float ValueYExit;
    [SerializeField] private float Time;
    [SerializeField] private float TimeExit;
    [SerializeField] private float TimeDelay;
    [SerializeField] private float TimeDelayExit;
    protected Animator anim;
    void Start()
    {

        anim = GetComponent<Animator>();
        OnScale();

    }

    void Update()
    {

    }

    private void OnScale()
    {
        transform.DOMoveY(ValueY, Time)
            .SetEase(Ease.InOutSine)
            .SetDelay(TimeDelay)
            .OnComplete(()=>
            {
                transform.DOMoveY(ValueYExit, TimeExit)
                    .SetDelay(TimeDelayExit)
                    .OnComplete(OnScale);
                // anim.SetBool("isWorking", true);


            });
            
            

    }



    // private void Flip()
    // {
    //     transform.localScale = new Vector3 (1, transform.localScale.y * -1);
    // }
}
