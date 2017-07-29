using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


class Card
{
    public string name;
    public Board.Regions validRegions;
    public GameObject card;

    protected Card() { }

    public Card(string name, Board.Regions validRegions)
    {
        this.name = name;
        this.validRegions = validRegions;
    }

    public void PlaceInHand()
    {
        card = new GameObject("card_" + name, typeof(RectTransform));

        card.transform.SetParent(God.theOne.hand.transform, false);
        RectTransform rt = card.transform as RectTransform;
        rt.anchorMin = new Vector2(0, 1);
        rt.anchorMax = new Vector2(0, 1);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(140, 200);

        card.AddComponent<LayoutElement>();

        Image bg = card.AddComponent<Image>();
        bg.sprite = God.theOne.cardBackground;
        bg.type = Image.Type.Sliced;
        bg.color = Color.blue;

        card.AddComponent<CardScript>().parent = this;

        GameObject imGo = new GameObject("card_image", typeof(RectTransform));
        imGo.transform.SetParent(card.transform, false);
        RectTransform imRt = imGo.transform as RectTransform;
        imRt.anchorMin = new Vector2(0.5f, 1);
        imRt.anchorMax = new Vector2(0.5f, 1);
        imRt.pivot = new Vector2(0.5f, 1);
        imRt.sizeDelta = new Vector2(120, 100);
        imRt.anchoredPosition = new Vector2(0, -10);
        imGo.AddComponent<Image>().sprite = God.theOne.coal_plant;

        GameObject txGo = new GameObject("card_text", typeof(RectTransform));
        txGo.transform.SetParent(card.transform, false);
        RectTransform txRt = txGo.transform as RectTransform;
        txRt.anchorMin = new Vector2(0.5f, 1);
        txRt.anchorMax = new Vector2(0.5f, 1);
        txRt.pivot = new Vector2(0.5f, 1);
        txRt.sizeDelta = new Vector2(120, 70);
        txRt.anchoredPosition = new Vector2(0, -120);
        Text txtx = txGo.AddComponent<Text>();
        txtx.text = "This is a coal powered power plant";
        txtx.font = God.theOne.standardFont;
    }

    void ConvertToMini(Vector2 pos)
    {
        UnityEngine.Object.Destroy(card);
        card = new GameObject("mini_card", typeof(RectTransform));

        card.transform.SetParent(God.theOne.board_go.transform, false);
        RectTransform rt = card.transform as RectTransform;
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 0);
        rt.pivot = new Vector2(0, 0);
        rt.sizeDelta = new Vector2(120, 120);
        rt.anchoredPosition = pos;

        Image bg = card.AddComponent<Image>();
        bg.sprite = God.theOne.cardBackground;
        bg.type = Image.Type.Sliced;

        God.theOne.activeCards.Add(this);
        card.AddComponent<MiniCardScript>().parent = this;

        GameObject imGo = new GameObject("card_image", typeof(RectTransform));
        imGo.transform.SetParent(card.transform, false);
        RectTransform imRt = imGo.transform as RectTransform;
        imRt.anchorMin = new Vector2(0.5f, 0.5f);
        imRt.anchorMax = new Vector2(0.5f, 0.5f);
        imRt.pivot = new Vector2(0.5f, 0.5f);
        imRt.sizeDelta = new Vector2(100, 100);
        imRt.anchoredPosition = new Vector2(0, 0);
        imGo.AddComponent<Image>().sprite = God.theOne.coal_plant;
    }

    class CardScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public Card parent;

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.parent.GetComponent<HandAnimator>().down = true;
            transform.SetParent(transform.parent.parent);
            Board.DisplayValidSpots(parent);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position += (Vector3)eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.parent.GetChild(0).GetComponent<HandAnimator>().down = false;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            if(results.Any(r=>r.gameObject.name == "Hand"))
            {
            }
            else if(results.Any(r=>r.gameObject.name == "Board"))
            {
                if (results.Any(r=>r.gameObject.name == "ghost_card"))
                {
                    //Vector2 pos;
                    //RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    //    God.theOne.board_go.transform.parent as RectTransform, Input.mousePosition,
                    //    God.theOne.board_go.transform.parent.GetComponent<Canvas>().worldCamera,
                    //    out pos);
                    Vector2 pos = ((RectTransform)results.Find(r => r.gameObject.name == "ghost_card").gameObject.transform).anchoredPosition;
                    Board.AddCard(parent, pos);
                    God.theOne.activeCards.Add(parent);
                    parent.ConvertToMini(pos);
                }
            }
            transform.SetParent(transform.parent.GetChild(0));
            Board.ClearGhosts();
        }
    }

    protected class MiniCardScript : MonoBehaviour
    {
        public Card parent;
    }
}