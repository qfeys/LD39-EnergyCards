using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


abstract class Card
{
    public GameObject handCard;
    public GameObject boardCard;

    public static GameObject AddToHand()
    {
        GameObject go = new GameObject("card", typeof(RectTransform));
        go.transform.SetParent(God.theOne.hand.transform, false);
        RectTransform rt = go.transform as RectTransform;
        rt.anchorMin = new Vector2(0, 1);
        rt.anchorMax = new Vector2(0, 1);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(140, 200);
        go.AddComponent<LayoutElement>();
        Debug.Log("New Card!");
        return go;
    }

    class CardScript : MonoBehaviour
    {

    }

}
