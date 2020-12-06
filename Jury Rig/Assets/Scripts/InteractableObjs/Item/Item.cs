using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    //public string itemName;

    public Sprite sprite;


    internal Color getColor => Color.white;

    internal void HideItem() => gameObject.SetActive(false);

    //public void DestroyItem()
    //{
    //    Destroy(gameObject);
    //}


}
