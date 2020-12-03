using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay4 : PlayerStatDisplay
{
    protected override void Update()
    {
        if (thePlayerData[3] == null) Destroy(this.gameObject);
        playerName.color = Color.yellow;
        playerName.text = thePlayerName.Player4Field.text;
        statVal.text = thePlayerData[3].Class + "\n" + thePlayerData[3].Hp + " points\n" + thePlayerData[3].Atk + " points\n" + thePlayerData[3].Coin + " coins";
    }
}
