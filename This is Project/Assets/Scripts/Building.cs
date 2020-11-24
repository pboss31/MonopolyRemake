using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Building : MonoBehaviour, ITakeDamage
{

    [SerializeField] private int _owner;
    [SerializeField] private int _price;
    [SerializeField] private int _hp;
    [SerializeField] private int _level;

    public int Owner { get => _owner; set => _owner = value; }
    public int Price { get => _price; set => _price = value; }
    public int Hp { get => _hp; set => _hp = value; }
    public int Level { get => _level; set => _level = value; }

    StateManager theStateManager;
    PlayerData[] thePlayerData;

    public void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        thePlayerData = GameObject.FindObjectsOfType<PlayerData>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
    }

    public void Update()
    {
        if (_owner == 0)
        {
            transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else if (_owner == 1)
        {
            transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
        else if (_owner == 2)
        {
            transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        }
        else
        {
            transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        }
    }

    public void TakeDamage(int dmg)
    {
        //decrease house hp
        _hp -= dmg;
        //destroy house if hp <= 0
        if (_hp <= 0)
        {
            switch (_level)
            {
                case 1:
                    transform.GetChild(1).gameObject.SetActive(false);
                    break;
                case 2:
                    transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case 3:
                    transform.GetChild(3).gameObject.SetActive(false);
                    break;
            }
            
            SetDefault();
        }
    }

    public void BuyHouse()
    {
        //show house, decrease player coin, set owner
        transform.GetChild(1).gameObject.SetActive(true);
        thePlayerData[theStateManager.CurrentPlayerId].Coin -= _price;
        _owner = theStateManager.CurrentPlayerId;
    }

    public void Upgrade()
    {
        switch (_level)
        {
            case 1:
                upgradeCalc(theStateManager.up1, theStateManager.hp1, _level);
                break;
            case 2:
                upgradeCalc(theStateManager.up2, theStateManager.hp2, _level);
                break;
        }
    }
    public void SellHouse() { }

    private void SetDefault()
    {
        _owner = -1;
        _level = 1;
        _hp = GetComponent<Tile>().BaseHp;
        _price = GetComponent<Tile>().BasePrice;
    }

    private void upgradeCalc(float up, float hp, int buildingLvl)
    {
        //setactive to next level
        transform.GetChild(_level).gameObject.SetActive(false);
        transform.GetChild(_level + 1).gameObject.SetActive(true);

        //price for upgrade
        _price = Mathf.CeilToInt(GetComponent<Tile>().BasePrice * up); 
        //decrease player's coin
        thePlayerData[theStateManager.CurrentPlayerId].Coin -= _price; 

        //hp = Base_hp * 1.2
        _hp = Mathf.CeilToInt(GetComponent<Tile>().BaseHp * hp); 

        _level++;

    }

    public int UpgradePrice()
    {
        switch (_level)
        {
            case 1:
                return  Mathf.CeilToInt(GetComponent<Tile>().BasePrice * theStateManager.up1);
            case 2:
                return Mathf.CeilToInt(GetComponent<Tile>().BasePrice * theStateManager.up2);
        }

        return 0;
    }
}
