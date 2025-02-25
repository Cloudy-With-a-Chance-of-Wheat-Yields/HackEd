using System.Data.Common;
using UnityEngine;

public class PlantsGrowing : MonoBehaviour
{

    [Header("Values")]
    public float plantHealth;
    public float growthRate; 
    public float growthY = 1;
    public float growthX = 1;
    public float growthZ = 1;
    public bool isDiseased = false;
    public bool highHeat = false;

    [Header("Objects needed")]
    public WeatherManager weatherManagerScript;
    public GameObject plantPrefab;
    public Material plantMaterial;

    public Interventions interventionsScript;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        weatherManagerScript = FindFirstObjectByType<WeatherManager>().GetComponent<WeatherManager>();
        plantHealth = weatherManagerScript.fltHealthCul;
        growthRate = weatherManagerScript.fltGrowthCul;
        isDiseased = weatherManagerScript.isDiseased;

        RefreshPlants();

        //interventionsScript = transform.root.GetComponent<Interventions>();

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown("Space"))
        {
            RefreshPlants();
            if (highHeat == true) PlantWilting();
            if (isDiseased == true) PlantDisease();
            interventionsScript.FnGrowToGrass();
        }
    }

    // increase y scale of plant by multiplying height by plant health
    public void PlantGrowth()
    {
        growthY *= growthRate;
        growthX *= (growthRate/4);
        growthZ *= (growthRate/4);
        plantPrefab.transform.localScale = new Vector3(1, growthY, 1);
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
