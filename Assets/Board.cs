using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Board
{
    Card[,] city = new Card[5, 5];
    Card[,] port = new Card[3, 3];
    Card[,] offshore = new Card[4, 4];
    Card[,] offshoreRoad = new Card[3, 3];
    Card[,] desert = new Card[4, 4];
    Card[,] desertRoad = new Card[3, 3];

    [Flags] public enum Regions { city = 1, port = 2, offshore = 4, offshoreRoad = 8, desert = 16, desertRoad = 32, Road = offshoreRoad | desertRoad }



    const float GRID_SIZE = 120;
    const float CITY_OFFSET_X = 9;
    const float CITY_OFFSET_Y = 6;
    const float PORT_OFFSET_X = 10;
    const float PORT_OFFSET_Y = 2;
    const float OFFSHORE_OFFSET_X = 1;
    const float OFFSHORE_OFFSET_Y = 6.5f;
    const float OSHR_OFFSET_X = 6;
    const float OSHR_OFFSET_Y = 7;
    const float DESERT_OFFSET_X = 19;
    const float DESERT_OFFSET_Y = 6.5f;
    const float DSRR_OFFSET_X = 15;
    const float DSRR_OFFSET_Y = 7;
}
