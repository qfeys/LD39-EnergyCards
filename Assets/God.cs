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
    public Sprite carBackground;


    // Use this for initialization
    void Start () {
        if (theOne == null)
            theOne = this;
        Card.AddToHand();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
