using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class SellMenu : MonoBehaviour
{

    StateManager theStateManager;
    PlayerPiece[] thePlayerPiece;
    PlayerData[] thePlayerData;
    Building[] building;

    public TextMeshProUGUI need;
    public TextMeshProUGUI earn;
    public Button giveUp;
    public Button sell;
    public AudioSource sound;
    private Tile currTile;

    private int choiceMade = 0;
    private List<GameObject> _sellHolder;
    private int needPrice;
    private int earnPrice;

    public List<GameObject> SellHolder { get => _sellHolder; set => _sellHolder = value; }

    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        thePlayerPiece = GameObject.FindObjectsOfType<PlayerPiece>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        thePlayerData = GameObject.FindObjectsOfType<PlayerData>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        building = GameObject.FindObjectsOfType<Building>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        sound = GetComponent<AudioSource>();

        _sellHolder = new List<GameObject>();
    }

    public void Sell()
    {
        sound.Play();

        //unhighlight the house that can be sell
        foreach (GameObject tile in thePlayerData[theStateManager.CurrentPlayerId].PlayerHouse)
        {
            tile.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
        }

        //delete house that has been selected
        foreach (GameObject house in _sellHolder)
        {
            house.GetComponent<Building>().SellHouse();
        }

        //pay to owner
        thePlayerData[theStateManager.CurrentPlayerId].Pay(currTile.GetComponent<Building>().FallPrice(), thePlayerData[currTile.GetComponent<Building>().Owner]);
        //leftover to me
        thePlayerData[theStateManager.CurrentPlayerId].Coin += currTile.GetComponent<Building>().FallPrice() + (needPrice * -1);
        choiceMade = 1;
    }

    public void GiveUp()
    {
        //unhighlight the house that can be sell
        foreach (GameObject tile in thePlayerData[theStateManager.CurrentPlayerId].PlayerHouse)
        {
            tile.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
        }

        _sellHolder.Clear();
        _sellHolder.AddRange(thePlayerData[theStateManager.CurrentPlayerId].PlayerHouse);

        //delete all of this currplayer house
        foreach (GameObject house in _sellHolder)
        {
            house.GetComponent<Building>().SellHouse();
        }

        //pay to owner
        thePlayerData[theStateManager.CurrentPlayerId].Pay(currTile.GetComponent<Building>().FallPrice(), thePlayerData[currTile.GetComponent<Building>().Owner]);

        //SOMEONE MUST DIE HERE
        thePlayerData[theStateManager.CurrentPlayerId].Hp = 0;

        choiceMade = 2;
    }

    void Update()
    {
        need.text = needPrice + " coins";
        if (theStateManager.IsSelling == false) return;
        //interactable
        if (needPrice <= 0)
            sell.interactable = true;
        
        else
            sell.interactable = false;
        
        if (choiceMade >= 1)
        {
            _sellHolder.Clear();
            transform.GetChild(0).gameObject.SetActive(false);
            //theStateManager.IsDoneActing = true;
            theStateManager.readdyForAtk = true;
            theStateManager.IsSelling = false;
            choiceMade = 0;
        }

    }

    public void OpenSellMenu()
    {
        currTile = thePlayerPiece[theStateManager.CurrentPlayerId].CurrTile;
        //open the menu -> choose house -> sell/give up
        theStateManager.IsSelling = true;
        transform.GetChild(0).gameObject.SetActive(true);
        needPrice = currTile.GetComponent<Building>().FallPrice();


        //highlight the house that can be sell
        foreach (GameObject tile in thePlayerData[theStateManager.CurrentPlayerId].PlayerHouse)
        {
            tile.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    public void AddHouse(GameObject house)
    {
        _sellHolder.Add(house);
        needPrice -= house.GetComponent<Building>().SellPrice();
    }

    public void RemoveHouse(GameObject house)
    {
        _sellHolder.Remove(house);
        needPrice += house.GetComponent<Building>().SellPrice();
    }
}
