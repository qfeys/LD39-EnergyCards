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
                new Tuple<string, Func<object>>("Power Generation",()=>State.SeaEnergy.ToString("0.#")),
                new Tuple<string, Func<object>>("Gas Production",()=>State.SeaGasProd.ToString("0.#"))
            }, 500, 36);
            RectTransform rt = ofshStats.gameObject.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 1);

            rt.anchoredPosition = new Vector2(OFFSHORE_OFFSET_X * GRID_SIZE + 60, OFFSHORE_OFFSET_Y * GRID_SIZE - 20);
        }
        {
            InfoTable ofshRdStats = new InfoTable(God.theOne.board_go.transform, new List<Tuple<string, Func<object>>>() {
                new Tuple<string, Func<object>>("Powerline Capacity",()=>
                    "" + Mathf.Min(State.SeaEnergy, State.SeaCables).ToString("0.#") + "/" + State.SeaCables.ToString("0.#")),
                new Tuple<string, Func<object>>("Gasline Capacity",()=>
                    "" + Mathf.Min(State.SeaGasProd, State.SeaPipes).ToString("0.#") + "/" + State.SeaPipes.ToString("0.#"))
            }, 500, 36);
            RectTransform rt = ofshRdStats.gameObject.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 0);

            rt.anchoredPosition = new Vector2(OSHR_OFFSET_X * GRID_SIZE - 40, (OSHR_OFFSET_Y + 3) * GRID_SIZE + 40);
        }
        {
            InfoTable cityStats = new InfoTable(God.theOne.board_go.transform, new List<Tuple<string, Func<object>>>() {
                new Tuple<string, Func<object>>("City & Port",()=>""),
                new Tuple<string, Func<object>>("Power Generation",()=>(State.CityEnergy + State.PortEnergy).ToString("0.#")),
                new Tuple<string, Func<object>>("Battery Storage",()=>
                    "" + Network.cityPowStored.ToString("0.#") + "/" + State.CityEnergyStore),
                new Tuple<string, Func<object>>("Coal consumtion",()=>(State.CityCoal + State.PortCoal) + "/" + State.TotalCoalProduction),
                new Tuple<string, Func<object>>("Oil consumtion",()=>(State.CityOil + State.PortOil) + "/" + State.TotalCityOilProd),
                new Tuple<string, Func<object>>("Oil Storage",()=>
                    "" + Network.cityOilStored.ToString("0.#") + "/" + State.CityOilStore),
                new Tuple<string, Func<object>>("Gas consumtion",()=>(State.CityGas + State.PortGas) + "/" + State.TotalCityGasProd),
                new Tuple<string, Func<object>>("Gas Storage",()=>
                    "" + Network.cityOilStored.ToString("0.#") + "/" + State.CityGasStore)
            }, 500, 36);
            RectTransform rt = cityStats.gameObject.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(1, 1);

            rt.anchoredPosition = new Vector2(CITY_OFFSET_X * GRID_SIZE - 40, CITY_OFFSET_Y * GRID_SIZE + 40);
        }
        //{
        //    InfoTable portStats = new InfoTable(God.theOne.board_go.transform, new List<Tuple<string, Func<object>>>() {
        //        new Tuple<string, Func<object>>("Power Generation",()=>State.PortEnergy),
        //        new Tuple<string, Func<object>>("Coal income",()=>State.PortCoal),
        //        new Tuple<string, Func<object>>("Oil income",()=>State.PortOil),
        //        new Tuple<string, Func<object>>("Gas consumtion",()=>State.PortGas)
        //    }, 500, 36);
        //    RectTransform rt = portStats.gameObject.transform as RectTransform;
        //    rt.anchorMin = new Vector2(0, 0);
        //    rt.anchorMax = new Vector2(0, 0);
        //    rt.pivot = new Vector2(0, 1);

        //    rt.anchoredPosition = new Vector2((CITY_OFFSET_X +4)* GRID_SIZE + 40, CITY_OFFSET_Y * GRID_SIZE - 80);
        //}
        {
            InfoTable DsrRdStats = new InfoTable(God.theOne.board_go.transform, new List<Tuple<string, Func<object>>>() {
                new Tuple<string, Func<object>>("Powerline Capacity",()=>
                    "" + Mathf.Min(State.DesertEnergy, State.DesertCables).ToString("0.#") + "/" + State.DesertCables),
                new Tuple<string, Func<object>>("Oilline Capacity",()=>
                    "" + Mathf.Min(State.DesertGasProd, State.DesertGasPipes).ToString("0.#") + "/" + State.DesertOilPipes),
                new Tuple<string, Func<object>>("Gasline Capacity",()=>
                    "" + Mathf.Min(State.DesertGasProd, State.DesertGasPipes).ToString("0.#") + "/" + State.DesertGasPipes)
            }, 500, 36);
            RectTransform rt = DsrRdStats.gameObject.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 0);

            rt.anchoredPosition = new Vector2(DSRR_OFFSET_X * GRID_SIZE - 40, (DSRR_OFFSET_Y + 3) * GRID_SIZE + 40);
        }
        {
            InfoTable desStats = new InfoTable(God.theOne.board_go.transform, new List<Tuple<string, Func<object>>>() {
                new Tuple<string, Func<object>>("Power Generation",()=>State.DesertEnergy.ToString("0.#")),
                new Tuple<string, Func<object>>("Battery Storage",()=>
                    "" + Network.desertPowStored.ToString("0.#") + "/" + State.DesertEnergyStore),
                new Tuple<string, Func<object>>("Oil Production",()=>State.DesertOilProd + "/" + State.TotalDesertOilProd),
                new Tuple<string, Func<object>>("Oil Storage",()=>
                    "" + Network.desertOilStored.ToString("0.#") + "/" + State.DesertOilStore),
                new Tuple<string, Func<object>>("Gas Production",()=>State.DesertGasProd + "/" + State.TotalDesertGasCons),
                new Tuple<string, Func<object>>("Gas Storage",()=>
                    "" + Network.desertGasStored.ToString("0.#") + "/" + State.DesertGasStore)
            }, 500, 36);
            RectTransform rt = desStats.gameObject.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 1);

            rt.anchoredPosition = new Vector2(DESERT_OFFSET_X * GRID_SIZE + 00, DESERT_OFFSET_Y * GRID_SIZE - 20);
        }
        {
            InfoTable GWStats = new InfoTable(God.theOne.board_go.transform, new List<Tuple<string, Func<object>>>() {
                new Tuple<string, Func<object>>("Global warming",()=> "+" + GameMaster.globalWarming.ToString("0.000") + "°C")
            }, 900, 54);
            RectTransform rt = GWStats.gameObject.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(0, 1);
            rt.pivot = new Vector2(0, 1);

            rt.anchoredPosition = new Vector2(20, -40);
        }
        {
            InfoTable PowerStats = new InfoTable(God.theOne.board_go.transform, new List<Tuple<string, Func<object>>>() {
                new Tuple<string, Func<object>>("Required Power",()=> (Network.cityPowAvailability * GameMaster.powerDemand) + "/" + GameMaster.powerDemand + "GW")
            }, 800, 54);
            RectTransform rt = PowerStats.gameObject.transform as RectTransform;
            rt.anchorMin = new Vector2(0.5f, 1);
            rt.anchorMax = new Vector2(0.5f, 1);
            rt.pivot = new Vector2(0.5f, 1);

            rt.anchoredPosition = new Vector2(0, -20);
        }
        {
            InfoTable ResStats = new InfoTable(God.theOne.board_go.transform, new List<Tuple<string, Func<object>>>() {
                new Tuple<string, Func<object>>("Resistance",()=> GameMaster.resistance + "%")
            }, 800, 54);
            RectTransform rt = ResStats.gameObject.transform as RectTransform;
            rt.anchorMin = new Vector2(1, 1);
            rt.anchorMax = new Vector2(1, 1);
            rt.pivot = new Vector2(1, 1);

            rt.anchoredPosition = new Vector2(-20, -40);
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
                    if (port[i, j] == null)
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
                    if (offshore[i, j] == null)
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
                    if (offshoreRoad[i, j] == null)
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
                    if (desert[i, j] == null)
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
                    if (desertRoad[i, j] == null)
                        locations.Add(DsrrLoc(i, j));
                }
            }
        }
        GhostCard.CreateGhostsAt(locations);
    }

    internal static void AddCard(Card card, Vector2 pos)
    {
        if (card.name.Substring(0, 5) == "polit")
            Modefiers.AddMod(card);
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
            card.CreateMini(CityLoc(x, y));
            break;
        case Regions.port:
            if (port[x, y] != null)
                throw new Exception("Cant put card at " + x + ", " + y);
            port[x, y] = card;
            card.CreateMini(PortLoc(x, y));
            break;
        }
    }

    internal static void RemoveCard(Card card)
    {
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                if (city[i, j] != null && city[i, j].Equals(card)) city[i, j] = null;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (port[i, j] != null && port[i, j].Equals(card)) port[i, j] = null;
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                if (offshore[i, j] != null && offshore[i, j].Equals(card)) offshore[i, j] = null;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (offshoreRoad[i, j] != null && offshoreRoad[i, j].Equals(card)) offshoreRoad[i, j] = null;
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                if (desert[i, j] != null && desert[i, j].Equals(card)) desert[i, j] = null;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (desertRoad[i, j] != null && desertRoad[i, j].Equals(card)) desertRoad[i, j] = null;
    }

    internal static void ClearGhosts()
    {
        GhostCard.RemoveGhosts();
    }

    static List<Card> AllCards { get
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

    static Vector2 CityLoc(int x, int y)    { return new Vector2((CITY_OFFSET_X + x) * GRID_SIZE,   (CITY_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 PortLoc(int x, int y)    { return new Vector2((PORT_OFFSET_X + x) * GRID_SIZE,   (PORT_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 OshLoc(int x, int y)     { return new Vector2((OFFSHORE_OFFSET_X + x) * GRID_SIZE, (OFFSHORE_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 OshrLoc(int x, int y)    { return new Vector2((OSHR_OFFSET_X + x) * GRID_SIZE,   (OSHR_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 DesertLoc(int x, int y)  { return new Vector2((DESERT_OFFSET_X + x) * GRID_SIZE, (DESERT_OFFSET_Y + y) * GRID_SIZE); }
    static Vector2 DsrrLoc(int x, int y)    { return new Vector2((DSRR_OFFSET_X + x) * GRID_SIZE,   (DSRR_OFFSET_Y + y) * GRID_SIZE); }

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
        internal static float coalAvailability;
        internal static float desertOilAvailability;
        internal static float desertOilStored;
        internal static float cityOilAvailability;
        internal static float cityOilStored;
        internal static float cityGasStored;
        internal static float desertPowStored;
        internal static float gasFlow;       // positive from desert to city
        internal static float cityGasAvailability;
        internal static float desertGasAvailability;
        internal static float ofshPow;
        internal static float desertPow;
        internal static float cityPow;
        internal static float cityPowStored;
        internal static float cityPowAvailability;
        internal static float desToCityPowFlow;
        internal static float desertGasStored;

        public static void Process()
        {
            CheckCoalAvailability();
            CheckOilAvailability();
            CheckGasAvailability();
            CalculatePower();
            CheckPowerAvailability();
        }

        private static void CheckCoalAvailability()
        {
            if(State.CityCoal + State.PortCoal >= 0)
            {
                coalAvailability = 1;
            }
            else
            {
                float shortage = State.CityCoal + State.PortCoal;
                float prod = State.TotalCoalProduction;
                coalAvailability = prod / (prod - shortage);
            }
        }

        private static void CheckOilAvailability()
        {
            /*  1) check oil in desert
             *  2) check oil in city
             *  3) send oil to the right place
             *  4) recalculate the recieving end while calculating storage
             */
            float desertOilNeed = 0;
            float cityOilNeed = 0;
            // 1) check oil in desert
            float desertOilLeftovers = 0;
            float sum = State.DesertOilProd + desertOilStored;
            if (sum >= 0)
            {
                desertOilAvailability = 1;
                desertOilLeftovers = sum;
            }
            else    // Request oil from city
                desertOilNeed = sum;
            // 2) check oil in city
            float cityOilLeftovers = 0;
            sum = State.CityOil + State.PortOil + cityOilStored;
            if (sum >= 0)
            {
                cityOilAvailability = 1;
                cityOilLeftovers = sum;
            }
            else    // request oil from desert
                cityOilNeed = sum;
            // 3) send oil to the right place
            if (desertOilNeed != 0 && cityOilNeed != 0) // both need oil. Do nothing
            {
                desertOilStored = 0;
                cityOilStored = 0;
                return;
            }else if(desertOilNeed != 0)    // desert needs oil. Send some. Store the leftovers in the city if possible, else in the desert
            {
                float send = Mathf.Min(desertOilNeed, cityOilLeftovers, State.DesertOilPipes);
                if(cityOilLeftovers > send)     // We still have oil in the city. Store what we can.
                {
                    if(cityOilLeftovers - send <= State.CityOilStore) // All oil can be stored in the city. Do so.
                        cityOilStored = cityOilLeftovers - send;
                    else    // We can store the max and have still left. Check if we can send it to the desert and do so.
                    {
                        cityOilStored = State.CityOilStore;
                        if(send != State.DesertOilPipes)    // the limit on sending was not the pipes. It was the demand. Send all we can so they can store it
                            send += Mathf.Min(cityOilLeftovers - cityOilStored, State.DesertOilPipes - send);
                    }
                    // The oil arrives in the desert. Calculate the availability.
                    if (State.DesertOilProd + desertOilStored + send >= 0) // The desert has now enough oil available. use it and store the rest if possible.
                    {
                        desertOilAvailability = 1;
                        desertOilStored = Mathf.Min(State.DesertOilProd + desertOilStored + send, State.DesertOilStore);
                    }
                    else    // The desert has still not enough oil. Calculate availability. Drain the reserves.
                    {
                        float shortage = State.DesertOilProd + desertOilStored + send;
                        float available = State.TotalDesertOilProd + desertOilStored + send;
                        desertOilAvailability = available / (available - shortage);
                        desertOilStored = 0;
                    }
                }
                else    // Limit decided by the cityleftovers. We could send more and use more. All stores will be empty. The desert will (probably) not have enough.
                {       // Calculate availability and drain the reserves. If the desert does have enough, it will still not have any reserves left.
                    float shortage = State.DesertOilProd + desertOilStored + send;
                    float available = State.TotalDesertOilProd + desertOilStored + send;
                    desertOilAvailability = available / (available - shortage);
                    desertOilStored = 0;
                }
            }else if(cityOilNeed != 0)  // city needs oil. ###  DO THE SAME AS ABOVE  ###, but opposite.
            {
                float send = Mathf.Min(cityOilNeed, desertOilLeftovers, State.DesertOilPipes);
                if (desertOilLeftovers > send)     // We still have oil in the city. Store what we can.
                {
                    if (desertOilLeftovers - send <= State.DesertOilStore) // All oil can be stored in the city. Do so.
                        desertOilStored = desertOilLeftovers - send;
                    else    // We can store the max and have still left. Check if we can send it to the desert and do so.
                    {
                        desertOilStored = State.DesertOilStore;
                        if (send != State.DesertOilPipes)    // the limit on sending was not the pipes. It was the demand. Send all we can so they can store it
                            send += Mathf.Min(desertOilLeftovers - desertOilStored, State.DesertOilPipes - send);
                    }
                    // The oil arrives in the desert. Calculate the availability.
                    if (State.CityOil + State.PortOil + cityOilStored + send >= 0) // The desert has now enough oil available. use it and store the rest if possible.
                    {
                        cityOilAvailability = 1;
                        cityOilStored = Mathf.Min(State.CityOil + State.PortOil + cityOilStored + send, State.CityOilStore);
                    }
                    else    // The desert has still not enough oil. Calculate availability. Drain the reserves.
                    {
                        float shortage = State.CityOil + State.PortOil + cityOilStored + send;
                        float available = State.TotalCityOilProd + cityOilStored + send;
                        cityOilAvailability = available / (available - shortage);
                        cityOilStored = 0;
                    }
                }
                else    // Limit decided by the cityleftovers. We could send more and use more. All stores will be empty. The desert will (probably) not have enough.
                {       // Calculate availability and drain the reserves. If the desert does have enough, it will still not have any reserves left.
                    float shortage = State.CityOil + State.PortOil + cityOilStored + send;
                    float available = State.TotalCityOilProd + cityOilStored + send;
                    cityOilAvailability = available / (available - shortage);
                    cityOilStored = 0;
                }
            }       // END REPEAT
        }

        private static void CheckGasAvailability()
        {
            float netGasAtCity = State.CityGas + State.PortGas + Mathf.Min(State.SeaGasProd, State.SeaPipes);
            /*  1) check gas in city
             *  2) send leftovers to desert
             *  3) check gas in desert
             *  4) store leftovers
             *  5) remember pipe utilisation
             */
            // 1) check gas in city
            float sum = netGasAtCity + cityGasStored + Mathf.Min(State.DesertGasPipes, desertGasStored);
            if (sum >= 0)   // there is enough gas
            {
                cityGasAvailability = 1;
                if (netGasAtCity + cityGasStored < 0)    // we used gas from the desert
                    gasFlow = -(netGasAtCity + cityGasStored);
                else    // We did not use gas from the desert. send leftovers towards the desert
                {
                    gasFlow = -Mathf.Min(sum, State.DesertGasPipes);
                    cityGasStored = sum + gasFlow;
                }
            }
            else    // There was not enough gas. We already used all we could from the desert, so bad luck. 
            {
                gasFlow = Mathf.Min(State.DesertGasPipes, desertGasStored);
                float shortage = sum;
                float available = State.TotalCityGasProd + gasFlow + cityGasStored;
                cityGasAvailability = available / (available - shortage);
            }
            // 3) check gas in desert
            sum = State.TotalDesertGasCons + desertGasStored - gasFlow;
            if (sum >= 0)    // there is enough gas
            {
                desertGasAvailability = 1;
                if (sum <= State.DesertGasStore)    // we can store all the leftover gas
                {
                    desertGasStored = sum;
                }
                else    // Store all we can, then Backflow
                {
                    desertGasStored = State.DesertGasStore;
                    gasFlow = Mathf.Max(Mathf.Min(gasFlow + sum, State.DesertGasPipes), -State.DesertGasPipes);
                }
            }
            else    // There is not enough gas
            {
                float shortage = sum;
                float available = - gasFlow + desertGasStored;
                cityGasAvailability = available / (available - shortage);
            }
        }

        private static void CalculatePower()
        {
            ofshPow = State.SeaEnergy;
            desertPow = 0f;
            desertPow += State.DesertOilEnergy * desertOilAvailability;
            desertPow += State.DesertGasEnergy * desertGasAvailability;
            desertPow += State.DesertSolarEnergy;
            cityPow = 0f;
            cityPow += State.CityCoalEnergy * coalAvailability;
            cityPow += State.CityOilEnergy * cityOilAvailability;
            cityPow += State.CityGasEnergy * cityGasAvailability;
            cityPow += State.CityFreeEnergy;
            cityPow += State.SpcdPowerDrain;
        }

        private static void CheckPowerAvailability()
        {
            float netPowerAtCity = cityPow + Mathf.Min(ofshPow, State.SeaCables);
            /*  1) check power in city
             *  2) send leftovers to desert
             *  3) check gas in desert
             *  4) store leftovers
             *  5) use leftovers for gas production
             */
            float cityLeftoverPower = 0;
            float desertLeftoverPower = 0;
            // 1) check power in city
            float sum = netPowerAtCity + cityPowStored + Mathf.Min(State.DesertCables, desertPowStored) - GameMaster.powerDemand;
            if (sum >= 0)   // there is enough power
            {
                cityPowAvailability = 1;
                if (netPowerAtCity + cityPowStored < 0)    // we used power from the desert
                    desToCityPowFlow = -(netPowerAtCity + cityPowStored);
                else    // We did not use power from the desert. Send leftovers towards the desert
                {
                    desToCityPowFlow = -Mathf.Min(sum, State.DesertCables);
                    cityPowStored = Mathf.Min(sum + desToCityPowFlow, State.CityEnergyStore);
                    cityLeftoverPower = sum + desToCityPowFlow - cityPowStored;
                }
            }
            else    // There was not enough Power. We already used all we could from the desert, so bad luck. 
            {
                desToCityPowFlow = Mathf.Min(State.DesertCables, desertPowStored);
                float shortage = sum;
                float available = State.TotalCityGasProd + gasFlow + cityGasStored;
                cityGasAvailability = (sum + GameMaster.powerDemand) / GameMaster.powerDemand;
            }
            // 3) check power in desert
            sum = desertPowStored - desToCityPowFlow;
            if (sum > 0)    // there is leftover power
            {
                if (sum <= State.DesertEnergyStore)    // we can store all the leftover power
                {
                    desertPowStored = sum;
                    desertLeftoverPower = sum - desertPowStored;
                }
                else    // Store all we can, then Backflow
                {
                    desertPowStored = State.DesertEnergyStore;
                    gasFlow = Mathf.Max(Mathf.Min(gasFlow + sum, State.DesertGasPipes), -State.DesertGasPipes);
                }
            }
            else    // There is no leftover power
            {
            }

            TryConvertingPowerToGas(cityLeftoverPower, desertLeftoverPower);
        }

        static void TryConvertingPowerToGas(float cityLeftoverPower, float desertLeftoverPower)
        {
            if (cityLeftoverPower > 0 || desertLeftoverPower > 0)    // We have leftover power to transform into gas
            {
                float cityFCap = State.CityFuelCapacity;
                float desertFCap = State.DesertFuelCapacity;
                if (cityLeftoverPower > 0 && cityFCap > 0)     // We have the power and the capacity
                {
                    float amountPowConverted = Mathf.Min(cityLeftoverPower, cityFCap * 20, (State.CityGasStore - cityGasStored) * 20);
                    cityGasStored += amountPowConverted / 20;
                    cityLeftoverPower -= amountPowConverted;
                    cityFCap -= amountPowConverted / 20;
                }
                if (desertLeftoverPower > 0 && desertFCap > 0)
                {
                    float amountPowConverted = Mathf.Min(desertLeftoverPower, desertFCap * 20, (State.DesertGasStore - desertGasStored) * 20);
                    desertGasStored += amountPowConverted / 20;
                    desertLeftoverPower -= amountPowConverted;
                    desertFCap -= amountPowConverted / 20;
                }
                if (desertLeftoverPower > 0 && desertFCap > 0) { } // We couldnt use all the power at both locations. Stop trying
                else if (cityLeftoverPower > 0 && desertGasStored < State.DesertGasStore)   // We can still use some power of the city
                {
                    if ((cityFCap > 0 && gasFlow > -State.DesertGasPipes))   // We can use the gas pipes to do this
                    {
                        float amountPowConverted =
                            Mathf.Min(cityLeftoverPower, cityFCap * 20, (gasFlow + State.DesertGasPipes) * 20, (State.DesertGasStore - desertGasStored) * 20);
                        cityLeftoverPower -= amountPowConverted;
                        cityFCap -= amountPowConverted / 20;
                        gasFlow -= amountPowConverted / 20;
                        desertGasStored += amountPowConverted / 20;
                    }
                    if (desToCityPowFlow > -State.DesertCables && desertFCap > 0)    // We can use the cables to do this
                    {
                        float amountPowConverted =
                            Mathf.Min(cityLeftoverPower, desertFCap * 20, desToCityPowFlow + State.DesertCables, (State.DesertGasStore - desertGasStored) * 20);
                        cityLeftoverPower -= amountPowConverted;
                        desToCityPowFlow -= amountPowConverted;
                        desertFCap -= amountPowConverted / 20;
                        desertGasStored += amountPowConverted / 20;
                    }
                }
                else if (desertLeftoverPower > 0 && cityGasStored < State.CityGasStore)   // We can still use some power of the desert
                {
                    if ((desertFCap > 0 && gasFlow > State.DesertGasPipes))   // We can use the gas pipes to do this
                    {
                        float amountPowConverted =
                            Mathf.Min(desertLeftoverPower, desertFCap * 20, (-gasFlow + State.DesertGasPipes) * 20, (State.CityGasStore - cityGasStored) * 20);
                        desertLeftoverPower -= amountPowConverted;
                        desertFCap -= amountPowConverted / 20;
                        gasFlow += amountPowConverted / 20;
                        cityGasStored += amountPowConverted / 20;
                    }
                    if (desToCityPowFlow > State.DesertCables && cityFCap > 0)    // We can use the cables to do this
                    {
                        float amountPowConverted =
                            Mathf.Min(desertLeftoverPower, cityFCap * 20, -desToCityPowFlow + State.DesertCables, (State.CityGasStore - cityGasStored) * 20);
                        desertLeftoverPower -= amountPowConverted;
                        desToCityPowFlow += amountPowConverted;
                        cityFCap -= amountPowConverted / 20;
                        cityGasStored += amountPowConverted / 20;
                    }
                }
            }
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
                return AllCards.Sum(c => Data.Warming(c));
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
        public static float TotalCoalProduction
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null)
                        {
                            float v = Data.Coal(city[i, j]);
                            val += v > 0 ? v : 0;
                        }
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (port[i, j] != null)
                        {
                            float v = Data.Coal(port[i, j]);
                            val += v > 0 ? v : 0;
                        }
                return val;
            }
        }
        public static float TotalDesertOilProd
        {
            get
            {
                float val = 20; // base
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (desert[i, j] != null)
                        {
                            float v = Data.Oil(desert[i, j]);
                            val += v > 0 ? v : 0;
                        }
                return val;
            }
        }
        public static float TotalCityOilProd
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null)
                        {
                            float v = Data.Oil(city[i, j]);
                            val += v > 0 ? v : 0;
                        }
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (port[i, j] != null)
                        {
                            float v = Data.Oil(port[i, j]);
                            val += v > 0 ? v : 0;
                        }
                return val;
            }
        }
        public static float TotalCityGasProd
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null)
                        {
                            float v = Data.Gas(city[i, j]);
                            val += v > 0 ? v : 0;
                        }
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (port[i, j] != null)
                        {
                            float v = Data.Gas(port[i, j]);
                            val += v > 0 ? v : 0;
                        }
                return val;
            }
        }
        public static float TotalDesertGasCons
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (desert[i, j] != null)
                        {
                            float v = Data.Gas(desert[i, j]);
                            val += v < 0 ? v : 0;
                        }
                return val;
            }
        }
        public static float DesertOilEnergy
        {
            get
            {
                float var = 0; // base
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (desert[i, j] != null && desert[i, j].name == "plant_oil") var += Data.Energy(desert[i, j]);
                return var;
            }
        }
        public static float DesertGasEnergy
        {
            get
            {
                float var = 0; // base
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (desert[i, j] != null && desert[i, j].name == "plant_gas") var += Data.Energy(desert[i, j]);
                return var;
            }
        }
        public static float DesertSolarEnergy
        {
            get
            {
                float var = 0; // base
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (desert[i, j] != null && desert[i, j].name == "plant_solar") var += Data.Energy(desert[i, j]);
                return var;
            }
        }
        public static float CityCoalEnergy
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null && city[i, j].name == "plant_coal") val = Data.Energy(city[i, j]);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (port[i, j] != null && port[i, j].name == "plant_coal") val = Data.Energy(port[i, j]);
                return val;
            }
        }
        public static float CityOilEnergy
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null && city[i, j].name == "plant_oil") val = Data.Energy(city[i, j]);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (port[i, j] != null && port[i, j].name == "plant_oil") val = Data.Energy(port[i, j]);
                return val;
            }
        }
        public static float CityGasEnergy
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null && city[i, j].name == "plant_gas") val = Data.Energy(city[i, j]);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (port[i, j] != null && port[i, j].name == "plant_gas") val = Data.Energy(port[i, j]);
                return val;
            }
        }
        public static float CityFreeEnergy
        {
            get
            {
                float val = 0; // base
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (city[i, j] != null && 
                            (city[i, j].name == "plant_nuke" || city[i, j].name == "plant_wind" || city[i, j].name == "plant_solar")) val = Data.Energy(city[i, j]);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (port[i, j] != null &&
                            (port[i, j].name == "plant_nuke" || port[i, j].name == "plant_wind" || port[i, j].name == "plant_solar")) val = Data.Energy(city[i, j]);
                return val;
            }
        }

        public static float SpcdPowerDrain { get; internal set; }
        public static float CityFuelCapacity { get; internal set; }
        public static float DesertFuelCapacity { get; internal set; }
    }

    static internal class Modefiers
    {
        public static bool Fort_GW
        {
            get
            {
                return mods.Keys.Any(c=> c.name == "polit_fortify_fofu");
            }
        }
        public static bool Fort_nuke
        {
            get
            {
                return mods.Keys.Any(c => c.name == "polit_fortify_nuke");
            }
        }
        public static bool Fort_wind
        {
            get
            {
                return mods.Keys.Any(c => c.name == "polit_fortify_wind");
            }
        }
        public static bool Fort_solar
        {
            get
            {
                return mods.Keys.Any(c => c.name == "polit_fortify_solar");
            }
        }
        public static bool ExtraCard
        {
            get
            {
                return mods.Keys.Any(c => c.name == "polit_extra_card");
            }
        }

        static Dictionary<Card, int> mods = new Dictionary<Card, int>();

        static public void AddMod(Card mod)
        {
            if (mod.name == "polit_extra_card")
                GameMaster.DealCard();
            if (mod.name == "polit_reshufel")
                Debug.Log("RESHUFFEL");
            mods.Add(mod, 3);
        }

        static public void Update()
        {
            foreach(Card k in mods.Keys.ToList())
            {
                mods[k]--;
                if (mods[k] < 0) { 
                    k.DestroyMiniCard();
                    mods.Remove(k);
                    if (k.name == "polit_extra_card")
                        GameMaster.removeCard = true;
                }
            }
        }


           // { new Card("polit_reshufel"   , Board.Regions.city), 2 },
    }
}
