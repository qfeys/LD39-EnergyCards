using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour {
    public static God theOne;
    

    public GameObject hand;
    public GameObject board_go;

    public Sprite miniCardBackground;
    public Sprite board_border;
    public Font standardFont;

    internal List<Card> activeCards;

    Vector3 mouseposition;
    float mouseZ;

    // Use this for initialization
    void Start () {
        if (theOne == null)
            theOne = this;
        activeCards = new List<Card>();
        ImageLibrary.LoadGraphics();
        Board.Create();
        Deck.Create();
        Bin.Create();
        GameMaster.Start();

        mouseZ = Camera.main.transform.position.y * Mathf.Atan(Camera.main.transform.rotation.eulerAngles.x * Mathf.Deg2Rad);
	}

    const float MOUSE_SPEED_MOD = 2f;
	
	// Update is called once per frame
	void Update () {


        // Mouse movement
        if (Input.GetMouseButton(1))
        {
            Vector3 newMousePos = Input.mousePosition;
            newMousePos.z = mouseZ;
            Camera.main.transform.Translate((Camera.main.ScreenToWorldPoint(mouseposition) - Camera.main.ScreenToWorldPoint(newMousePos)) * MOUSE_SPEED_MOD);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10);
        }
        mouseposition = Input.mousePosition;
        mouseposition.z = mouseZ;
    }

    public IEnumerator Perform(IEnumerator coroutine)
    {
        yield return StartCoroutine(coroutine);
    }

    internal void DisplayDefeat()
    {
        throw new NotImplementedException();
    }

    internal void DisplayVictory()
    {
        throw new NotImplementedException();
    }

    public void EnableCardDestruction()
    {
        Card.CardDestruction = !Card.CardDestruction;
        Debug.Log("Card destruction: " + Card.CardDestruction);
    }
}
