using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Building : MonoBehaviour, ITakeDamage
{

    [SerializeField] private int _owner;
    [SerializeField] private int _price;
    [SerializeField] private int _hp;

    public int Owner { get => _owner; set => _owner = value; }
    public int Price { get => _price; set => _price = value; }
    public int Hp { get => _hp; set => _hp = value; }

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

    public void TakeDamage()
    {
        //decrease house hp
        _hp -= thePlayerData[theStateManager.CurrentPlayerId].Atk;
    }

    public void BuyHouse()
    {
        //show house, decrease player coin, set owner
        transform.GetChild(1).gameObject.SetActive(true);
        thePlayerData[theStateManager.CurrentPlayerId].Coin -= _price;
        _owner = theStateManager.CurrentPlayerId;
    }
}
