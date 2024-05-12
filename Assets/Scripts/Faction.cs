public enum Faction { Player, NPC, Enviroment, Enemy, Cat}

public static class FactionHelpers
{
    public static bool IsHostile(this Faction f1, Faction f2)
    {
        switch (f1)
        {
            case Faction.Player:
                return (f2 == Faction.Enemy);
            case Faction.NPC:
                return (f2 != Faction.NPC);
            case Faction.Enviroment:
                return (f2 != Faction.Enviroment);
            case Faction.Enemy:
                return (f2 == Faction.Player);
            case Faction.Cat:
                return (f2 == Faction.Enemy);
            default:
                break;
        }
        return false;
    }
    
}