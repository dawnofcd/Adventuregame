using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FruitType
{
    apple,
    banana,
    cherries,
    kiwi,
    melon,
    orange,
    pineapple,
    strawberry
}
public class Fruit_item : MonoBehaviour
{
    private Animator anim;
    public FruitType myFruitType;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite[] fruitImage;


    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.GetComponent<dataPlayer>() != null)
        {
            collison.GetComponent<dataPlayer>();
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();

        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }

        anim.SetLayerWeight((int)myFruitType, 1);

    }
    private void OnValidate()
    {
        sr.sprite = fruitImage[(int)myFruitType];
    }


}

