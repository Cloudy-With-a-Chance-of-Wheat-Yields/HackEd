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
       

    }

    public void FnInterventionSpray()
    {
        

    }

    public void FnInterventionIrrigate()
    {


    }
    public void FnInterventionHarvest()
    {

        weatherManager.isPlanted = false;
    }
}
