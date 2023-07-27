using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hpbar : MonoBehaviour
{
    public Slider _Hpbar;

    private void Awake()
    {
        _Hpbar = GetComponent<Slider>();    
    }
    public void UpdateHpbar(int currenHp, int Maxhp)
    {
        _Hpbar.value= (float)currenHp / (float)Maxhp;
    }
    private void Update()
    {
        UpdateHpbar(PlayerCrl.instance.dataPlayer.currentHp, PlayerCrl.instance.dataPlayer.Hp);
    }

}
