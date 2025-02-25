using UnityEngine;

public class Interventions : MonoBehaviour
{

    [SerializeField] WeatherManager weatherManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void FnInterventionSow(Crop crop)
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


    }
}
