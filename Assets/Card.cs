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

    public Card()
    {
        GameObject go = new GameObject("card", typeof(RectTransform));

        go.transform.SetParent(God.theOne.hand.transform, false);
        RectTransform rt = go.transform as RectTransform;
        rt.anchorMin = new Vector2(0, 1);
        rt.anchorMax = new Vector2(0, 1);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(140, 200);

        go.AddComponent<LayoutElement>();

        Image bg = go.AddComponent<Image>();
        bg.sprite = God.theOne.carBackground;
        bg.type = Image.Type.Sliced;
        bg.color = Color.blue;

        go.AddComponent<CardScript>().parent = this;

        GameObject imGo = new GameObject("card_image", typeof(RectTransform));
        imGo.transform.SetParent(go.transform, false);
        RectTransform imRt = imGo.transform as RectTransform;
        imRt.anchorMin = new Vector2(0.5f, 1);
        imRt.anchorMax = new Vector2(0.5f, 1);
        imRt.pivot = new Vector2(0.5f, 1);
        imRt.sizeDelta = new Vector2(120, 100);
        imRt.anchoredPosition = new Vector2(0, -10);
        imGo.AddComponent<Image>().sprite = God.theOne.coal_plant;

        GameObject txGo = new GameObject("card_text", typeof(RectTransform));
        txGo.transform.SetParent(go.transform, false);
        RectTransform txRt = txGo.transform as RectTransform;
        txRt.anchorMin = new Vector2(0.5f, 1);
        txRt.anchorMax = new Vector2(0.5f, 1);
        txRt.pivot = new Vector2(0.5f, 1);
        txRt.sizeDelta = new Vector2(120, 70);
        txRt.anchoredPosition = new Vector2(0, -120);
        Text txtx = txGo.AddComponent<Text>();
        txtx.text = "This is a coal powered power plant";
        txtx.font = God.theOne.standardFont;


        Debug.Log("New Card!");
    }

    class CardScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public Card parent;

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
class TestCard : Card
{

}
