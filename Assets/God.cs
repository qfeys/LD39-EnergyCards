using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class God : MonoBehaviour {
    public static God theOne;
    

    public GameObject hand;
    public GameObject board_go;

    public Sprite miniCardBackground;
    public Sprite ghost_card;
    public Sprite board_border;
    public Sprite bin;
    public Sprite sliced_bg;
    public Font standardFont;

    public AudioClip playCard;
    public AudioClip binCard;

    internal List<Card> activeCards;

    Vector3 mouseposition;
    float mouseHeight = 9;
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
        Tutor.Create();
        Tooltip.Create();
        GameMaster.Start();

        mouseZ = Camera.main.transform.position.z * Mathf.Atan(Camera.main.transform.rotation.eulerAngles.x * Mathf.Deg2Rad);
	}

    const float MOUSE_SPEED_MOD = 1f;
	
	// Update is called once per frame
	void Update () {


        // Mouse movement
        if (Input.GetMouseButton(1))
        {
            Vector3 newMousePos = Input.mousePosition;
            newMousePos.z = mouseZ;
            float deltax = -(Camera.main.ScreenToWorldPoint(mouseposition) - Camera.main.ScreenToWorldPoint(newMousePos)).x;
            float deltay = -(Camera.main.ScreenToWorldPoint(mouseposition) - Camera.main.ScreenToWorldPoint(newMousePos)).y *
                Mathf.Atan(Camera.main.transform.rotation.eulerAngles.x * Mathf.Deg2Rad);
            Camera.main.transform.Translate(new Vector3(deltax,deltay) * MOUSE_SPEED_MOD);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -mouseHeight);
        }
        mouseposition = Input.mousePosition;
        mouseposition.z = mouseZ;
        if (Input.mouseScrollDelta.y != 0)
        {
            mouseHeight -= Input.mouseScrollDelta.y;
            Camera.main.transform.position =
                new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -mouseHeight);
            mouseZ = Camera.main.transform.position.z * Mathf.Atan(Camera.main.transform.rotation.eulerAngles.x * Mathf.Deg2Rad);
        }
    }

    public IEnumerator Perform(IEnumerator coroutine)
    {
        yield return StartCoroutine(coroutine);
    }

    internal void DisplayDefeat()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndScreen");
        GameObject.Find("FinalText").GetComponent<Text>().text = "You lost. Better luck next time.";
        GameObject.Find("FinalText").GetComponent<AudioSource>().Play();
    }

    internal void DisplayVictory()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndScreen");
        GameObject.Find("FinalText").GetComponent<Text>().text = "You won! Congratulations";
        GameObject.Find("FinalText").GetComponent<AudioSource>().Play();
    }

    public void EnableCardDestruction()
    {
        Card.CardDestruction = !Card.CardDestruction;
        Debug.Log("Card destruction: " + Card.CardDestruction);
    }

    public void PlayCardSound()
    {
        GetComponent<AudioSource>().clip = playCard;
        GetComponent<AudioSource>().Play();
    }

    public void PlayBinSound()
    {
        GetComponent<AudioSource>().clip = binCard;
        GetComponent<AudioSource>().Play();
    }
}
