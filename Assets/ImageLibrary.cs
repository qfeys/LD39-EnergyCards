using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

static class ImageLibrary
{
    public static Sprite GetImage(string name)
    {
        return God.theOne.cardBackground;
    }
}

