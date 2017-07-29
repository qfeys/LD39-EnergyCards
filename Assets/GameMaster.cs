using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

static class GameMaster
{
    public static void Start()
    {
        /*  1) Deal 4 cards
         *  2) Play 1 card
         *  2.1) check victory
         *  3) Destroy 1 card
         *  4) take 2 cards
         *  5) goto 2)
         * 
         */
        for (int i = 0; i < 4; i++) DealCard();
        //flow = new Thread(() => { try { Flow(); } catch (Exception e) { mainThreadException = e; UnityEngine.Debug.LogError(e); } });
        //flow.Start();
        //UnityEngine.Debug.Log("flow: " + flow.ThreadState);
        God.theOne.StartCoroutine(God.theOne.Perform(Flow()));
    }

    static IEnumerator Flow()
    {
        while (true)
        {
            Card.canPlay = true;
            while (Card.canPlay)
            {
                yield return new WaitForSeconds(0.1f);
            }

            if (CheckVictory())
                break;
            Bin.Activate();
            Card.canBin = true;
            while (Card.canBin)
            {
                yield return new WaitForSeconds(0.1f);
            }
            DealCard();
            DealCard();
        }
    }

    static void DealCard()
    {
        Deck.TakeNext();
    }

    static bool CheckVictory()
    {
        return false;
    }
}
