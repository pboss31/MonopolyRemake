using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAction : MonoBehaviour
{
    StateManager theStateManager;
    ChanceData chance;
    PlayerData[] thePlayerData;
    PlayerPiece[] thePlayerPiece;
    BuyMenu buyMenu;
    UpgradeMenu upgradeMenu;
    SellMenu sellMenu;

    void Start()
    {
        chance = GameObject.FindObjectOfType<ChanceData>();
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        buyMenu = GameObject.FindObjectOfType<BuyMenu>();
        upgradeMenu = GameObject.FindObjectOfType<UpgradeMenu>();
        sellMenu = GameObject.FindObjectOfType<SellMenu>();
        thePlayerData = GameObject.FindObjectsOfType<PlayerData>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        thePlayerPiece = GameObject.FindObjectsOfType<PlayerPiece>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();   
    }

    private Tile currTile;
    
    void Update()
    {
        if (theStateManager.readdyForAtk == false) return;

        Debug.Log("CAN ATTACK " + theStateManager.CanAtk);
        if (theStateManager.CanAtk == true)
        {
            CheckPlayerPosition();
        }
        else
        {
            theStateManager.IsDoneActing = true;
        }
        theStateManager.readdyForAtk = false;

    }

    public void OnCall()
    {
        currTile = thePlayerPiece[theStateManager.CurrentPlayerId].CurrTile;

        SavePlayerPosition();

        theStateManager.IsDoneAnimating = true;

        CheckTile();
    }

    public void SavePlayerPosition()
    {
        //dicetotal or spacestomove
        theStateManager.PlayerPosition[theStateManager.CurrentPlayerId] += theStateManager.DiceTotal % 40;
        Debug.Log("PlayerPosition = " + theStateManager.PlayerPosition[theStateManager.CurrentPlayerId]);
    }

    public void CheckPlayerPosition()
    {
        for (int i = 0; i < theStateManager.PlayerPosition.Length; i++)
        {
            if (theStateManager.CurrentPlayerId != i)
            {
                if (theStateManager.PlayerPosition[i] == theStateManager.PlayerPosition[theStateManager.CurrentPlayerId])
                {

                    //atk the player
                    thePlayerData[i].TakeDamage(thePlayerData[theStateManager.CurrentPlayerId].Atk);

                    //SOMEONE MIGHT DIE HERE
                    Debug.Log("SAME POSITION!\nthisPos = " + theStateManager.PlayerPosition[theStateManager.CurrentPlayerId] + " enemy" + i + "Pos = " + theStateManager.PlayerPosition[i]);
                }
            }
        }

        theStateManager.IsDoneActing = true;

    }

    public void CheckTile()
    {
        //chance tile
        if ("Chance" == currTile.tag)
        {
            theStateManager.CanAtk = false;
            Debug.Log("Player: " + theStateManager.CurrentPlayerId + " landed on " + currTile.tag + " tile");
            chance.ChanceRandom();
            theStateManager.readdyForAtk = true;  
        }
        //special tile
        else if ("Special" == currTile.tag)
        {
            theStateManager.CanAtk = false;
            Debug.Log("Player: " + theStateManager.CurrentPlayerId + " landed on " + currTile.tag + " tile");
            string typeOfSpecial = currTile.name;
            switch (typeOfSpecial)
            {
                case "Start":
                    thePlayerData[theStateManager.CurrentPlayerId].Hp += 20;
                    thePlayerData[theStateManager.CurrentPlayerId].Atk += 20;
                    thePlayerData[theStateManager.CurrentPlayerId].Coin += 20;
                    Debug.Log("Player: " + theStateManager.CurrentPlayerId + " land at start :D"); break;
                case "HP":
                    thePlayerData[theStateManager.CurrentPlayerId].Hp += 20;
                    Debug.Log("Player : " + theStateManager.CurrentPlayerId + " HP increase :D"); break;
                case "ATK":
                    thePlayerData[theStateManager.CurrentPlayerId].Atk += 20;
                    Debug.Log("Player: " + theStateManager.CurrentPlayerId + " ATK increase :D"); break;
                case "COIN":
                    thePlayerData[theStateManager.CurrentPlayerId].Coin += 20;
                    Debug.Log("Player: " + theStateManager.CurrentPlayerId + " COIN increase:D"); break;
            }
            theStateManager.readdyForAtk = true;        
        }
        //house tile
        else if ("House" == currTile.tag)
        {
            theStateManager.CanAtk = true;

            Debug.Log("Player: " + theStateManager.CurrentPlayerId + " landed on " + currTile.tag + " tile");

            //built
            if (currTile.transform.GetChild(1).gameObject.activeSelf || currTile.transform.GetChild(2).gameObject.activeSelf || currTile.transform.GetChild(3).gameObject.activeSelf)
            {
                Debug.Log("The owner of this house is Player : " + currTile.GetComponent<Building>().Owner);
                //owner = you
                if (currTile.GetComponent<Building>().Owner == theStateManager.CurrentPlayerId)
                {
                    //house already at max level
                    if (currTile.GetComponent<Building>().Level == 3)
                    {
                        theStateManager.readdyForAtk = true;                      
                    }
                    //enough money
                    if (thePlayerData[theStateManager.CurrentPlayerId].Coin >= currTile.GetComponent<Building>().UpgradePrice())
                    {
                        //open upgrade menu
                        upgradeMenu.OpenUpgradeMenu();
                    }
                    //not enough money
                    else
                    {
                        Debug.Log("NOT ENOUGH MONEY");
                        theStateManager.readdyForAtk = true;
                    }
                }
                //owner != you
                else
                {
                    Debug.Log("Player" + theStateManager.CurrentPlayerId + " lost " + currTile.GetComponent<Building>().FallPrice() + " coins to Player" + currTile.GetComponent<Building>().Owner);
                    //enough money
                    if (thePlayerData[theStateManager.CurrentPlayerId].Coin >= currTile.GetComponent<Building>().FallPrice())
                    {
                        //Pay(Amount, ToWho)
                        thePlayerData[theStateManager.CurrentPlayerId].Pay(currTile.GetComponent<Building>().FallPrice(), thePlayerData[currTile.GetComponent<Building>().Owner]);
                        //atk the house
                        currTile.GetComponent<Building>().TakeDamage(thePlayerData[theStateManager.CurrentPlayerId].Atk);
                        
                    }
                    //not enough money -> sell
                    else
                    {
                        sellMenu.OpenSellMenu();
                    }
                }
            }

            //not built
            else
            {
                //enough money
                if (thePlayerData[theStateManager.CurrentPlayerId].Coin >= currTile.GetComponent<Building>().Price)
                {
                    buyMenu.OpenBuyMenu();
                }
                // not enough money
                else
                {
                    Debug.Log("NOT ENOUGH MONEY");
                    theStateManager.readdyForAtk = true;
                }
            }
        }
    }
}
