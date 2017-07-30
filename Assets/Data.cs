using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class Data
{
    static RNG rng1 = new RNG();
    static RNG rng2 = new RNG();

    static public float Energy(Card card)
    {
        if (card.name == "plant_coal") return 10;
        if (card.name == "plant_oil") return 10;
        if (card.name == "plant_gas") return 10;
        if (card.name == "plant_nuke") return 30;
        if (card.name == "plant_wind") return rng1.number * 5;
        if (card.name == "plant_solar") return rng2.number * 3 + 1;
        if (card.name == "trans_SPCD") return -1;
        if (card.name == "store_fuel") return -20;
        return 0;
    }
    static public float Coal(Card card)
    {
        if (card.name == "plant_coal") return -1;
        if (card.name == "trans_term_coal") return 4;
        if (card.name == "trans_rail") return 2;
        return 0;
    }
    static public float Oil(Card card)
    {
        if (card.name == "plant_oil") return -1;
        if (card.name == "trans_pipe_oil") return 2;
        if (card.name == "trans_term_oil") return 4;
        return 0;
    }
    static public float Gas(Card card)
    {
        if (card.name == "plant_gas") return -1;
        if (card.name == "trans_pipe_gas") return 2;
        if (card.name == "trans_term_lng") return 4;
        if (card.name == "store_fuel") return 1;
        return 0;
    }
    static public float Flow(Card card)
    {
        if (card.name == "trans_HV") return 4;
        if (card.name == "trans_HVDC") return 8;
        if (card.name == "trans_SPCD") return 12;
        return 0;
    }
    static public float StorePow(Card card)
    {
        if (card.name == "store_bat") return 4;
        return 0;
    }
    static public float StoreOil(Card card)
    {
        if (card.name == "store_oil") return 4;
        return 0;
    }
    static public float StoreGas(Card card)
    {
        if (card.name == "store_oil") return 4;
        return 0;
    }
    static public float Warming(Card card)
    {
        if (card.name == "plant_coal") return 0.03f;
        if (card.name == "plant_oil") return 0.02f;
        if (card.name == "plant_gas") return 0.02f;
        if (card.name == "trans_term_coal") return 0.005f;
        if (card.name == "trans_term_oil") return 0.005f;
        if (card.name == "trans_term_gas") return 0.005f;
        if (card.name == "store_fuel") return -0.015f;
        return 0;
    }
    static public float Resistance(Card card, Board.Regions region)
    {
        if (card.name == "plant_coal")
            if (region == Board.Regions.city) return -1 - .5f * GameMaster.globalWarming;
            else return -.5f - .3f * GameMaster.globalWarming;
        if (card.name == "plant_oil")
            if (region == Board.Regions.city) return -.8f - .4f * GameMaster.globalWarming;
            else return -.5f - .2f * GameMaster.globalWarming;
        if (card.name == "plant_gas")
            if (region == Board.Regions.city) return -.5f - .3f * GameMaster.globalWarming;
            else return -.4f - .2f * GameMaster.globalWarming;
        if (card.name == "plant_nuke")
            if (region == Board.Regions.city) return -2f - .0f * GameMaster.globalWarming;
            else return -1f - .0f * GameMaster.globalWarming;
        if (card.name == "plant_wind")
            if (region == Board.Regions.city) return -.8f + .1f * GameMaster.globalWarming;
            else return -.2f + .1f * GameMaster.globalWarming;
        if (card.name == "plant_solar")
            if (region == Board.Regions.city) return -.5f + .1f * GameMaster.globalWarming;
            else return -.1f + .1f * GameMaster.globalWarming;
        if (card.name == "trans_HV")
            return -.1f + .0f * GameMaster.globalWarming;
        if (card.name == "trans_HVDC")
            return -.1f + .0f * GameMaster.globalWarming;
        if (card.name == "trans_SPCD")
            return -.2f + .0f * GameMaster.globalWarming;
        if (card.name == "trans_pipe_oil")
            return -.2f + .1f * GameMaster.globalWarming;
        if (card.name == "trans_pipe_gas")
            return -.2f + .1f * GameMaster.globalWarming;
        if (card.name == "trans_rail")
            return -.2f + .3f * GameMaster.globalWarming;
        if (card.name == "trans_term_coal")
            return -.3f + .6f * GameMaster.globalWarming;
        if (card.name == "trans_term_oil")
            return -.2f + .3f * GameMaster.globalWarming;
        if (card.name == "trans_term_lng")
            return -.2f + .3f * GameMaster.globalWarming;
        if (card.name == "store_bat")
            if (region == Board.Regions.city) return -.2f + .1f * GameMaster.globalWarming;
            else return -.0f + .1f * GameMaster.globalWarming;
        if (card.name == "store_oil")
            if (region == Board.Regions.city) return -.2f - .1f * GameMaster.globalWarming;
            else return -.0f - .1f * GameMaster.globalWarming;
        if (card.name == "store_gas")
            if (region == Board.Regions.city) return -.2f - .1f * GameMaster.globalWarming;
            else return -.0f - .1f * GameMaster.globalWarming;
        if (card.name == "store_fuel")
            if (region == Board.Regions.city) return -.2f + .1f * GameMaster.globalWarming;
            else return -.1f + .1f * GameMaster.globalWarming;
        if (card.name == "pol_resist")
            return -.1f + .0f * GameMaster.globalWarming;
        return 0;
    }

    internal class RNG
    {
        static Random rng = new Random();
        static List<RNG> owns = new List<RNG>();
        public float number;
        public RNG() { number = (float)rng.NextDouble();
            owns.Add( this);
        }
        public static void Update() { owns.ForEach(r => r.number = (float)rng.NextDouble()); }
    }
}
