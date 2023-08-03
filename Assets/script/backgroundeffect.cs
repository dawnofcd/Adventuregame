using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backgroundeffect : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] float _X , _Y;
    

    void Update()
    {
        effect();
    }
    void effect()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(_X, _Y) * Time.deltaTime, image.uvRect.size);
    }      
           
}
 