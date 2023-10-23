using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hpbar : MonoBehaviour
{
    public static Hpbar instance;
    public Slider _Hpbar;
    private void Awake() {
         instance=this;
         _Hpbar = GetComponent<Slider>();   
    }

    public void UpdateHpBar(int currenHp, int Maxhp)
    {
        _Hpbar.value= (float) currenHp / (float)Maxhp;
    }

    
}
