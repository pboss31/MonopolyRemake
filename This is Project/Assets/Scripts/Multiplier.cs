using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Multiplier : MonoBehaviour
{
    StateManager theStateManager;

    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        StartCoroutine(GetMultiplier());
    }

    IEnumerator GetMultiplier()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/monopoly/GetMultiplier.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string mltp = www.downloadHandler.text;
            string[] data = mltp.Split(' ');

            theStateManager.up1 = float.Parse(data[0], System.Globalization.CultureInfo.InvariantCulture);
            theStateManager.up2 = float.Parse(data[1]);
            theStateManager.pay1 = float.Parse(data[2]);
            theStateManager.pay2 = float.Parse(data[3]);
            theStateManager.pay3 = float.Parse(data[4]);
            theStateManager.hp1 = float.Parse(data[5]);
            theStateManager.hp2 = float.Parse(data[6]);
            theStateManager.sell = float.Parse(data[7]);
        }
    }
}
