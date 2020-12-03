using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay2 : PlayerStatDisplay
{
    protected override void Update()
    {
        if (thePlayerData[1] == null) Destroy(this.gameObject);
        playerName.color = Color.blue;
        playerName.text = thePlayerName.Player2Field.text;
        statVal.text = thePlayerData[1].Class + "\n" + thePlayerData[1].Hp + " points\n" + thePlayerData[1].Atk + " points\n" + thePlayerData[1].Coin + " coins";
    }
}
