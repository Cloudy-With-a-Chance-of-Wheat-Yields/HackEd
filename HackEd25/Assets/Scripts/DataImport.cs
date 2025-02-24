using UnityEngine;
using UnityEngine.Networking;
using System;

[System.Serializable]
public class WeatherDataEntry {
    public float min_temperature;
    public float max_temperature;
    public float total_precipitation;
    public float wind_speed;
}

public class DataImport : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
    {
        string raw_data = fetch_api("2021");

    }
    public string fetch_api(string year) {
        UnityWebRequest webRequest = UnityWebRequest.Get("https://hacked-zg4z.onrender.com/" + year);
        if (!webRequest.isNetworkError)
        {
            return webRequest.downloadHandler.text;
        }
        else
        {
            Debug.Log("Couldn't reach API - Something has gone horribly, horribly wrong");
            return "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
