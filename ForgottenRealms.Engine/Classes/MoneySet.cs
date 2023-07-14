namespace ForgottenRealms.Engine.Classes;

public class Money
{
    public const int Copper = 0;
    public const int Silver = 1;
    public const int Electrum = 2;
    public const int Gold = 3;
    public const int Platinum = 4;
    public const int Gems = 5;
    public const int Jewelry = 6;

    public static string[] names = { "Copper", "Silver", "Electrum", "Gold", "Platinum", "Gems", "Jewelry" };

    public static int[] per_copper = { 1, 10, 100, 200, 1000 };
}

public class MoneySet : IDataIO
{
    private int[] money = new int[7];

    // overload operator +
    public static MoneySet operator +(MoneySet a, MoneySet b)
    {
        var c = new MoneySet();

        for (var coin = Money.Copper; coin <= Money.Jewelry; coin++)
        {
            c.money[coin] = a.money[coin] + b.money[coin];
        }

        return c;
    }


    public void ClearAll()
    {
        for (var coin = Money.Copper; coin <= Money.Jewelry; coin++)
        {
            money[coin] = 0;
        }
    }


    public void ClearCoins()
    {
        for (var coin = Money.Copper; coin <= Money.Platinum; coin++)
        {
            money[coin] = 0;
        }
    }


    public int GetExpWorth()
    {
        var total = GetGoldWorth();

        total += money[Money.Gems] * 250;
        total += money[Money.Jewelry] * 2200;

        return total;
    }


    public int GetGoldWorth()
    {
        var copperValue = 0;
        for (var coin = Money.Copper; coin <= Money.Platinum; coin++)
        {
            copperValue += money[coin] * Money.per_copper[coin];
        }

        return copperValue / Money.per_copper[Money.Gold];
    }

    public void AddCoins(int coinType, int count) => money[coinType] += count;


    public void SetCoins(int coinType, int count) => money[coinType] = count;


    public void SubtractGoldWorth(int gold)
    {
        var coppers = gold * Money.per_copper[Money.Gold];

        var coin = Money.Copper;

        while (coppers > 0)
        {
            var sub_coins = coppers / Money.per_copper[coin] + 1;

            if (money[coin] < sub_coins)
            {
                sub_coins = money[coin];
            }

            coppers -= Money.per_copper[coin] * sub_coins;
            money[coin] -= sub_coins;

            coin++;
        }

        if (coppers < 0)
        {
            coppers = Math.Abs(coppers);
            coin = Money.Platinum;

            while (coppers > 0)
            {
                var add_coins = coppers / Money.per_copper[coin];
                coppers -= Money.per_copper[coin] * add_coins;

                money[coin] += add_coins;
                coin--;
            }
        }
    }


    public int GetCoins(int coinType) => money[coinType];


    public bool AnyMoney()
    {
        for (var coin = Money.Copper; coin <= Money.Jewelry; coin++)
        {
            if (money[coin] > 0)
            {
                return true;
            }
        }

        return false;
    }

    public bool ScaleAll(double scale)
    {
        var didScale = false;
        for (var coin = Money.Copper; coin <= Money.Platinum; coin++)
        {
            didScale = didScale || money[coin] > 0;
            money[coin] = (int)(money[coin] * scale);
        }

        return didScale;
    }

    public int Copper => money[Money.Copper];

    //set { money[Money.copper] = value; }
    public int Electrum => money[Money.Electrum];

    //set { money[Money.electrum] = value; }
    public int Silver => money[Money.Silver];

    //set { money[Money.silver] = value; }
    public int Gold => money[Money.Gold];

    //set { money[Money.gold] = value; }
    public int Platinum => money[Money.Platinum];

    //set { money[Money.platum] = value; }
    public int Gems => money[Money.Gems];

    //set { money[5] = value; }
    public int Jewels => money[Money.Jewelry];
    //set { money[6] = value; }

    void IDataIO.Write(byte[] data, int offset)
    {
        Sys.ShortToArray((short)money[0], data, offset + 0);
        Sys.ShortToArray((short)money[1], data, offset + 2);
        Sys.ShortToArray((short)money[2], data, offset + 4);
        Sys.ShortToArray((short)money[3], data, offset + 6);
        Sys.ShortToArray((short)money[4], data, offset + 8);
        Sys.ShortToArray((short)money[5], data, offset + 10);
        Sys.ShortToArray((short)money[6], data, offset + 12);
    }

    void IDataIO.Read(byte[] data, int offset)
    {
        money[0] = Sys.ArrayToShort(data, offset + 0);
        money[1] = Sys.ArrayToShort(data, offset + 2);
        money[2] = Sys.ArrayToShort(data, offset + 4);
        money[3] = Sys.ArrayToShort(data, offset + 6);
        money[4] = Sys.ArrayToShort(data, offset + 8);
        money[5] = Sys.ArrayToShort(data, offset + 10);
        money[6] = Sys.ArrayToShort(data, offset + 12);
    }
}
