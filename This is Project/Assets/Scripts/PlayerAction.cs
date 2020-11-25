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

    // Start is called before the first frame update
    void Start()
    {
        chance = GameObject.FindObjectOfType<ChanceData>();
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        buyMenu = GameObject.FindObjectOfType<BuyMenu>();
        upgradeMenu = GameObject.FindObjectOfType<UpgradeMenu>();
        thePlayerData = GameObject.FindObjectsOfType<PlayerData>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        thePlayerPiece = GameObject.FindObjectsOfType<PlayerPiece>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();

        foreach (var item in thePlayerData)
        {
            Debug.Log(item.name);
        }
    }

    private Tile currTile;

    public void OnCall()
    {
        currTile = thePlayerPiece[theStateManager.CurrentPlayerId].CurrTile;

        SavePlayerPosition();
        CheckPlayerPosition();

        theStateManager.IsDoneAnimating = true;
    }

    public void SavePlayerPosition()
    {
        //dicetotal or spacestomove
        theStateManager.PlayerPosition[theStateManager.CurrentPlayerId] += theStateManager.DiceTotal % 40;
        Debug.Log("PlayerPosition = " + theStateManager.PlayerPosition[theStateManager.CurrentPlayerId]);
    }

    public void CheckPlayerPosition()
    {
        //check what tile is the player on and return canAtkTile
        bool canAtkTile = CheckTile();
        //check if 2 players on the same tile
        if (canAtkTile == false) { return; }
        for (int i = 0; i < theStateManager.PlayerPosition.Length; i++)
        {
            if (theStateManager.CurrentPlayerId != i)
            {
                if (theStateManager.PlayerPosition[i] == theStateManager.PlayerPosition[theStateManager.CurrentPlayerId])
                {
                    //atk the player
                    thePlayerData[i].TakeDamage(thePlayerData[theStateManager.CurrentPlayerId].Atk);
                    Debug.Log("SAME POSITION!\nthisPos = " + theStateManager.PlayerPosition[theStateManager.CurrentPlayerId] + " enemy" + i + "Pos = " + theStateManager.PlayerPosition[i]);
                }
            }
        }

    }

    public bool CheckTile()
    {
        //chance tile
        if ("Chance" == currTile.tag)
        {
            Debug.Log("Player: " + theStateManager.CurrentPlayerId + " landed on " + currTile.tag + " tile");
            chance.ChanceRandom();
            //MIGHT HAVE TO DELETE THIS
            theStateManager.IsDoneActing = true;
            return true;
        }
        //special tile
        else if ("Special" == currTile.tag)
        {
            Debug.Log("Player: " + theStateManager.CurrentPlayerId + " landed on " + currTile.tag + " tile");
            string typeOfSpecial = currTile.name;
            switch (typeOfSpecial)
            {
                case "Start": Debug.Log("Player: " + theStateManager.CurrentPlayerId + " land at start :)"); break;
                case "Jail": Debug.Log("Skip Player: " + theStateManager.CurrentPlayerId + " turn :("); break;
                case "GotoAnywhere": Debug.Log("Player: " + theStateManager.CurrentPlayerId + " can go anywhere :)"); break;
                case "GotoJail": Debug.Log("Player: " + theStateManager.CurrentPlayerId + " did something :)"); break;
            }
            //MIGHT HAVE TO DELETE THIS
            theStateManager.IsDoneActing = true;
            return false;
        }
        //house tile
        else if ("House" == currTile.tag)
        {
            Debug.Log("Player: " + theStateManager.CurrentPlayerId + " landed on " + currTile.tag + " tile");

            //built
            if (currTile.transform.GetChild(1).gameObject.activeSelf || currTile.transform.GetChild(2).gameObject.activeSelf || currTile.transform.GetChild(3).gameObject.activeSelf)
            {
                Debug.Log("The owner of this house is Player : " + currTile.GetComponent<Building>().Owner);
                //owner = you
                if (currTile.GetComponent<Building>().Owner == theStateManager.CurrentPlayerId)
                {
                    //house already at max level
                    if (currTile.GetComponent<Building>().Level == 3) { theStateManager.IsDoneActing = true; return true; }
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
                        theStateManager.IsDoneActing = true;
                    }
                }
                //owner != you
                else
                {
                    //Pay(Amount, ToWho)
                    thePlayerData[theStateManager.CurrentPlayerId].Pay(currTile.GetComponent<Building>().FallPrice(), thePlayerData[currTile.GetComponent<Building>().Owner]);

                    Debug.Log("Player" + theStateManager.CurrentPlayerId + " lost " + currTile.GetComponent<Building>().Price + " coins to Player" + currTile.GetComponent<Building>().Owner);

                    //atk the house
                    currTile.GetComponent<Building>().TakeDamage(thePlayerData[theStateManager.CurrentPlayerId].Atk);

                    theStateManager.IsDoneActing = true;
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
                    theStateManager.IsDoneActing = true;
                }
            }

            return true;
        }

        return true;
    }
}
