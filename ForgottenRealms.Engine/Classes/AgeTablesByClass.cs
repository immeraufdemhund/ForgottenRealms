namespace ForgottenRealms.Engine.Classes;

public class AgeTable
{
    public AgeTable(short baseAge, byte diceCount, byte diceSize)
    {
        BaseAge = baseAge;
        DiceCount = diceCount;
        DiceSize = diceSize;
    }

    public readonly short BaseAge;
    public readonly byte DiceCount;
    public readonly byte DiceSize;
}

/// <summary>
/// This class represents all age tables based on class.
/// There should be a minimum of 7 AgeTables for the 7 base classes:
///     cleric = 0, druid = 1,fighter = 2, paladin = 3, ranger = 4, magic_user = 5, thief = 6,
/// </summary>
public class AgeTablesByClass
{
    private readonly AgeTable[] _ageTables;

    public AgeTablesByClass(AgeTable[] values)
    {
        _ageTables = values;
    }

    public AgeTable this[int i] => _ageTables[i];

    public AgeTable this[ClassId i] => _ageTables[(int)i];
}
