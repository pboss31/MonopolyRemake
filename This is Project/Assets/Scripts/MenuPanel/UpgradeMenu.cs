using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
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
    public GameObject UpgradeButton;
    public GameObject CancelButton;
    public Text UpgradePriceDisplay;

    public int ChoiceMade = 0;

    public void Upgrade()
    {
        //set owner, decrease player's coin
        currTile.GetComponent<Building>().Upgrade();
        ChoiceMade = 1;
    }

    public void Cancel()
    {
        ChoiceMade = 2;
    }

    void Update()
    {
        if (theStateManager.IsUpgrading == false) { return; }
        if (ChoiceMade >= 1)
        {
            //close the menu
            transform.GetChild(0).gameObject.SetActive(false);
            theStateManager.IsUpgrading = false;
            theStateManager.IsDoneActing = true;
            ChoiceMade = 0;
        }
    }

    public void OpenUpgradeMenu()
    {
        currTile = thePlayerPiece[theStateManager.CurrentPlayerId].CurrTile;
        //open the menu and display upgrade price
        theStateManager.IsUpgrading = true;
        transform.GetChild(0).gameObject.SetActive(true);
        UpgradePriceDisplay.text = "Price : " + currTile.GetComponent<Building>().UpgradePrice();
    }
}
