using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BuyMenu : MonoBehaviour
{
    StateManager theStateManager;
    PlayerPiece[] thePlayerPiece;
    PlayerData[] thePlayerData;

    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        thePlayerPiece = GameObject.FindObjectsOfType<PlayerPiece>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        thePlayerData = GameObject.FindObjectsOfType<PlayerData>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();     
    }

    private Tile currTile;
    public GameObject BuyButton;
    public GameObject CancelButton;
    public Text TilePriceDisplay;

    public int ChoiceMade = 0;

    public void Buy()
    {
        //set owner, decrease player's coin
        currTile.GetComponent<Building>().BuyHouse();
        ChoiceMade = 1;
    }

    public void Cancel()
    {
        ChoiceMade = 2;
    }

    void Update()
    {
        if(theStateManager.IsBuying == false) { return; }
        if (ChoiceMade >= 1)
        {
            //close the menu
            transform.GetChild(0).gameObject.SetActive(false);
            theStateManager.IsBuying = false;
            theStateManager.IsDoneActing = true;
            ChoiceMade = 0;
        }
    }

    public void OpenBuyMenu()
    {
        currTile = thePlayerPiece[theStateManager.CurrentPlayerId].CurrTile;
        //open the menu and display price
        theStateManager.IsBuying = true;
        transform.GetChild(0).gameObject.SetActive(true);
        TilePriceDisplay.text = "Price : " + currTile.GetComponent<Building>().Price;
    }
}
