using System.Data.Common;
using UnityEngine;

public class PlantsGrowing : MonoBehaviour
{

    [Header("Values")]
    public float plantHealth;
    public float growthRate; 
    public float growthY;
    public float growthX;
    public float growthZ;
    public bool isDiseased = false;
    public bool highHeat = false;
    public bool isTimeToGrow = false;
    Vector3 initialScale;

    [Header("Objects needed")]
    public WeatherManager weatherManagerScript;
    public GameObject plantPrefab;
    public Material plantMaterial;

    Interventions interventionsScript;
    MonthOnGlobal monthOnScript;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        weatherManagerScript = FindFirstObjectByType<WeatherManager>().GetComponent<WeatherManager>();
        plantHealth = weatherManagerScript.fltHealthCul;
        growthRate = weatherManagerScript.fltGrowthCul;
        isDiseased = weatherManagerScript.isDiseased;

        RefreshPlants();

        interventionsScript = GetComponentInParent<Interventions>();
        monthOnScript = FindAnyObjectByType<MonthOnGlobal>().GetComponent<MonthOnGlobal>();

        initialScale = plantPrefab.transform.localScale;


    }

    // Update is called once per frame
    void Update()
    {

        if (monthOnScript.monthOn == true)
        {
            plantHealth = weatherManagerScript.fltHealthCul;
            growthRate = weatherManagerScript.fltGrowthCul;
            isDiseased = weatherManagerScript.isDiseased;

            isTimeToGrow = true;
            Debug.Log("calling from PlantsGrowing");
            RefreshPlants();
            if (highHeat == true) PlantWilting();
            if (isDiseased == true) PlantDisease();
            PlantGrowth();
            isTimeToGrow = false;
            
        }
    }

    // increase y scale of plant by multiplying height by plant health
    public void PlantGrowth()
    {
        growthRate = Mathf.Clamp01(growthRate + 0.01f);

        float scaleFactor = Mathf.Lerp(1f, 10f, growthRate); 

        float growthX = initialScale.x * (scaleFactor / 4);
        float growthY = initialScale.y * (scaleFactor / 2);
        float growthZ = initialScale.z * (scaleFactor / 4);

        plantPrefab.transform.localScale = new Vector3(growthX, growthY, growthZ);
    }

    public void RefreshPlants()
    {
        plantMaterial.color = Color.white;
        plantMaterial.SetFloat("_SnowFade", 0);
    }
    
    // add disease shader to plants
    public void PlantDisease()
    {
        plantMaterial.SetColor("_SnowColor", new Color(0.45f, 0.25f, 0.25f, 0.5f)); 
        plantMaterial.SetFloat("_SnowFade", 1);

    }

    // adjust sprite colour or shader to make plants yellow/wilting
    public void PlantWilting()
    {
        plantMaterial.SetColor("_SnowColor", new Color(1, 0.6f, 0, 0.5f)); 
        plantMaterial.SetFloat("_SnowFade", 1);
    }

}
