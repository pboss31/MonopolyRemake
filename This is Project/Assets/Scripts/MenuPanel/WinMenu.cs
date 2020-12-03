using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class WinMenu : MonoBehaviour
{
    StateManager theStateManager;
    public TextMeshProUGUI Winner;
    public Button QuitButton;
    public TextMeshProUGUI Log;
    public Button Confirm;
    public Button Cancel;
    private int win;

    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    IEnumerator GetLog()
    {
        string name = GameObject.FindObjectOfType<PlayerData>().Name;
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/monopoly/GetLog.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string winInfo = www.downloadHandler.text;
                Debug.Log(winInfo);
                if (winInfo == "Player not found")
                {
                    Log.text = "Player not found.\nInserting this player";
                }
                else
                {
                    string[] row = winInfo.Split(' ');
                    win = int.Parse(row[1]);
                    Debug.Log(win);
                    win++;
                    Log.text = "Name " + row[0] + " | Wins " + win;
                }
            }
        }
    }

    IEnumerator SendMatch()
    {
        string name = GameObject.FindObjectOfType<PlayerData>().Name;
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        if (win == 0) win = 1;
        form.AddField("wincount", win);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/monopoly/SendMatch.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("SUCCESS");
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenWinMenu()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Winner.text = GameObject.FindObjectOfType<PlayerData>().Name;
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        yield return StartCoroutine(GetLog());
        yield return StartCoroutine(SendMatch());
    }
}
