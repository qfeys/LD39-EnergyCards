using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

    public class InfoTable
    {
        List<Tuple<string, Func<object>>> info;

        public GameObject exampleText;

        GameObject go;
        public GameObject gameObject { get { return go; } }
        int fontSize;

        public InfoTable(Transform parent, List<Tuple<string, Func<object>>> info, int width = 200, int fontSize = 12) :
            this(parent, width, fontSize)
        {
            this.info = info;
            Redraw();
        }

        public InfoTable(Transform parent, Func<List<Tuple<string, Func<object>>>> script, int width = 200, int fontSize = 12) :
            this(parent, width, fontSize)
        {
            info = null;
            ActiveInfoTable ait = go.AddComponent<ActiveInfoTable>();
            ait.parent = this;
            ait.script = script;
            info = script();
            Redraw();
        }

        InfoTable(Transform parent, int width = 200, int fontSize = 12)
        {

            this.fontSize = fontSize;
            go = new GameObject("Info Table", typeof(RectTransform));
            go.transform.SetParent(parent, false);
            ((RectTransform)go.transform).sizeDelta = new Vector2(width, 100);
            VerticalLayoutGroup VLayGr = go.AddComponent<VerticalLayoutGroup>();
            VLayGr.childForceExpandHeight = false;
            VLayGr.childForceExpandWidth = false;
        }

        public void Redraw()
        {
            for (int i = go.transform.childCount - 1; i >= 0; i--)      // Kill all previous lines
            {
                UnityEngine.Object.Destroy(go.transform.GetChild(i).gameObject);
                go.transform.GetChild(i).SetParent(null);
            }
            if (info.Count == 0)        // no data - place a dummy line
            {
                GameObject line = CreateLine();

                TextBox name = new TextBox(line.transform, "#####", "#####", fontSize, TextAnchor.MiddleLeft);
                name.SetFlexibleWidth(1);

                TextBox data = new TextBox(line.transform, "#####", "#####", fontSize, TextAnchor.MiddleRight);
            }
            else
            {
                for (int i = 0; i < info.Count; i++)       ///TODO: OPTIMISATION - DO ONLY REDRAW IF THE LINES ARE NOT THE SAME
                {
                    GameObject line = CreateLine();
                    TextBox name = new TextBox(line.transform, info[i].Item1, "#####", fontSize, TextAnchor.MiddleLeft);
                    name.SetFlexibleWidth(1);

                    TextBox data = new TextBox(line.transform, info[i].Item2, "#####", fontSize, TextAnchor.MiddleRight);
                }
            }
        }

        private GameObject CreateLine()
        {
            GameObject line = new GameObject("Line", typeof(RectTransform));
            line.transform.SetParent(go.transform, false);
            LayoutElement LayEl = line.AddComponent<LayoutElement>();
            LayEl.minHeight = fontSize;
            LayEl.preferredHeight = fontSize * 2;
            LayEl.flexibleWidth = 1;
            LayEl.flexibleHeight = 0;
            HorizontalLayoutGroup HLayGr = line.AddComponent<HorizontalLayoutGroup>();
            HLayGr.childForceExpandWidth = true;
            HLayGr.childForceExpandHeight = true;
            HLayGr.padding = new RectOffset((int)LayEl.minHeight, (int)LayEl.minHeight, 0, 0);
            return line;
        }

        public void SetInfo(List<Tuple<string, Func<object>>> newInfo)
        {
            info = newInfo;
        }

        public void SetInfo(Tuple<string, Func<object>> newInfo)
        {
            info = new List<Tuple<string, Func<object>>> {
                newInfo
            };
        }

        public void AddInfo(Tuple<string, Func<object>> newInfo)
        {
            info.Add(newInfo);
        }

        internal void ResetInfo()
        {
            info = new List<Tuple<string, Func<object>>>();
        }

        public class ActiveInfoTable : MonoBehaviour
        {
            public InfoTable parent;

            public Func<List<Tuple<string, Func<object>>>> script;
            
            private void Update()
            {
                List<Tuple<string, Func<object>>> newInfo = script();
                if (parent.info.SequenceEqual(newInfo) == false)
                {
                    parent.info = newInfo;
                    parent.Redraw();
                }
            }
        }
    }
    public class Tuple<T1, T2>
    {
        public T1 Item1;
        public T2 Item2;
        public Tuple(T1 item1, T2 item2)
        {
            Item1 = item1; Item2 = item2;
        }
    }
public class TextBox
{
    public string TextID { get { return _textID; } set { _textID = value; } }
    public string _textID;
    public string MousoverID { get { return _mousoverID; } set { _mousoverID = value; } }
    public string _mousoverID;
    public Func<object> Data_ { get { return _data; } set { _data = value; } }
    public Func<object> _data;

    bool isData;

    GameObject go;
    Text text;
    public GameObject gameObject { get { return go; } }

    public TextBox(Transform parent, string textID, string mousoverID, int size = 12, TextAnchor allignment = TextAnchor.MiddleLeft, Color? color = null)
    {
        TextID = textID; MousoverID = mousoverID;
        isData = false;
        go = new GameObject(textID, typeof(RectTransform));
        StanConstr(parent, size, allignment);
        text.text = Localisation.GetText(textID);
        text.color = color ?? Color.black;
    }

    public TextBox(Transform parent, Func<object> data, string mousoverID, int size = 12, TextAnchor allignment = TextAnchor.MiddleLeft, Color? color = null)
    {
        TextID = null; MousoverID = mousoverID;
        isData = true;
        Data_ = data;
        go = new GameObject("dataText", typeof(RectTransform));
        StanConstr(parent, size, allignment);
        text.text = data().ToString();
        text.color = color ?? Color.red;
    }

    private void StanConstr(Transform parent, int size, TextAnchor allignment)
    {
        go.transform.SetParent(parent, false);
        RectTransform tr = (RectTransform)go.transform;
        tr.anchorMin = new Vector2(0, 0.5f);
        tr.anchorMax = new Vector2(0, 0.5f);
        tr.pivot = new Vector2(0, 0.5f);
        tr.anchoredPosition = new Vector2(0, 0);
        text = go.AddComponent<Text>();
        text.font = God.theOne.standardFont;
        text.fontSize = size;
        text.alignment = allignment;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        TextBoxScript tbs = go.AddComponent<TextBoxScript>();
        tbs.parent = this;
    }

    public void SetFlexibleWidth(float width)
    {
        if (go.GetComponent<LayoutElement>() == null)
            go.AddComponent<LayoutElement>();
        go.GetComponent<LayoutElement>().flexibleWidth = width;
    }

    private void Update()
    {
        if (isData)
        {
            text.text = Localisation.GetText(Data_().ToString());
        }
    }

    class TextBoxScript : MonoBehaviour
    {
        public TextBox parent;

        private void Update()
        {
            parent.Update();
        }
    }
}
