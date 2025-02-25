using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Threading.Tasks;

public static class JsonHelper
{
    public static WeatherDataEntry[] FromJson<WeatherDataEntry>(string json)
    {
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
    public int[] field_colours;

    public String year = "2021";

    WeatherDataEntry[] ContactAPI()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://hacked-zg4z.onrender.com/?weather=" + year);
        UnityWebRequestAsyncOperation req = request.SendWebRequest();
        while (!req.isDone) { }
        string rawdata = request.downloadHandler.text;
        WeatherDataEntry[] data = JsonHelper.FromJson<WeatherDataEntry>("{ \"Items\":" + rawdata + "}");
        return data;
    } 
    int[] GetFieldColours()
    {
        int[] field_colours = new int[6];
        UnityWebRequest request = UnityWebRequest.Get("https://hacked-zg4z.onrender.com/?connection=getc");
        UnityWebRequestAsyncOperation req = request.SendWebRequest();
        while (!req.isDone) { }
        string rawdata = request.downloadHandler.text.Replace("[", "").Replace("]", "");
        string[] data_arr = rawdata.Split(',');
        field_colours[0] = int.Parse(data_arr[0]);
        field_colours[1] = int.Parse(data_arr[1]);
        field_colours[2] = int.Parse(data_arr[2]);
        field_colours[3] = int.Parse(data_arr[3]);
        field_colours[4] = int.Parse(data_arr[4]);
        field_colours[5] = int.Parse(data_arr[5]);
        return field_colours;
    } 
    
    public void SetFieldColours()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://hacked-zg4z.onrender.com/colour?0="+field_colours[0]+"&1="+field_colours[1]+"&2="+field_colours[2]+"&3="+field_colours[3]+"&4="+field_colours[4]+"&5="+field_colours[5]);
        UnityWebRequestAsyncOperation req = request.SendWebRequest();
    } 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        info = ContactAPI();
        field_colours = new int[] {0, 0, 0, 0, 0, 0};
        SetFieldColours();
    }
  
    // Update is called once per frame
    void Update()
    {
       
    }
}
