using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

static class GameMaster
{
    static int turn;
    static public float powerDemand;
    static public float resistance = 1;
    static public float globalWarming;
    internal static bool removeCard = false;

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
        SetBoardIntoStartingPosition();
        turn = 1;
        for (int i = 0; i < 4; i++) DealCard();
        //flow = new Thread(() => { try { Flow(); } catch (Exception e) { mainThreadException = e; UnityEngine.Debug.LogError(e); } });
        //flow.Start();
        //UnityEngine.Debug.Log("flow: " + flow.ThreadState);
        God.theOne.StartCoroutine(God.theOne.Perform(Flow()));
    }

    private static void SetBoardIntoStartingPosition()
    {
        Board.AddCard(Deck.GetCard("city"), Board.Regions.city, 2, 2);
        Board.AddCard(Deck.GetCard("plant_coal"), Board.Regions.city, 4, 0);
        Board.AddCard(Deck.GetCard("plant_coal"), Board.Regions.city, 3, 1);
        Board.AddCard(Deck.GetCard("plant_coal"), Board.Regions.city, 0, 3);
        Board.AddCard(Deck.GetCard("plant_coal"), Board.Regions.city, 2, 1);
        Board.AddCard(Deck.GetCard("plant_coal"), Board.Regions.city, 2, 0);
        Board.AddCard(Deck.GetCard("trans_rail"), Board.Regions.city, 0, 4);
        Board.AddCard(Deck.GetCard("trans_rail"), Board.Regions.city, 4, 2);
        Board.AddCard(Deck.GetCard("plant_oil"), Board.Regions.port, 0, 2);
        Board.AddCard(Deck.GetCard("plant_oil"), Board.Regions.port, 1, 2);
        Board.AddCard(Deck.GetCard("trans_term_oil"), Board.Regions.port, 1, 0);
        Board.AddCard(Deck.GetCard("trans_term_coal"), Board.Regions.port, 0, 0);
        Board.AddCard(Deck.GetCard("store_oil"), Board.Regions.port, 0, 1);
    }

    static IEnumerator Flow()
    {
        while (true)
        {
            CalculatePower();
            Card.canPlay = true;
            while (Card.canPlay)
            {
                yield return new WaitForSeconds(0.1f);
            }
            CalculateResistance();
            CalculateGW();
            Data.RNG.Update();
            CheckEnd();
            Bin.Activate();
            Card.canBin = true;
            while (Card.canBin)
            {
                yield return new WaitForSeconds(0.1f);
            }
            if (removeCard)
            {
                removeCard = false;
                Card.canBin = true;
                while (Card.canBin)
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
            Board.Modefiers.Update();
            DealCard();
            DealCard();
            turn++;
        }
    }

    private static void CalculatePower()
    {
        powerDemand = 60 + turn * 5;
    }

    private static void CalculateGW()
    {
        globalWarming += Board.State.TotalWarming;
    }

    private static void CalculateResistance()
    {
        resistance += Board.State.TotalResistance;
        Debug.Log("Delta Resistance: " + Board.State.TotalResistance);
        Debug.Log("Resistance: " + resistance);
    }

    static void CheckEnd()
    {
        Board.Network.Process();
        if (Board.Network.cityPowAvailability < 1)
        {
            resistance += 1 - Board.Network.cityPowAvailability;
            Debug.Log("Insufficiant power");
        }

        Debug.Log("POWER availability: " + Board.Network.cityPowAvailability);
        if (resistance > 100)
            God.theOne.DisplayDefeat();
        if (resistance < 0)
            God.theOne.DisplayVictory();

    }

    public static void DealCard()
    {
        Deck.TakeNext();
    }
}
