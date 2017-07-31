using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

static class Tooltip
{
    static GameObject go;

    static public void Create()
    {
        go = new GameObject("Tooltip", typeof(RectTransform));

        go.transform.SetParent(God.theOne.hand.transform.parent, false);
        RectTransform rt = go.transform as RectTransform;
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 0);
        rt.pivot = new Vector2(0, 1);
        rt.sizeDelta = new Vector2(100, 50);
        rt.anchoredPosition = new Vector2(0, 0);


        Image bg = go.AddComponent<Image>();
        bg.sprite = ImageLibrary.GetImage("tooltip");
        bg.type = Image.Type.Sliced;

        GameObject txGo = new GameObject("tutor_text", typeof(RectTransform));
        txGo.transform.SetParent(go.transform, false);
        RectTransform txRt = txGo.transform as RectTransform;
        txRt.anchorMin = new Vector2(0.5f, 0);
        txRt.anchorMax = new Vector2(0.5f, 0);
        txRt.pivot = new Vector2(0.5f, 0);
        txRt.sizeDelta = new Vector2(400, 50);
        txRt.anchoredPosition = new Vector2(0, 20);
        Text txtx = txGo.AddComponent<Text>();
        txtx.font = God.theOne.standardFont;
        txtx.color = Color.red;
        txtx.alignment = TextAnchor.LowerCenter;

        go.AddComponent<TooltipScript>();

        go.SetActive(false);
    }

    internal static void Display(string text)
    {
        return;
        go.SetActive(true);
        go.GetComponentInChildren<Text>().text = text;
    }

    internal static void EndDisplay()
    {
        return;
        go.SetActive(false);
    }

    class TooltipScript : MonoBehaviour
    {
        private void Update()
        {
            ((RectTransform)transform).anchoredPosition = (Vector2)Input.mousePosition - Vector2.one;
        }
    }
}

