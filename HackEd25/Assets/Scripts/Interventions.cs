using UnityEngine;

public class Interventions : MonoBehaviour
{

    [SerializeField] WeatherManager weatherManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weatherManager = GetComponent<WeatherManager>();

    }

    public void FnInterventionSow()
    {
        weatherManager.isPlanted = true;
        weatherManager.intMonthPlanted = -1;
        Debug.Log("sowing function called");

        Transform[] allChildren = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in allChildren)
        {
            if (child.name == "Seed")
            {
                child.gameObject.SetActive(true);
            }
        }

       // cost function
    }

    public void FnInterventionSpray()
    {
        weatherManager.isSpray = true;
        //cost function
    }

    public void FnInterventionIrrigate()
    {
        weatherManager.isIrrigate = true;
        //cost function
    }
    public void FnInterventionHarvest()
    {
        weatherManager.isPlanted = false;
        //cost function
        // revenue 
    }

    public void FnGrowToGrass()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in allChildren)
        {
            if (child.name == "Seed")
            {
                child.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in allChildren)
        {
            if (child.name == "Grass_02")
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}