using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Building : MonoBehaviour, ITakeDamage, IClickable
{
    StateManager theStateManager;
    PlayerData[] thePlayerData;
    HouseInfoMenu houseMenu;

    [SerializeField] private int owner;
    [SerializeField] private int price;
    [SerializeField] private int hp;
    [SerializeField] private int level;
    [SerializeField] private int houseNo;

    public int Owner { get => owner; set => owner = value; }
    public int Price { get => price; set => price = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Level { get => level; set => level = value; }
    public int HouseNo { get => houseNo; set => houseNo = value; }

    public void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        thePlayerData = GameObject.FindObjectsOfType<PlayerData>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        houseMenu = GameObject.FindObjectOfType<HouseInfoMenu>();
    }

    public void Update()
    {
        if (owner == 0)
        {
            transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            transform.GetChild(2).GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            transform.GetChild(3).GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else if (owner == 1)
        {
            transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            transform.GetChild(2).GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            transform.GetChild(3).GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
        else if (owner == 2)
        {
            transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            transform.GetChild(2).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            transform.GetChild(3).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        }
        else
        {
            transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
            transform.GetChild(2).GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
            transform.GetChild(3).GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        }
    }

    public void OnClick()
    {
        houseMenu.OpenHouseInfoMenu(this);
    }

    public void TakeDamage(int dmg)
    {
        //decrease house hp
        hp -= dmg;
        //destroy house if hp <= 0
        if (hp <= 0)
        {
            SetDefault();
        }
        theStateManager.readdyForAtk = true;
    }

    public void BuyHouse()
    {
        //add this gameobject into PlayerHouse
        thePlayerData[theStateManager.CurrentPlayerId].PlayerHouse.Add(this.gameObject);
        //show house, decrease player coin, set owner
        transform.GetChild(1).gameObject.SetActive(true);
        thePlayerData[theStateManager.CurrentPlayerId].Coin -= price;
        owner = theStateManager.CurrentPlayerId;
    }

    public void Upgrade()
    {
        switch (level)
        {
            case 1:
                upgradeCalc(theStateManager.up1, theStateManager.hp1, level);
                break;
            case 2:
                upgradeCalc(theStateManager.up2, theStateManager.hp2, level);
                break;
        }
    }

    public void SellHouse()
    {
        SetDefault();
    }
    

    private void SetDefault()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);

        //remove this gameobject from PlayerHouse
        thePlayerData[owner].PlayerHouse.Remove(gameObject);
        
        owner = -1;
        level = 1;
        hp = GetComponent<Tile>().BaseHp;
        price = GetComponent<Tile>().BasePrice;
    }

    private void upgradeCalc(float up, float hp, int buildingLvl)
    {
        //setactive to next level & deactive old one
        transform.GetChild(level).gameObject.SetActive(false);
        transform.GetChild(level + 1).gameObject.SetActive(true);

        //price for upgrade
        price = Mathf.CeilToInt(GetComponent<Tile>().BasePrice * up); 
        //decrease player's coin
        thePlayerData[theStateManager.CurrentPlayerId].Coin -= price; 

        //hp = hp * hpMult
        this.hp = Mathf.CeilToInt(this.hp * hp); 

        level++;

    }

    public int UpgradePrice()
    {
        switch (level)
        {
            case 1:
                return Mathf.CeilToInt(GetComponent<Tile>().BasePrice * theStateManager.up1);
            case 2:
                return Mathf.CeilToInt(GetComponent<Tile>().BasePrice * theStateManager.up2);
            default:
                return 0;
        }
    }

    public int FallPrice()
    {
        switch (level)
        {
            case 1: 
                return Mathf.CeilToInt(price * theStateManager.pay1);
            case 2:
                return Mathf.CeilToInt(price * theStateManager.pay2);
            case 3:
                return Mathf.CeilToInt(price * theStateManager.pay3);
            default:
                return 0;
        }   
    }

    public int SellPrice()
    {
        int basePrice = GetComponent<Tile>().BasePrice;
        float mult = theStateManager.sell;
        switch (level)
        {
            case 1:
                return Mathf.CeilToInt(basePrice * mult);
                // (Base) * sell
            case 2:
                return Mathf.CeilToInt((basePrice + (basePrice * theStateManager.up1)) * mult );
                // [(Base) + (Base * up1)] * sell
            case 3:
                return Mathf.CeilToInt((basePrice + (basePrice * theStateManager.up1) + (basePrice * theStateManager.up2)) * mult);
                //[(Base) + (Base*up1) + (Base*up2)] * sell
            default:
                return 0;
        }
    }
}
