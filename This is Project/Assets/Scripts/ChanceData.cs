using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChanceData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        thePlayerData = GameObject.FindObjectsOfType<PlayerData>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
    }

    StateManager theStateManager;
    PlayerData[] thePlayerData;

    public void ChanceRandom()
    {
        // 0 = hp , 1 = atk , 2 = coin
        int typeOfChance = Random.Range(0, 6);
        int value;
        switch (typeOfChance)
        {
            case 0: value = Random.Range(1, 51); Debug.Log("HP +" + value); thePlayerData[theStateManager.CurrentPlayerId].Hp += value;  break;
            case 1: value = Random.Range(1, 51); Debug.Log("ATK +" + value); thePlayerData[theStateManager.CurrentPlayerId].Atk += value; break;
            case 2: value = Random.Range(1, 501); Debug.Log("COIN +" + value); thePlayerData[theStateManager.CurrentPlayerId].Coin += value; break;
            case 3: value = Random.Range(-50, 0); Debug.Log("HP " + value); thePlayerData[theStateManager.CurrentPlayerId].Hp += value; break;
            case 4: value = Random.Range(-50, 0); Debug.Log("ATK " + value); thePlayerData[theStateManager.CurrentPlayerId].Atk += value; break;
            case 5: value = Random.Range(-501, 0); Debug.Log("COIN " + value); thePlayerData[theStateManager.CurrentPlayerId].Coin += value; break;
        }
    }

}
