using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdamager : MonoBehaviour
{
    float timeElapsed; // Thời gian đã trôi qua từ lúc va chạm
    bool isColliding; // Biến đánh dấu xem đối tượng đang va chạm hay không

    private void Update()
    {
        // Nếu đối tượng đang va chạm thì tăng biến đếm thời gian
        if (isColliding)
        {
            
            timeElapsed += Time.deltaTime;

            // Nếu thời gian đạt đủ 2 giây, trừ máu và reset biến đếm thời gian
            if (timeElapsed >= 1f)
            {
               PlayerCrl.instance.dataPlayer.currentHp -=10;
               timeElapsed = 0f;
            }
        }
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = true;
            PlayerCrl.instance.dataPlayer.currentHp -= 10;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = false;
            timeElapsed = 0f; // Reset thời gian khi ngừng va chạm
        }
    }
    
   
}
