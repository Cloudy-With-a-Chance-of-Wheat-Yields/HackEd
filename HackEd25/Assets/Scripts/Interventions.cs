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
}
