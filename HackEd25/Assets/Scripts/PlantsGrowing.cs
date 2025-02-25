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

    [Header("Objects needed")]
    public WeatherManager weatherManagerScript;
    public GameObject plantPrefab;
    public Material plantMaterial;

    Interventions interventionsScript;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        weatherManagerScript = FindFirstObjectByType<WeatherManager>().GetComponent<WeatherManager>();
        plantHealth = weatherManagerScript.fltHealthCul;
        growthRate = weatherManagerScript.fltGrowthCul;
        isDiseased = weatherManagerScript.isDiseased;

        RefreshPlants();

        interventionsScript = GetComponentInParent<Interventions>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("calling from PlantsGrowing");
            RefreshPlants();
            if (highHeat == true) PlantWilting();
            if (isDiseased == true) PlantDisease();
            
        }
    }

    // increase y scale of plant by multiplying height by plant health
    public void PlantGrowth()
    {
        growthRate *= 10;
        growthY *= growthRate;
        growthX *= (growthRate/4);
        growthZ *= (growthRate/4);
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
