using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Linq;

public class PlayerNameInput : MonoBehaviour
{
    PlayerData[] thePlayerData;

    public InputField Player1Field;
    public InputField Player2Field;
    public InputField Player3Field;
    public InputField Player4Field;
    public TextMeshProUGUI mapText;

    public Button StartButton;

    void Start()
    {
        thePlayerData = GameObject.FindObjectsOfType<PlayerData>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        StartCoroutine(GetMap());
    }

    public void callStart()
    {
        thePlayerData[0].Name = Player1Field.text;
        thePlayerData[1].Name = Player2Field.text;
        thePlayerData[2].Name = Player3Field.text;
        thePlayerData[3].Name = Player4Field.text;
        transform.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Player1Field.text.Length > 0 && Player2Field.text.Length > 0 && Player3Field.text.Length > 0 && Player4Field.text.Length > 0)
        {
            StartButton.interactable = true;
        }
    }

    IEnumerator GetMap()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/monopoly/GetMap.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string mapName = www.downloadHandler.text;
            //split data to row -> Westeros, ....
            mapText.text = "MAP\n" + mapName;
        }
    }
}

