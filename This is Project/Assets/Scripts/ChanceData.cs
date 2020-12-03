using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

public class ChanceData : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetChance());
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        thePlayerData = GameObject.FindObjectsOfType<PlayerData>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
    }

    StateManager theStateManager;
    PlayerData[] thePlayerData;
    List<int> hpList = new List<int>();
    List<int> atkList = new List<int>();
    List<int> coinList = new List<int>();

    IEnumerator GetChance()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/monopoly/GetCard.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string chanceInfo = www.downloadHandler.text;
            //split data to row -> ATK -50, ....
            string[] row = chanceInfo.Split('\n');

            foreach (string i in row)
            {
                string[] data = i.Split(' ');
                if(data[0] == "ATK")
                {
                    atkList.Add(int.Parse(data[1]));
                }
                else if (data[0] == "Hp")
                {
                    hpList.Add(int.Parse(data[1]));
                }
                else if (data[0] == "Coin")
                {
                    coinList.Add(int.Parse(data[1]));
                }
            }
        }
    }

    public void ChanceRandom()
    {
        // 0 = hp , 1 = atk , 2 = coin
        int typeOfChance = Random.Range(0, 3);
        int index;

        switch (typeOfChance)
        {
            case 0:
                index = Random.Range(0, atkList.Count); Debug.Log("ATK +" + atkList[index]); thePlayerData[theStateManager.CurrentPlayerId].Atk += atkList[index];  break;
            case 1:
                index = Random.Range(0, hpList.Count); Debug.Log("HP +" + hpList[index]); thePlayerData[theStateManager.CurrentPlayerId].Hp += hpList[index]; break;
            case 2:
                index = Random.Range(0, coinList.Count); Debug.Log("COIN +" + coinList[index]); thePlayerData[theStateManager.CurrentPlayerId].Coin += coinList[index]; break;
        }
    }

}
