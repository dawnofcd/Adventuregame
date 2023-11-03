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
  private float _timeDelayBurn;
  //chạm vào 2s sau thì bật  



  void LateUpdate()
  {
    TouchOn();

  }
  void TouchOn()
  {
    if (timeBurn <= 5 && IsOn)
    {
      timeBurn += Time.deltaTime;
      if (_timeDelayBurn <= 2f) // chạm vào 2 s sau thì bật
      {
        _timeDelayBurn += Time.deltaTime;
        return;
      }
      else
      {
        anim.SetBool("On", true);
        IsDame = true;// bật lửa thì bật biến gây dame
        _timeDelayBurn = 0f;
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

  void OnTriggerEnter2D(Collider2D other)
  {

    anim.SetBool("isHit", true);
    IsOn = true;
    if (IsDame && other.gameObject.GetComponent<Player>())
    {


      PlayerReceiver.instance.TakeDame(dameFire);
      other.gameObject.GetComponent<Player>().KnockBack(transform);
    }
  }

  void OnTriggerStay2D(Collider2D other)
  {
    anim.SetBool("isHit", false);
    if (IsDame && other.gameObject.GetComponent<Player>())
    {

      PlayerReceiver.instance.TakeDame(dameFire);
      other.gameObject.GetComponent<Player>().KnockBack(transform);
    }
  }
  void OnTriggerExit(Collider other)
  {
    anim.SetBool("isHit", false);
    // IsDame = false; // nếu không chạm vào lửa thì k nhận dame
  }

}
