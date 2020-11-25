using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SendToDB : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Send("variable"));
    }

    IEnumerator Send(string date)
    {
        WWWForm form = new WWWForm();
        form.AddField("date", date);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/monopoly/SendMatch.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("www.downloadHandler.text");
            }
        }
    }
}