using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class Data
{
    static Random rng = new Random();

    static float Energy(Card card)
    {
        if (card.name == "plant_coal") return 10;
        if (card.name == "plant_oil") return 10;
        if (card.name == "plant_gas") return 10;
        if (card.name == "plant_nuke") return 30;
        if (card.name == "plant_wind") return (float)rng.NextDouble() * 5;
        if (card.name == "plant_wind") return (float)rng.NextDouble() * 3 + 1;
        if (card.name == "trans_SPCD") return -1;
        if (card.name == "store_fuel") return -20;
        return 0;
    }
    static float Coal(Card card)
    {
        if (card.name == "plant_coal") return -1;
        if (card.name == "trans_term_coal") return 4;
        if (card.name == "trans_rail") return 2;
        return 0;
    }
    static float Oil(Card card)
    {
        if (card.name == "plant_oil") return -1;
        if (card.name == "trans_pipe_oil") return 2;
        if (card.name == "trans_term_oil") return 4;
        return 0;
    }
    static float Gas(Card card)
    {
        if (card.name == "plant_gas") return -1;
        if (card.name == "trans_pipe_gas") return 2;
        if (card.name == "trans_term_lng") return 4;
        if (card.name == "trans_term_lng") return 1;
        return 0;
    }
    static float Flow(Card card)
    {
        if (card.name == "trans_HV") return 4;
        if (card.name == "trans_HVDC") return 8;
        if (card.name == "trans_SPCD") return 12;
        return 0;
    }
    static float StorePow(Card card)
    {
        if (card.name == "store_bat") return 4;
        return 0;
    }
    static float StoreOil(Card card)
    {
        if (card.name == "store_oil") return 4;
        return 0;
    }
    static float StoreGas(Card card)
    {
        if (card.name == "store_oil") return 4;
        return 0;
    }
    static float Resistance(Card card)
    {
        throw new NotImplementedException();
    }
}
