using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour {
    public static God theOne;


    public float powerDemand;
    public float resistance;
    public float carbonLevel;

    public GameObject hand;
    public GameObject board;

    public Sprite cardBackground;
    public Sprite coal_plant;
    public Font standardFont;

    internal List<Card> activeCards;

    Vector3 mouseposition;
    float mouseZ;

    // Use this for initialization
    void Start () {
        if (theOne == null)
            theOne = this;
        new TestCard();

        mouseZ = Camera.main.transform.position.y * Mathf.Atan(Camera.main.transform.rotation.eulerAngles.x * Mathf.Deg2Rad);
	}
	
	// Update is called once per frame
	void Update () {


        // Mouse movement
        if (Input.GetMouseButton(1))
        {
            Vector3 newMousePos = Input.mousePosition;
            newMousePos.z = mouseZ;
            Camera.main.transform.Translate(Camera.main.ScreenToWorldPoint(mouseposition) - Camera.main.ScreenToWorldPoint(newMousePos));
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10);
        }
        mouseposition = Input.mousePosition;
        mouseposition.z = mouseZ;
    }
}
