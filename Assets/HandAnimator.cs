using UnityEngine;
using System.Collections;

public class HandAnimator : MonoBehaviour {

    public bool down = false;
    public bool fullyDown = false;
    const float POSITION_UP = 0.2f;
    const float POSITION_DOWN = 0.6f;
    const float POSITION_FULLY_DOWN = 0.9f;
    const float MARGIN = 0.05f;
    const float SPEED = 1.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        RectTransform rt = ((RectTransform)transform);
        if (fullyDown)
        {
            if (rt.pivot.y < POSITION_FULLY_DOWN)
                rt.pivot = new Vector2(rt.pivot.x, rt.pivot.y + SPEED * Time.deltaTime);
        }
        else
        {
            if (!down & rt.pivot.y > POSITION_UP)
            {
                rt.pivot = new Vector2(rt.pivot.x, rt.pivot.y - SPEED * Time.deltaTime);

            }
            else if (down)
            {
                if (rt.pivot.y < POSITION_DOWN - MARGIN)
                {
                    rt.pivot = new Vector2(rt.pivot.x, rt.pivot.y + SPEED * Time.deltaTime);
                }
                else if (rt.pivot.y > POSITION_DOWN + MARGIN)
                {
                    rt.pivot = new Vector2(rt.pivot.x, rt.pivot.y - SPEED * Time.deltaTime);
                }
                else
                {
                    rt.pivot = new Vector2(rt.pivot.x, rt.pivot.y + (-rt.pivot.y + POSITION_DOWN) / 2);
                    Debug.Log("Works");
                }
            }
        }
	}
}
