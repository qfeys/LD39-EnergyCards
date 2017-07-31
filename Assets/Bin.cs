using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

static class Bin
{
    static GameObject go;
    static List<Card> bin;

    const float POSITION_UP = 0.2f;
    const float POSITION_DOWN = 0.6f;
    static public bool down = false;

    static public void Create()
    {
        bin = new List<Card>();
        go = new GameObject("bin", typeof(RectTransform));

        go.transform.SetParent(God.theOne.hand.transform.parent, false);
        RectTransform rt = go.transform as RectTransform;
        rt.anchorMin = new Vector2(1, 1);
        rt.anchorMax = new Vector2(1, 1);
        rt.pivot = new Vector2(1, POSITION_UP);
        rt.sizeDelta = new Vector2(160, 220);
        rt.anchoredPosition = new Vector2(-20,0);

        Image bg = go.AddComponent<Image>();
        bg.sprite = ImageLibrary.GetImage("bin");

        go.AddComponent<BinScript>();
    }

    static public void Activate()
    {
        down = true;
    }

    static public void Dispose(Card card)
    {
        bin.Add(card);
        down = false;
    }

    class BinScript : MonoBehaviour
    {
        const float SPEED = 1.5f;
        
        void Update()
        {
            RectTransform rt = ((RectTransform)transform);
            if (!down & rt.pivot.y > POSITION_UP)
            {
                rt.pivot = new Vector2(rt.pivot.x, rt.pivot.y - SPEED * Time.deltaTime);

            }
            else if (down & rt.pivot.y < POSITION_DOWN)
            {
                rt.pivot = new Vector2(rt.pivot.x, rt.pivot.y + SPEED * Time.deltaTime);
            }
        }
    }
    
}
