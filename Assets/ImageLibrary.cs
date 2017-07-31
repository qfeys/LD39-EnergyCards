using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

static class ImageLibrary
{
    static Dictionary<string, Texture2D> dict;

    public static void LoadGraphics()
    {
        string path = @"Assets\Graphics";
        string[] allPaths = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
        dict = new Dictionary<string, Texture2D>();
        for (int i = 0; i < allPaths.Length; i++)
        {
            byte[] data = File.ReadAllBytes(allPaths[i]);
            Texture2D t = null;
            if (allPaths[i].EndsWith("_long.png"))
                t = new Texture2D(140, 200, TextureFormat.ARGB32, false) {
                    name = Path.GetFileNameWithoutExtension(allPaths[i])
                };
            if (allPaths[i].EndsWith("_big.png"))
                t = new Texture2D(120, 100, TextureFormat.ARGB32, false) {
                    name = Path.GetFileNameWithoutExtension(allPaths[i])
                };
            if (allPaths[i].EndsWith("_mini.png"))
                t = new Texture2D(100, 100, TextureFormat.ARGB32, false) {
                    name = Path.GetFileNameWithoutExtension(allPaths[i])
                };
            if (t == null) continue;
            t.LoadImage(data);
            dict.Add(Path.GetFileNameWithoutExtension(allPaths[i]), t);
        }
        Debug.Log("Loaded " + allPaths.Length + " graphics.");
    }

    public static Sprite GetImage(string name)
    {
        if (name == "background_minicards")
            return God.theOne.miniCardBackground;
        if (name == "ghost_card")
            return God.theOne.ghost_card;
        if (name == "bin")
            return God.theOne.bin;
        if (name == "tooltip")
            return God.theOne.sliced_bg;
        if (name == "tutor")
            return God.theOne.sliced_bg;
        if (dict.ContainsKey(name))
        {
            Texture2D t = dict[name];
            Sprite sp = Sprite.Create(t, new Rect(0, 0, t.width, t.height),
                new Vector2(0.5f, 0.5f), 150, 1, SpriteMeshType.Tight);
            return sp;
        }
        Debug.Log("Failed loading graphic: " + name);
        return God.theOne.miniCardBackground;
    }
}

