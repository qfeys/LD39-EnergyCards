using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
class GhostCard : Card
{
    public new string name = "ghost";
    public new Board.Regions validRegions =
        Board.Regions.city | Board.Regions.desert | Board.Regions.offshore | Board.Regions.port | Board.Regions.Road;
    
    static List<GhostCard> presentGhosts;

    public GhostCard(Vector2 pos)
    {
        card = new GameObject("ghost_card", typeof(RectTransform));

        card.transform.SetParent(God.theOne.board_go.transform, false);
        RectTransform rt = card.transform as RectTransform;
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 0);
        rt.pivot = new Vector2(0, 0);
        rt.sizeDelta = new Vector2(120, 120);
        rt.anchoredPosition = pos;

        Image bg = card.AddComponent<Image>();
        bg.sprite = ImageLibrary.GetImage("ghost_card");
        bg.type = Image.Type.Sliced;
        bg.color = Color.gray;

        God.theOne.activeCards.Add(this);
        card.AddComponent<MiniCardScript>().parent = this;
    }

    public static void CreateGhostsAt(List<Vector2 > locations)
    {
        presentGhosts = new List<GhostCard>();
        locations.ForEach(loc =>
        {
            GhostCard gc = new GhostCard(loc);
            presentGhosts.Add(gc);
        });
    }

    public static void RemoveGhosts()
    {
        if(presentGhosts != null)
        presentGhosts.ForEach(g =>
        {
            Object.Destroy(g.card);
        });
        presentGhosts = null;
    }
}
