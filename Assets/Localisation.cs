using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

internal static class Localisation
{
    static Dictionary<string, string> data = new Dictionary<string, string> {
        { "plant_coal"      , "Coal power plant" },
        { "plant_oil"       , "Oil power plant" },
        { "plant_gas"       , "Natural gas power plant" },
        { "plant_nuke"      , "Oil powered power plant" },
        { "plant_wind"      , "Wind farm" },
        { "plant_solar"     , "Solar farm" },
        { "trans_HV"        , "HV powerline" },
        { "trans_HVDC"      , "HVDC powerline" },
        { "trans_SPCD"      , "Superconducting powerline" },
        { "trans_pipe_oil"  , "Oil pipeline" },
        { "trans_pipe_gas"  , "Gas pipeline" },
        { "trans_rail"      , "Coal railway" },
        { "trans_term_coal" , "Coal harbor terminal" },
        { "trans_term_oil"  , "Oil harbor terminal" },
        { "trans_term_lng"  , "LNG harbor terminal" },
        { "store_bat"       , "Battery storage" },
        { "store_oil"       , "Oil storage" },
        { "store_gas"       , "Gas storage" },
        { "store_fuel"      , "Carbon to gas fuel cell" },
        { "polit_resist"    , "Media campaign" },
        { "polit_fortify_fofu", "Chinese hoax" },
        { "polit_fortify_nuke", "Mega Energy!" },
        { "polit_fortify_wind", "In my backyard" },
        { "polit_fortify_solar", "Solar roofs" },
        { "polit_extra_card", "Extra card" },
        { "polit_reshufel", "Reshuffel" },
    };

    internal static string GetText(string v)
    {
        if (data.ContainsKey(v))
            return data[v];
        Debug.Log("Failed loading string: " + v);
        return v;
    }
}
