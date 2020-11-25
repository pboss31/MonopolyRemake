using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior
{
    private static int _hp = 500;
    private static int _atk = 50;
    private static int _coin = 10000;

    public static int Hp { get => _hp; set => _hp = value; }
    public static int Atk { get => _atk; set => _atk = value; }
    public static int Coin { get => _coin; set => _coin = value; }

}

public class Tank 
{
    private static int _hp = 1000;
    private static int _atk = 25;
    private static int _coin = 10000;

    public static int Hp { get => _hp; set => _hp = value; }
    public static int Atk { get => _atk; set => _atk = value; }
    public static int Coin { get => _coin; set => _coin = value; }
}

public class Berserker 
{
    private static int _hp = 250;
    private static int _atk = 100;
    private static int _coin = 10000;

    public static int Hp { get => _hp; set => _hp = value; }
    public static int Atk { get => _atk; set => _atk = value; }
    public static int Coin { get => _coin; set => _coin = value; }
}

public class Merchant 
{
    private static int _hp = 250;
    private static int _atk = 25;
    private static int _coin = 100000;

    public static int Hp { get => _hp; set => _hp = value; }
    public static int Atk { get => _atk; set => _atk = value; }
    public static int Coin { get => _coin; set => _coin = value; }
}