using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

static class Board
{
    static Card[,] city = new Card[5, 5];
    static Card[,] port = new Card[3, 3];
    static Card[,] offshore = new Card[4, 4];
    static Card[,] offshoreRoad = new Card[3, 3];
    static Card[,] desert = new Card[4, 4];
    static Card[,] desertRoad = new Card[3, 3];

    [Flags] public enum Regions { city = 1, port = 2, offshore = 4, offshoreRoad = 8, desert = 16, desertRoad = 32, Road = offshoreRoad | desertRoad }

    static public void Create()
    {
        Sprite border = God.theOne.board_border;
        {
            GameObject go = new GameObject("border_port", typeof(RectTransform));

            go.transform.SetParent(God.theOne.board_go.transform, false);
            RectTransform rt = go.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 0);
            rt.sizeDelta = new Vector2(3 * GRID_SIZE + 40, 3 * GRID_SIZE + 40);

            rt.anchoredPosition = new Vector2(PORT_OFFSET_X * GRID_SIZE - 20, PORT_OFFSET_Y * GRID_SIZE - 20);

            Image bg = go.AddComponent<Image>();
            bg.sprite = border;
            bg.type = Image.Type.Sliced;
        }
        {
            GameObject go = new GameObject("border_offsh", typeof(RectTransform));

            go.transform.SetParent(God.theOne.board_go.transform, false);
            RectTransform rt = go.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 0);
            rt.sizeDelta = new Vector2(4 * GRID_SIZE + 40, 4 * GRID_SIZE + 40);

            rt.anchoredPosition = new Vector2(OFFSHORE_OFFSET_X * GRID_SIZE - 20, OFFSHORE_OFFSET_Y * GRID_SIZE - 20);

            Image bg = go.AddComponent<Image>();
            bg.sprite = border;
            bg.type = Image.Type.Sliced;
        }
        {
            GameObject go = new GameObject("oshr_port", typeof(RectTransform));

            go.transform.SetParent(God.theOne.board_go.transform, false);
            RectTransform rt = go.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 0);
            rt.sizeDelta = new Vector2(3 * GRID_SIZE + 40, 3 * GRID_SIZE + 40);

            rt.anchoredPosition = new Vector2(OSHR_OFFSET_X * GRID_SIZE - 20, OSHR_OFFSET_Y * GRID_SIZE - 20);

            Image bg = go.AddComponent<Image>();
            bg.sprite = border;
            bg.type = Image.Type.Sliced;
        }
        {
            GameObject go = new GameObject("border_city", typeof(RectTransform));

            go.transform.SetParent(God.theOne.board_go.transform, false);
            RectTransform rt = go.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 0);
            rt.sizeDelta = new Vector2(5 * GRID_SIZE + 40, 5 * GRID_SIZE + 40);

            rt.anchoredPosition = new Vector2(CITY_OFFSET_X * GRID_SIZE - 20, CITY_OFFSET_Y * GRID_SIZE - 20);

            Image bg = go.AddComponent<Image>();
            bg.sprite = border;
            bg.type = Image.Type.Sliced;
        }
        {
            GameObject go = new GameObject("border_dsrr", typeof(RectTransform));

            go.transform.SetParent(God.theOne.board_go.transform, false);
            RectTransform rt = go.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 0);
            rt.sizeDelta = new Vector2(3 * GRID_SIZE + 40, 3 * GRID_SIZE + 40);

            rt.anchoredPosition = new Vector2(DSRR_OFFSET_X * GRID_SIZE - 20, DSRR_OFFSET_Y * GRID_SIZE - 20);

            Image bg = go.AddComponent<Image>();
            bg.sprite = border;
            bg.type = Image.Type.Sliced;
        }
        {
            GameObject go = new GameObject("border_desert", typeof(RectTransform));

            go.transform.SetParent(God.theOne.board_go.transform, false);
            RectTransform rt = go.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 0);
            rt.sizeDelta = new Vector2(4 * GRID_SIZE + 40, 4 * GRID_SIZE + 40);

            rt.anchoredPosition = new Vector2(DESERT_OFFSET_X * GRID_SIZE - 20, DESERT_OFFSET_Y * GRID_SIZE - 20);

            Image bg = go.AddComponent<Image>();
            bg.sprite = border;
            bg.type = Image.Type.Sliced;
        }
        {
            InfoTable ofshStats = new InfoTable(God.theOne.board_go.transform, new List<Tuple<string, Func<object>>>() {
                new Tuple<string, Func<object>>("Power Generation",()=>State.SeaEnergy),
                new Tuple<string, Func<object>>("Gas Production",()=>State.SeaGasProd)
            }, 500, 36);
            RectTransform rt = ofshStats.gameObject.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 1);

            rt.anchoredPosition = new Vector2(OFFSHORE_OFFSET_X * GRID_SIZE + 60, OFFSHORE_OFFSET_Y * GRID_SIZE - 20);
        }
        {
            InfoTable ofshRdStats = new InfoTable(God.theOne.board_go.transform, new List<Tuple<string, Func<object>>>() {
                new Tuple<string, Func<object>>("Powerline Capacity",()=>State.SeaCables),
                new Tuple<string, Func<object>>("Gasline Capacity",()=>State.SeaPipes)
            }, 500, 36);
            RectTransform rt = ofshRdStats.gameObject.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 0);

            rt.anchoredPosition = new Vector2(OSHR_OFFSET_X * GRID_SIZE - 40, (OSHR_OFFSET_Y + 3) * GRID_SIZE + 40);
        }
        {
            InfoTable cityStats = new InfoTable(God.theOne.board_go.transform, new List<Tuple<string, Func<object>>>() {
                new Tuple<string, Func<object>>("Power Generation",()=>State.CityEnergy),
                new Tuple<string, Func<object>>("Battery Storage",()=>State.CityEnergyStore),
                new Tuple<string, Func<object>>("Coal consumtion",()=>State.CityCoal),
                new Tuple<string, Func<object>>("Oil consumtion",()=>State.CityOil),
                new Tuple<string, Func<object>>("Oil Storage",()=>State.CityOilStore),
                new Tuple<string, Func<object>>("Gas consumtion",()=>State.CityOil),
                new Tuple<string, Func<object>>("Gas Storage",()=>State.CityGasStore)
            }, 500, 36);
            RectTransform rt = cityStats.gameObject.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(1, 1);

            rt.anchoredPosition = new Vector2(CITY_OFFSET_X * GRID_SIZE - 40, CITY_OFFSET_Y * GRID_SIZE + 40);
        }
        {
            InfoTable portStats = new InfoTable(God.theOne.board_go.transform, new List<Tuple<string, Func<object>>>() {
                new Tuple<string, Func<object>>("Power Generation",()=>State.PortEnergy),
                new Tuple<string, Func<object>>("Coal income",()=>State.PortCoal),
                new Tuple<string, Func<object>>("Oil income",()=>State.PortOil),
                new Tuple<string, Func<object>>("Gas consumtion",()=>State.PortGas)
            }, 500, 36);
            RectTransform rt = portStats.gameObject.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 1);

            rt.anchoredPosition = new Vector2((CITY_OFFSET_X +4)* GRID_SIZE + 40, CITY_OFFSET_Y * GRID_SIZE - 80);
        }
        {
            InfoTable DsrRdStats = new InfoTable(God.theOne.board_go.transform, new List<Tuple<string, Func<object>>>() {
                new Tuple<string, Func<object>>("Powerline Capacity",()=>State.DesertCables),
                new Tuple<string, Func<object>>("Oilline Capacity",()=>State.DesertOilPipes),
                new Tuple<string, Func<object>>("Gasline Capacity",()=>State.DesertGasPipes)
            }, 500, 36);
            RectTransform rt = DsrRdStats.gameObject.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 0);

            rt.anchoredPosition = new Vector2(DSRR_OFFSET_X * GRID_SIZE - 40, (DSRR_OFFSET_Y + 3) * GRID_SIZE + 40);
        }
        {
            InfoTable desStats = new InfoTable(God.theOne.board_go.transform, new List<Tuple<string, Func<object>>>() {
                new Tuple<string, Func<object>>("Power Generation",()=>State.DesertEnergy),
                new Tuple<string, Func<object>>("Battery Storage",()=>State.DesertEnergyStore),
                new Tuple<string, Func<object>>("Oil Production",()=>State.DesertOilProd),
                new Tuple<string, Func<object>>("Oil Storage",()=>State.DesertOilStore),
                new Tuple<string, Func<object>>("Gas Production",()=>State.DesertGasProd),
                new Tuple<string, Func<object>>("Gas Storage",()=>State.DesertGasStore)
            }, 500, 36);
            RectTransform rt = desStats.gameObject.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 1);

            rt.anchoredPosition = new Vector2(DESERT_OFFSET_X * GRID_SIZE + 00, DESERT_OFFSET_Y * GRID_SIZE - 20);
        }
    }

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
            Debug.Log("Add card to port at: " + x + ", " + y);
        }
        else if (pos.x < OSHR_OFFSET_X * GRID_SIZE)
        {
            int x = (int)(pos.x / GRID_SIZE - OFFSHORE_OFFSET_X);
            int y = (int)(pos.y / GRID_SIZE - OFFSHORE_OFFSET_Y);
            if (offshore[x, y] != null)
                throw new Exception("Cant put card at " + x + ", " + y);
            offshore[x, y] = card;
            Debug.Log("Add card to offshore at: " + x + ", " + y);
        }
        else if (pos.x < CITY_OFFSET_X * GRID_SIZE)
        {
            int x = (int)(pos.x / GRID_SIZE - OSHR_OFFSET_X);
            int y = (int)(pos.y / GRID_SIZE - OSHR_OFFSET_Y);
            if (offshoreRoad[x, y] != null)
                throw new Exception("Cant put card at " + x + ", " + y);
            offshoreRoad[x, y] = card;
            Debug.Log("Add card to oshr at: " + x + ", " + y);
        }
        else if (pos.x < DSRR_OFFSET_X * GRID_SIZE)
        {
            int x = (int)(pos.x / GRID_SIZE - CITY_OFFSET_X);
            int y = (int)(pos.y / GRID_SIZE - CITY_OFFSET_Y);
            if (city[x, y] != null)
                throw new Exception("Cant put card at " + x + ", " + y);
            city[x, y] = card;
            Debug.Log("Add card to city at: " + x + ", " + y);
        }
        else if (pos.x < DESERT_OFFSET_X * GRID_SIZE)
        {
            int x = (int)(pos.x / GRID_SIZE - DSRR_OFFSET_X);
            int y = (int)(pos.y / GRID_SIZE - DSRR_OFFSET_Y);
            if (desertRoad[x, y] != null)
                throw new Exception("Cant put card at " + x + ", " + y);
            desertRoad[x, y] = card;
            Debug.Log("Add card to dsrr at: " + x + ", " + y);
        }
        else
        {
            int x = (int)(pos.x / GRID_SIZE - DESERT_OFFSET_X);
            int y = (int)(pos.y / GRID_SIZE - DESERT_OFFSET_Y);
            if (desert[x, y] != null)
                throw new Exception("Cant put card at " + x + ", " + y);
            desert[x, y] = card;
            Debug.Log("Add card to desert at: " + x + ", " + y);
        }
    }

    internal static void AddCard(Card card, Regions reg, int x, int y)
    {
        switch (reg)
        {
        case Regions.city:
            if (city[x, y] != null)
                throw new Exception("Cant put card at " + x + ", " + y);
            city[x, y] = card;
            break;
        case Regions.port:
            if (port[x, y] != null)
                throw new Exception("Cant put card at " + x + ", " + y);
            port[x, y] = card;
            break;
        }
    }

    internal static void ClearGhosts()
    {
        GhostCard.RemoveGhosts();
    }

    static List<Card> allCards { get
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (city[i, j] != null) cards.Add(city[i, j]);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (port[i, j] != null) cards.Add(port[i, j]);
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (offshore[i, j] != null) cards.Add(offshore[i, j]);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (offshoreRoad[i, j] != null) cards.Add(offshoreRoad[i, j]);
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (desert[i, j] != null) cards.Add(desert[i, j]);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (desertRoad[i, j] != null) cards.Add(desertRoad[i, j]);
            return cards;
        } }

    static Vector2 CityLoc(int x, int y) { return new Vector2((CITY_OFFSET_X + x) * GRID_SIZE, (CITY_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 PortLoc(int x, int y) { return new Vector2((PORT_OFFSET_X + x) * GRID_SIZE, (PORT_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 OshLoc(int x, int y) { return new Vector2((OFFSHORE_OFFSET_X + x) * GRID_SIZE, (OFFSHORE_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 OshrLoc(int x, int y) { return new Vector2((OSHR_OFFSET_X + x) * GRID_SIZE, (OSHR_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 DesertLoc(int x, int y) { return new Vector2((DESERT_OFFSET_X + x) * GRID_SIZE, (DESERT_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 DsrrLoc(int x, int y) { return new Vector2((DSRR_OFFSET_X + x) * GRID_SIZE, (DSRR_OFFSET_Y + y) * GRID_SIZE); }

    const float GRID_SIZE = 120;
    const float CITY_OFFSET_X = 10;
    const float CITY_OFFSET_Y = 5;
    const float PORT_OFFSET_X = 11;
    const float PORT_OFFSET_Y = 1;
    const float OFFSHORE_OFFSET_X = 1;
    const float OFFSHORE_OFFSET_Y = 5.5f;
    const float OSHR_OFFSET_X = 6;
    const float OSHR_OFFSET_Y = 6;
    const float DESERT_OFFSET_X = 20;
    const float DESERT_OFFSET_Y = 5.5f;
    const float DSRR_OFFSET_X = 16;
    const float DSRR_OFFSET_Y = 6;

    static internal class Network
    {
        public static float CalculatePower() // TODO: ALL THE THINGS!!!!!!
        {
            float offshSupply = Mathf.Min(State.SeaEnergy, State.SeaCables);
            float desertSuplly = Mathf.Min(State.DesertEnergy, State.DesertCables);
            float portSupply = State.PortEnergy;
            float citySupply = State.CityEnergy;
            return offshSupply + desertSuplly + portSupply + citySupply;
        }
    }

    static internal class State
    {
        public static float SeaGasProd
        {
            get
            {
                float gas = 20; // base
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (offshore[i, j] != null) gas += Data.Gas(offshore[i, j]);
                return gas;
            }
        }
        public static float SeaEnergy
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (offshore[i, j] != null) val += Data.Energy(offshore[i, j]);
                return val;
            }
        }
        public static float SeaPipes
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (offshoreRoad[i, j] != null) val += Data.Gas(offshoreRoad[i, j]);
                return val;
            }
        }
        public static float SeaCables
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (offshoreRoad[i, j] != null) val += Data.Flow(offshoreRoad[i, j]);
                return val;
            }
        }
        public static float PortCoal
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (port[i, j] != null) val += Data.Coal(port[i, j]);
                return val;
            }
        }
        public static float PortOil
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (port[i, j] != null) val += Data.Oil(port[i, j]);
                return val;
            }
        }
        public static float PortGas
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (port[i, j] != null) val += Data.Gas(port[i, j]);
                return val;
            }
        }
        public static float PortEnergy
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (port[i, j] != null) val += Data.Energy(port[i, j]);
                return val;
            }
        }
        //public static float PortOilStore
        //{
        //    get
        //    {
        //        float val = 0; // base
        //        for (int i = 0; i < 3; i++)
        //            for (int j = 0; j < 3; j++)
        //                if (port[i, j] != null) val += Data.StoreOil(port[i, j]);
        //        return val;
        //    }
        //}
        //public static float PortGasStore
        //{
        //    get
        //    {
        //        float val = 0; // base
        //        for (int i = 0; i < 3; i++)
        //            for (int j = 0; j < 3; j++)
        //                if (port[i, j] != null) val += Data.StoreGas(port[i, j]);
        //        return val;
        //    }
        //}
        public static float CityEnergy
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null) val += Data.Energy(city[i, j]);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (offshoreRoad[i, j] != null) val += Data.Energy(offshoreRoad[i, j]);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (desertRoad[i, j] != null) val += Data.Energy(desertRoad[i, j]);
                return val;
            }
        }
        public static float CityCoal
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null) val += Data.Coal(city[i, j]);
                return val;
            }
        }
        public static float CityOil
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null) val += Data.Oil(city[i, j]);
                return val;
            }
        }
        public static float CityGas
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null) val += Data.Gas(city[i, j]);
                return val;
            }
        }
        public static float CityOilStore
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null) val += Data.StoreOil(city[i, j]);
                return val;
            }
        }
        public static float CityGasStore
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null) val += Data.StoreGas(city[i, j]);
                return val;
            }
        }
        public static float CityEnergyStore
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null) val += Data.StorePow(city[i, j]);
                return val;
            }
        }
        public static float DesertOilPipes
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (desertRoad[i, j] != null) val += Data.Oil(desertRoad[i, j]);
                return val;
            }
        }
        public static float DesertGasPipes
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (desertRoad[i, j] != null) val += Data.Gas(desertRoad[i, j]);
                return val;
            }
        }
        public static float DesertCables
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (desertRoad[i, j] != null) val += Data.Flow(desertRoad[i, j]);
                return val;
            }
        }
        public static float DesertEnergy
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (desert[i, j] != null) val += Data.Energy(desert[i, j]);
                return val;
            }
        }
        public static float DesertOilProd
        {
            get
            {
                float val = 20; // base
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (desert[i, j] != null) val += Data.Oil(desert[i, j]);
                return val;
            }
        }
        public static float DesertGasProd
        {
            get
            {
                float gas = 0; // base
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (desert[i, j] != null) gas += Data.Gas(desert[i, j]);
                return gas;
            }
        }
        public static float DesertEnergyStore
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (desert[i, j] != null) val += Data.StorePow(desert[i, j]);
                return val;
            }
        }
        public static float DesertOilStore
        {
            get
            {
                float val = 20; // base
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (desert[i, j] != null) val += Data.StoreOil(desert[i, j]);
                return val;
            }
        }
        public static float DesertGasStore
        {
            get
            {
                float gas = 0; // base
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (desert[i, j] != null) gas += Data.StoreGas(desert[i, j]);
                return gas;
            }
        }
        public static float TotalWarming
        {
            get
            {
                return allCards.Sum(c => Data.Warming(c));
            }
        }
        public static float TotalResistance
        {
            get
            {
                float val = 0;
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null) val += Data.Resistance(city[i, j],Regions.city);
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (offshore[i, j] != null) val += Data.Resistance(offshore[i, j],Regions.offshore);
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (desert[i, j] != null) val += Data.Resistance(desert[i, j],Regions.desert);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (port[i, j] != null) val += Data.Resistance(port[i, j],Regions.port);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (offshoreRoad[i, j] != null) val += Data.Resistance(offshoreRoad[i, j],Regions.offshoreRoad);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (desertRoad[i, j] != null) val += Data.Resistance(desertRoad[i, j],Regions.desertRoad);
                return val;
            }
        }
    }
}
