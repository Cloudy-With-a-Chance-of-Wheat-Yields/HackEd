using System.Data.Common;
using UnityEngine;

public class PlantsGrowing : MonoBehaviour
{
    public float plantHealth;
    public float growthRate; 

    public float growthY = 1;

    public bool highHumidity = false;
    public bool highHeat = false;

    public WeatherManager weatherManagerScript;

    public GameObject plantPrefab;
    public Material plantMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        weatherManagerScript = FindFirstObjectByType<WeatherManager>().GetComponent<WeatherManager>();
        //plantHealth = weatherManagerScript.plantHealth;
        plantHealth = 0.5f;
        growthRate = 0.5f;

        plantMaterial.color = Color.white;

    }

    // Update is called once per frame
    void Update()
    {
        if (highHeat == true) PlantWilting();
        if (highHumidity == true) PlantDisease();
    }

    // increase y scale of plant by multiplying height by plant health
    public void PlantGrowth()
    {
        growthY *= growthRate;
        plantPrefab.transform.localScale = new Vector3(1, growthY, 1);
    }

    public void RefreshPlants()
    {
        plantMaterial.color = Color.white;
    }
    
    // add disease shader to plants
    public void PlantDisease()
    {
        
    }

    // adjust sprite colour or shader to make plants yellow/wilting
    public void PlantWilting()
    {
        plantMaterial.color *= new Color(1, 0.6f, 0, 0.5f);
    }

}
