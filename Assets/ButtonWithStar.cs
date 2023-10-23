using System.Collections.Generic;
using UnityEngine;



public class ButtonWithStar : MonoBehaviour
{
   public GameObject []star;
   public int currentLv;
   
   
 

  public void StarRating(int starInButton )
   {
    for(int i=0; i<4;i++)
     if(currentLv==i)
     {
        int Star=  starInButton;
        Debug.Log(Star);
       
        if(Star==0)
        {  
            star[0].SetActive(false);
            star[1].SetActive(false);
            star[2].SetActive(false);
        }
        if (Star==1)
        {
            star[0].SetActive(true);
            star[1].SetActive(false);
            star[2].SetActive(false);
        }
        
        if(Star==2)
        {
            
            star[0].SetActive(true);
            star[1].SetActive(true);
            star[2].SetActive(false);
        }

        if(Star==3)
        {
            star[0].SetActive(true);
            star[1].SetActive(true);
            star[2].SetActive(true);
        }
     }
   }
   
}

