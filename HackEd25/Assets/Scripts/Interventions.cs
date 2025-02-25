using UnityEngine;

public class Interventions : MonoBehaviour
{

    [SerializeField] WeatherManager weatherManager;
    [SerializeField] Budget budget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weatherManager = GetComponent<WeatherManager>();
        budget = FindFirstObjectByType<Budget>();
    }

    public void FnInterventionSow()
    {
        budget.FnSowCharge();
        // cost function
        if (budget.isOperationAllowed)
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
        }
      
    }

    public void FnInterventionSpray()
    {
        budget.FnSprayCharge();
        if (budget.isOperationAllowed)
        {


            weatherManager.isSpray = true;
        }
            //cost function
    }

    public void FnInterventionIrrigate()
    {
        budget.FnIrrigateCharge();
        if (budget.isOperationAllowed)
        {

            weatherManager.isIrrigate = true;
            //cost function
        }
    }
    public void FnInterventionHarvest()
    {
       budget.FnHarvestCharge();
        if (budget.isOperationAllowed)
        {

            weatherManager.isPlanted = false;
            //cost function
            // revenue 
            budget.FnHarvestRevenue(weatherManager.fltGrowthCul, weatherManager.fltHealthCul);

        }
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