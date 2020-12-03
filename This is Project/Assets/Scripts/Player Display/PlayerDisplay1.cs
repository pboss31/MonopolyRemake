using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay1 : PlayerStatDisplay
{
    protected override void Update()
    {
        if (thePlayerData[0] == null) Destroy(this.gameObject);
        playerName.color = Color.red;
        playerName.text = thePlayerName.Player1Field.text;
        statVal.text = thePlayerData[0].Class + "\n" + thePlayerData[0].Hp + " points\n" + thePlayerData[0].Atk + " points\n" + thePlayerData[0].Coin + " coins";
    }

}
