using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay3 : PlayerStatDisplay
{
    protected override void Update()
    {
        if (thePlayerData[2] == null) Destroy(this.gameObject);
        playerName.color = Color.green;
        playerName.text = thePlayerName.Player3Field.text;
        statVal.text = thePlayerData[2].Class + "\n" + thePlayerData[2].Hp + " points\n" + thePlayerData[2].Atk + " points\n" + thePlayerData[2].Coin + " coins";
    }
}
