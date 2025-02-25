using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;


public static class JsonHelper
{
    public static WeatherDataEntry[] FromJson<WeatherDataEntry>(string json)
    {
        Debug.Log(json);
        Wrapper<WeatherDataEntry> wrapper = JsonUtility.FromJson<Wrapper<WeatherDataEntry>>(json);
        return wrapper.Items;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}


[System.Serializable]
public class WeatherDataEntry {
    public float min_temperature;
    public float max_temperature;
    public float total_precipitation;
    public float wind_speed;
}



public class DataImport : MonoBehaviour
{

    public WeatherDataEntry[] info;

    IEnumerator ContactAPI()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://hacked-zg4z.onrender.com/?weather=2021"))
        {
            yield return request.SendWebRequest();
            string rawdata = request.downloadHandler.text;
            Debug.Log(rawdata);
            info = JsonHelper.FromJson<WeatherDataEntry>("{ \"Items\":" + rawdata + "}");
            Debug.Log(info[0].max_temperature);

        } 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ContactAPI());
    }
  
    // Update is called once per frame
    void Update()
    {
        
    }
}
