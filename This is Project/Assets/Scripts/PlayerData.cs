using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int _hp;
    [SerializeField] private int _atk;
    [SerializeField] private int _coin;
    [SerializeField] private string _class;

    private void Start()
    {
        if (gameObject.name == "Player1-Piece") {
            this._hp = Warrior.Hp;
            this._atk = Warrior.Atk;
            this._coin = Warrior.Coin;
            this._class = "Warrior";
        }

        if (gameObject.name == "Player2-Piece")
        {  
            this._hp = Tank.Hp;
            this._atk = Tank.Atk;
            this._coin = Tank.Coin;
            this._class = "Tank";
        }

        if (gameObject.name == "Player3-Piece")
        {
            this._hp = Berserker.Hp;
            this._atk = Berserker.Atk;
            this._coin = Berserker.Coin;
            this._class = "Berserker";
        }

        if (gameObject.name == "Player4-Piece")
        {
            this._hp = Merchant.Hp;
            this._atk = Merchant.Atk;
            this._coin = Merchant.Coin;
            this._class = "Merchant";
        }
    }

    public int Hp { get => _hp; set => _hp = value; }
    public int Atk { get => _atk; set => _atk = value; }
    public int Coin { get => _coin; set => _coin = value; }
    public string Class { get => _class; set => _class = value; }

    public void TakeDamage(int dmg)
    {
        _hp -= dmg;
    }

    public void Pay(int payAmount, PlayerData player)
    {
        //player coin decrease 
        _coin -= payAmount;
        //player coin increase
        player.Coin += payAmount;
    }
}
