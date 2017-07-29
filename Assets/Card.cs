using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    abstract class Card
    {
        public GameObject handCard;
        public GameObject boardCard;

        static GameObject AddToHand()
        {
            GameObject go = new GameObject("card", typeof(RectTransform));

            return go;
        }

        class CardScript : MonoBehaviour
        {

        }

    }
}
