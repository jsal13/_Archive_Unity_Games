using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Relics
// List + Getter/Setter of Statistic objects.
{
    public List<Relic> relics;

    public Relics(List<Relic> relics)
    {
        this.relics = relics;
    }

    public Relic GetRelic(RelicType relicType) => this.relics.FirstOrDefault(x => x.relicType == relicType);
}

[Serializable]
public class Relic
// An individual Player Relic (eg, an item with an effect.)
// NOTE: CURRENTLY, EVERY EFFECT IS DONE AT THE BEGINNING OF A TURN.
{
    public RelicType relicType;
    public string relicDescription = "";

    public Relic(RelicType relicType)
    {
        this.relicType = relicType;
    }
}

public enum RelicType
{
    StrengthBonus,
    DexterityBonus,
    MagicBonus,
    StressBonus,
    GoldBonus
}