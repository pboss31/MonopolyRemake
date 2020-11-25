using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PlayerStatDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        thePlayerData = GameObject.FindObjectsOfType<PlayerData>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        thePlayerName = GameObject.FindObjectOfType<PlayerNameInput>();
        myText = GetComponentsInChildren<Text>();
    }

    PlayerNameInput thePlayerName;
    PlayerData[] thePlayerData;
    Text[] myText;

    // Update is called once per frame
    void Update()
    {
        if (myText[0].name == "Player 1 Text")
        {
            ShowStat(0, Color.red, thePlayerName.Player1Field.text);
        }
        if (myText[1].name == "Player 2 Text")
        {
            ShowStat(1, Color.blue, thePlayerName.Player2Field.text);
        }
        if (myText[2].name == "Player 3 Text")
        {
            ShowStat(2, Color.green, thePlayerName.Player3Field.text);
        }
        if (myText[3].name == "Player 4 Text")
        {
            ShowStat(3, Color.yellow, thePlayerName.Player4Field.text);
        }

    }

    public void ShowStat(int i, Color color, string playerName)
    {
        myText[i].text = "Player " + (i + 1) + " : " + playerName + "\nClass : " + thePlayerData[i].Class + "\nHP : " + thePlayerData[i].Hp + "\nATK : " + thePlayerData[i].Atk + "\nCOIN : " + thePlayerData[i].Coin;
        myText[i].color = color;
    }
}
