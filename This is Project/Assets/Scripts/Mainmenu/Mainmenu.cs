using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Mainmenu : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button leaderboardBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Button searchBtn;
    public Button deleteButton;
    [SerializeField] private Button backButton;
    public TMP_InputField playerName;
    public TextMeshProUGUI playerLog;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenLeaderboardMenu()
    {
        transform.GetChild(4).gameObject.SetActive(true);
    }

    public void CloseLeaderBoardMenu()
    {
        transform.GetChild(4).gameObject.SetActive(false);
    }

    public void SearchPlayer()
    {
        StartCoroutine(GetLog());
    }

    public void DeletePlayer()
    {
        StartCoroutine(DeleteData());
    }

    IEnumerator GetLog()
    {
        string name = playerName.text;
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

                if (winInfo == "Player not found")
                {
                    deleteButton.interactable = false;
                    playerLog.text = "Player not found";
                }
                else
                {
                    deleteButton.interactable = true;
                    string[] row = winInfo.Split(' ');
                    playerLog.text = "Name : " + row[0] + "\nTotal wins : " + row[1];     
                }
            }
        }
    }

    IEnumerator DeleteData()
    {
        string name = playerName.text;
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/monopoly/DeleteData.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
