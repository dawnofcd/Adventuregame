using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
  private Animator anim;
  [SerializeField] float timeBurn;
  private bool IsOn;
  private bool IsDame;
  private int dameFire = 50;
  private float _timedelayburn;
  //chạm vào 2s sau thì bật  
  void touchOn()
  {
    
    if (timeBurn <= 5 && IsOn)
    {
      timeBurn += Time.deltaTime;
      if (_timedelayburn<=2f)
      {
      _timedelayburn+=Time.deltaTime;
      return;
      }
      else
      { 
        anim.SetBool("On", true);
        IsDame = true;// bật lửa thì bật biến gây dame
        _timedelayburn=0f;
      }
    }
    else if (timeBurn > 5)
    {
      timeBurn = 0;
      IsOn = false;
    }
    if (!IsOn)
    {
      anim.SetBool("On", false);
      IsDame = false;
    }

  }

  void Awake()
  {
    anim = GetComponent<Animator>();
  }

  void LateUpdate()
  {
    touchOn();

  }
  void OnTriggerEnter2D(Collider2D other)
  {

    anim.SetBool("isHit", true);
    IsOn = true;
    if (IsDame && other.gameObject.GetComponent<Player>())
    {

      other.gameObject.GetComponent<Player>().TakeDame(dameFire);
      other.gameObject.GetComponent<Player>().KnockBack(transform);
    }
  }

  void OnTriggerStay2D(Collider2D other)
  {
    anim.SetBool("isHit", false);
    if (IsDame && other.gameObject.GetComponent<Player>())
    {

      other.gameObject.GetComponent<Player>().TakeDame(dameFire);
      other.gameObject.GetComponent<Player>().KnockBack(transform);
    }
  }
  void OnTriggerExit(Collider other)
  {
    anim.SetBool("isHit", false);
   // IsDame = false; // nếu không chạm vào lửa thì k nhận dame
  }

}
