using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

public class ConnectedTile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetTile());
        tile = GameObject.FindObjectsOfType<Tile>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        building = GameObject.FindObjectsOfType<Building>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
    }

    Tile[] tile;
    Building[] building;

    IEnumerator GetTile()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/monopoly/GetTile.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string tileInfo = www.downloadHandler.text;
            //split data to row -> 1 start 0 0 , ....
            string[] row = tileInfo.Split('\n');

            for (int i = 0; i < tile.Length; i++)
            {

                //  Tile script
                //split the row into data
                string[] data = row[i].Split(' ');
                //setting id
                tile[i].Id = int.Parse(data[0]);
                //setting name
                tile[i].Name = data[1];
                //setting base price
                tile[i].BasePrice = int.Parse(data[2]);
                //setting base hp
                tile[i].BaseHp = int.Parse(data[3]);
                //setting next tile
                tile[i].NextTile[0] = tile[(i + 1) % 40];

                //  Building script
                //setting building to false
                tile[i].transform.GetChild(1).gameObject.SetActive(false);
                //setting price
                building[i].Price = tile[i].BasePrice;
                //setting hp
                building[i].Hp = tile[i].BaseHp;
                //setting owner to -1 (NONE)
                building[i].Owner = -1;
            }
        }
    }
}
