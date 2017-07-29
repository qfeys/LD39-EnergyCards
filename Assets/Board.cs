using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static class Board
{
    static Card[,] city = new Card[5, 5];
    static Card[,] port = new Card[3, 3];
    static Card[,] offshore = new Card[4, 4];
    static Card[,] offshoreRoad = new Card[3, 3];
    static Card[,] desert = new Card[4, 4];
    static Card[,] desertRoad = new Card[3, 3];

    [Flags] public enum Regions { city = 1, port = 2, offshore = 4, offshoreRoad = 8, desert = 16, desertRoad = 32, Road = offshoreRoad | desertRoad }

    static public void DisplayValidSpots(Card target)
    {
        Regions r = target.validRegions;
        List<Vector2> locations = new List<Vector2>();
        if ((r & Regions.city) == Regions.city)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (city[i, j] == null)
                        locations.Add(CityLoc(i, j));
                }
            }
        }
        if ((r & Regions.port) == Regions.port)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (city[i, j] == null)
                        locations.Add(PortLoc(i, j));
                }
            }
        }
        if ((r & Regions.offshore) == Regions.offshore)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (city[i, j] == null)
                        locations.Add(OshLoc(i, j));
                }
            }
        }
        if ((r & Regions.offshoreRoad) == Regions.offshoreRoad)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (city[i, j] == null)
                        locations.Add(OshrLoc(i, j));
                }
            }
        }
        if ((r & Regions.desert) == Regions.desert)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (city[i, j] == null)
                        locations.Add(DesertLoc(i, j));
                }
            }
        }
        if ((r & Regions.desertRoad) == Regions.desertRoad)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (city[i, j] == null)
                        locations.Add(DsrrLoc(i, j));
                }
            }
        }
        GhostCard.CreateGhostsAt(locations);
    }

    internal static void AddCard(Card card, Vector2 pos)
    {
        if (pos.y < CITY_OFFSET_Y * GRID_SIZE)
        {
            int x = (int)(pos.x / GRID_SIZE - PORT_OFFSET_X);
            int y = (int)(pos.y / GRID_SIZE - PORT_OFFSET_Y);
            if (port[x, y] != null)
                throw new Exception("Cant put card at " + x + ", " + y);
            port[x, y] = card;
        }
        else if (pos.x < OSHR_OFFSET_X * GRID_SIZE)
        {
            int x = (int)(pos.x / GRID_SIZE - OFFSHORE_OFFSET_X);
            int y = (int)(pos.y / GRID_SIZE - OFFSHORE_OFFSET_Y);
            if (offshore[x, y] != null)
                throw new Exception("Cant put card at " + x + ", " + y);
            offshore[x, y] = card;
        }
        else if (pos.x < CITY_OFFSET_X * GRID_SIZE)
        {
            int x = (int)(pos.x / GRID_SIZE - OSHR_OFFSET_X);
            int y = (int)(pos.y / GRID_SIZE - OSHR_OFFSET_Y);
            if (offshoreRoad[x, y] != null)
                throw new Exception("Cant put card at " + x + ", " + y);
            offshoreRoad[x, y] = card;
        }
        else if (pos.x < DSRR_OFFSET_X * GRID_SIZE)
        {
            int x = (int)(pos.x / GRID_SIZE - CITY_OFFSET_X);
            int y = (int)(pos.y / GRID_SIZE - CITY_OFFSET_Y);
            if (city[x, y] != null)
                throw new Exception("Cant put card at " + x + ", " + y);
            city[x, y] = card;
        }
        else if (pos.x < DESERT_OFFSET_X * GRID_SIZE)
        {
            int x = (int)(pos.x / GRID_SIZE - DSRR_OFFSET_X);
            int y = (int)(pos.y / GRID_SIZE - DSRR_OFFSET_Y);
            if (desertRoad[x, y] != null)
                throw new Exception("Cant put card at " + x + ", " + y);
            desertRoad[x, y] = card;
        }
        else
        {
            int x = (int)(pos.x / GRID_SIZE - DESERT_OFFSET_X);
            int y = (int)(pos.y / GRID_SIZE - DESERT_OFFSET_Y);
            if (desert[x, y] != null)
                throw new Exception("Cant put card at " + x + ", " + y);
            desert[x, y] = card;
        }
    }

    internal static void ClearGhosts()
    {
        GhostCard.RemoveGhosts();
    }

    static Vector2 CityLoc(int x, int y) { return new Vector2((CITY_OFFSET_X + x) * GRID_SIZE, (CITY_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 PortLoc(int x, int y) { return new Vector2((PORT_OFFSET_X + x) * GRID_SIZE, (PORT_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 OshLoc(int x, int y) { return new Vector2((OFFSHORE_OFFSET_X + x) * GRID_SIZE, (OFFSHORE_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 OshrLoc(int x, int y) { return new Vector2((OSHR_OFFSET_X + x) * GRID_SIZE, (OSHR_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 DesertLoc(int x, int y) { return new Vector2((DESERT_OFFSET_X + x) * GRID_SIZE, (DESERT_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 DsrrLoc(int x, int y) { return new Vector2((DSRR_OFFSET_X + x) * GRID_SIZE, (DSRR_OFFSET_Y + y) * GRID_SIZE); }

    const float GRID_SIZE = 120;
    const float CITY_OFFSET_X = 10;
    const float CITY_OFFSET_Y = 6;
    const float PORT_OFFSET_X = 11;
    const float PORT_OFFSET_Y = 2;
    const float OFFSHORE_OFFSET_X = 1;
    const float OFFSHORE_OFFSET_Y = 6.5f;
    const float OSHR_OFFSET_X = 6;
    const float OSHR_OFFSET_Y = 7;
    const float DESERT_OFFSET_X = 20;
    const float DESERT_OFFSET_Y = 6.5f;
    const float DSRR_OFFSET_X = 16;
    const float DSRR_OFFSET_Y = 7;
}
