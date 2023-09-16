using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class Trapdamager : MonoBehaviour
{
     int Damager=10; 
    // float timeElapsed; // Thời gian đã trôi qua từ lúc va chạm
    // bool isColliding; // Biến đánh dấu xem đối tượng đang va chạm hay không
    // private void Update( )
    // {
    //     // Nếu đối tượng đang va chạm thì tăng biến đếm thời gian
    //     if (isColliding)
    //     {
            
    //         timeElapsed += Time.deltaTime;

    //         // Nếu thời gian đạt đủ 2 giây, trừ máu và reset biến đếm thời gian
    //         if (timeElapsed >= 1f)
    //         {
    //           gameObject.GetComponent<Player>().CurrentHp -=10 ;
    //            timeElapsed = 0f;
    //         }
    //     }
    // }

    

    // public void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.GetComponent<Player>())
    //     { 
    //          gameObject.GetComponent<Player>().CurrentHp -=10 ;
    //         isColliding = true;
           
    //     }
    // }
    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.GetComponent<Player>())
    //     {
    //         isColliding = false;
    //         timeElapsed = 0f; // Reset thời gian khi ngừng va chạm
    //     }
    // }
    
   private void  OnCollisionEnter2D(Collision2D other) 
   {
     if(other.gameObject.GetComponent<Player>())
     {
        other.gameObject.GetComponent<Player>().TakeDame(Damager);
        other.gameObject.GetComponent<Player>().KnockBack(transform);
     }
   }
     
   
}
