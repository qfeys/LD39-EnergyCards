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
    public static bool canPlay = false;
    internal static bool canBin = false;
    internal static bool CardDestruction { get { return _cardDestruction; } set { _cardDestruction = value; God.theOne.hand.GetComponent<HandAnimator>().fullyDown = value; } }
    internal static bool _cardDestruction = false;
    const float TOOLTIP_DELAY = 0.5f;

    protected Card() { }

    public Card(string name, Board.Regions validRegions)
    {
        this.name = name;
        this.validRegions = validRegions;
    }

    public Card(Card copy)
    {
        name = copy.name;
        validRegions = copy.validRegions;
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
        bg.sprite = ImageLibrary.GetImage("card_background_long");
        switch (name.Substring(0, 5))
        {
        case "plant": bg.color = new Color(1, 0.94f, 0.25f, 1); break;
        case "trans": bg.color = new Color(1, 0.25f, 0.25f, 1); break;
        case "store": bg.color = new Color(0.25f, 1, 0.25f, 1); break;
        case "polit": bg.color = new Color(0.25f, 0.25f, 1, 1); break;
        }

        card.AddComponent<CardScript>().parent = this;

        GameObject imGo = new GameObject("card_image", typeof(RectTransform));
        imGo.transform.SetParent(card.transform, false);
        RectTransform imRt = imGo.transform as RectTransform;
        imRt.anchorMin = new Vector2(0.5f, 1);
        imRt.anchorMax = new Vector2(0.5f, 1);
        imRt.pivot = new Vector2(0.5f, 1);
        imRt.sizeDelta = new Vector2(120, 100);
        imRt.anchoredPosition = new Vector2(0, -10);
        imGo.AddComponent<Image>().sprite = ImageLibrary.GetImage(name + "_big");

        GameObject txGo = new GameObject("card_text", typeof(RectTransform));
        txGo.transform.SetParent(card.transform, false);
        RectTransform txRt = txGo.transform as RectTransform;
        txRt.anchorMin = new Vector2(0.5f, 1);
        txRt.anchorMax = new Vector2(0.5f, 1);
        txRt.pivot = new Vector2(0.5f, 1);
        txRt.sizeDelta = new Vector2(120, 70);
        txRt.anchoredPosition = new Vector2(0, -120);
        Text txtx = txGo.AddComponent<Text>();
        //txtx.text = Localisation.GetText(name + "_text");
        txtx.text = Localisation.GetText(name);
        txtx.font = God.theOne.standardFont;
    }

    void ConvertToMini(Vector2 pos)
    {
        UnityEngine.Object.Destroy(card);
        CreateMini(pos);
    }

    public void DestroyMiniCard()
    {
        Board.RemoveCard(this);
        UnityEngine.Object.Destroy(card);
    }

    internal void CreateMini(Vector2 pos)
    {
        card = new GameObject("mini_card", typeof(RectTransform));

        card.transform.SetParent(God.theOne.board_go.transform, false);
        RectTransform rt = card.transform as RectTransform;
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 0);
        rt.pivot = new Vector2(0, 0);
        rt.sizeDelta = new Vector2(120, 120);
        rt.anchoredPosition = pos;

        Image bg = card.AddComponent<Image>();
        bg.sprite = ImageLibrary.GetImage("background_minicards");
        if(name != "city")
            switch (name.Substring(0, 5))
            {
            case "plant": bg.color = new Color(1, 0.94f, 0.25f, 1); break;
            case "trans": bg.color = new Color(1, 0.25f, 0.25f, 1); break;
            case "store": bg.color = new Color(0.25f, 1, 0.25f, 1); break;
            case "polit": bg.color = new Color(0.25f, 0.25f, 1, 1); break;
            }

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
        imGo.AddComponent<Image>().sprite = ImageLibrary.GetImage(name + "_mini");
    }

    class CardScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler , IPointerEnterHandler, IPointerExitHandler
    {
        public Card parent;
        float clock = TOOLTIP_DELAY;
        bool enter = false;

        private void Update()
        {
            if (enter)
                clock -= Time.deltaTime;
            if (clock < 0)
                Tooltip.Display(parent.name);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.parent.GetComponent<HandAnimator>().down = true;
            transform.SetParent(transform.parent.parent);
            if(canPlay)
                Board.DisplayValidSpots(parent);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position += (Vector3)eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            transform.SetParent(transform.parent.GetChild(0));
            Board.ClearGhosts();
            transform.parent.GetComponent<HandAnimator>().down = false;

            if (results.Any(r => r.gameObject.name == "Hand"))
            {
            }
            else if (results.Any(r => r.gameObject.name == "Board") && canPlay)
            {
                if (results.Any(r => r.gameObject.name == "ghost_card"))
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
                    God.theOne.PlayCardSound();
                    canPlay = false;
                }
            }else if(results.Any(r=>r.gameObject.name == "bin") && canBin)
            {
                Bin.Dispose(parent);
                canBin = false;
                Destroy(gameObject);
                God.theOne.PlayBinSound();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            enter = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            enter = false;
            if (clock < 0)
                Tooltip.EndDisplay();
            clock = TOOLTIP_DELAY;

        }
    }

    protected class MiniCardScript : MonoBehaviour, IPointerClickHandler
    {
        public Card parent;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (CardDestruction)
            {
                parent.DestroyMiniCard();
                Destroy(this.gameObject);
                Debug.Log("Try destruction");
            }
        }
    }
}