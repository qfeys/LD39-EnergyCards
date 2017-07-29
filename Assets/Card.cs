using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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

        Image im = go.AddComponent<Image>();
        im.sprite = God.theOne.carBackground;
        im.type = Image.Type.Sliced;
        im.color = Color.blue;

        go.AddComponent<CardScript>();
        Debug.Log("New Card!");
        return go;
    }

    class CardScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.parent.GetComponent<HandAnimator>().down = true;
            transform.SetParent(transform.parent.parent);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position += (Vector3)eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.parent.GetChild(0).GetComponent<HandAnimator>().down = false;
            transform.SetParent(transform.parent.GetChild(0));
        }
    }

}
