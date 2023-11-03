using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Trap_SpikeHead : MonoBehaviour   
{   
    private Animator anim;
    [SerializeField] private float ValueY1;
    [SerializeField] private float ValueY2;
    [SerializeField] private float Time1;
    [SerializeField] private float Time2;
    [SerializeField] private float TimeDelay1;
    [SerializeField] private float TimeDelay2;

    void Start()
    {
        anim = GetComponent<Animator>();
        OnScale();
        // animator();
    }

    private void OnScale()
    {
        transform.DOMoveY(ValueY1, Time1)
            .SetEase(Ease.InOutSine)
            .SetDelay(TimeDelay1)
            .OnComplete(()=>
            {
                transform.DOMoveY(ValueY2, Time2)
                    .SetDelay(TimeDelay2)
                    .OnComplete(OnScale);


            });


    }




    IEnumerator animator()
    {
        anim.SetBool("blink",true);
        yield return new WaitForSeconds(2);


    }

}
