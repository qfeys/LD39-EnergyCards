using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

static class Tutor
{
    static GameObject go;

    static public void Create()
    {
        go = new GameObject("Tutor", typeof(RectTransform));

        go.transform.SetParent(God.theOne.hand.transform.parent, false);
        RectTransform rt = go.transform as RectTransform;
        rt.anchorMin = new Vector2(0.5f, 1);
        rt.anchorMax = new Vector2(0.5f, 1);
        rt.pivot = new Vector2(0.5f, 1);
        rt.sizeDelta = new Vector2(650, 80);
        rt.anchoredPosition = new Vector2(0, 30);

        Image bg = go.AddComponent<Image>();
        bg.sprite = ImageLibrary.GetImage("tutor");
        bg.type = Image.Type.Sliced;
        bg.color = new Color(0.25f, 0.25f, 1f, 1);

        GameObject txGo = new GameObject("tutor_text", typeof(RectTransform));
        txGo.transform.SetParent(go.transform, false);
        RectTransform txRt = txGo.transform as RectTransform;
        txRt.anchorMin = new Vector2(0.5f, 0);
        txRt.anchorMax = new Vector2(0.5f, 0);
        txRt.pivot = new Vector2(0.5f, 0);
        txRt.sizeDelta = new Vector2(650, 50);
        txRt.anchoredPosition = new Vector2(0, 20);
        Text txtx = txGo.AddComponent<Text>();
        txtx.font = God.theOne.standardFont;
        txtx.color = Color.red;
        txtx.fontSize = 20;
        txtx.alignment = TextAnchor.LowerCenter;
        txtx.text = messages[0];

        go.AddComponent<TutorScript>();
    }

    static List<string> messages = new List<string>() {
        "Play a card, or destroy a card on the field.",
        "Throw away one card.",
        "Throw away another card."
    };

    public static void PlayMessage(int i)
    {
        go.GetComponentInChildren<Text>().text = messages[i];
    }



    class TutorScript : MonoBehaviour
    {

    }
}