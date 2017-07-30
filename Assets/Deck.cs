using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

static class Deck
{
    static List<Card> deck;
    static GameObject go;

    public static void Create()
    {
        go = new GameObject("deck", typeof(RectTransform));

        go.transform.SetParent(God.theOne.hand.transform, false);
        RectTransform rt = go.transform as RectTransform;
        rt.anchorMin = new Vector2(1, 1);
        rt.anchorMax = new Vector2(1, 1);
        rt.pivot = new Vector2(1, 1);
        rt.sizeDelta = new Vector2(140, 200);

        go.AddComponent<LayoutElement>().ignoreLayout = true;
        rt.anchoredPosition = Vector2.zero;

        Image bg = go.AddComponent<Image>();
        bg.sprite = ImageLibrary.GetImage("back_long");

        go.AddComponent<DeckScript>();

        AddCards();
        Shuffel();
    }

    private static void AddCards()
    {
        var lib = new Dictionary<Card, int> {
            { new Card("plant_coal"     , Board.Regions.city | Board.Regions.port), 5 },
            { new Card("plant_oil"      , Board.Regions.city | Board.Regions.port | Board.Regions.desert), 5 },
            { new Card("plant_gas"      , Board.Regions.city | Board.Regions.port), 5 },
            { new Card("plant_nuke"     , Board.Regions.offshore | Board.Regions.port), 5 },
            { new Card("plant_wind"     , Board.Regions.city | Board.Regions.offshore), 5 },
            { new Card("plant_solar"    , Board.Regions.city | Board.Regions.desert), 5 },
            { new Card("trans_HV"       , Board.Regions.Road), 5 },
            { new Card("trans_HVDC"     , Board.Regions.Road), 5 },
            { new Card("trans_SPCD"     , Board.Regions.Road), 5 },
            { new Card("trans_pipe_oil" , Board.Regions.Road), 5 },
            { new Card("trans_pipe_gas" , Board.Regions.Road), 5 },
            { new Card("trans_rail"     , Board.Regions.city), 5 },
            { new Card("trans_term_coal", Board.Regions.port), 5 },
            { new Card("trans_term_oil" , Board.Regions.port), 5 },
            { new Card("trans_term_lng" , Board.Regions.port), 5 },
            { new Card("store_bat"      , Board.Regions.city | Board.Regions.desert), 5 },
            { new Card("store_oil"      , Board.Regions.city | Board.Regions.desert), 5 },
            { new Card("store_gas"      , Board.Regions.city | Board.Regions.desert), 5 },
            { new Card("store_fuel"     , Board.Regions.city | Board.Regions.port | Board.Regions.desert), 5 },
            { new Card("pol_resist"     , Board.Regions.city), 5 },
            { new Card("pol_fortify_fofu", Board.Regions.city), 5 },
            { new Card("pol_fortify_nuke", Board.Regions.city), 5 },
            { new Card("pol_fortify_wind", Board.Regions.city), 5 },
            { new Card("pol_fortify_solar", Board.Regions.city), 5 },
            { new Card("pol_extra_card" , Board.Regions.city), 5 },
            { new Card("pol_reshufel"   , Board.Regions.city), 5 }
        };
        deck = new List<Card>();
        foreach(var kvp in lib)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                deck.Add(new Card(kvp.Key));
            }
        }
    }

    private static void Shuffel()
    {
        var rng = new System.Random();
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }

    public static void TakeNext()
    {
        deck[0].PlaceInHand();
        deck.RemoveAt(0);
    }

    class DeckScript : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            //TakeNext();
        }
    }
}
