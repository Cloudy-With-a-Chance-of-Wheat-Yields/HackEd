using UnityEngine;

public class MonthOnGlobal : MonoBehaviour
{
    [SerializeField] WeatherManager[] weatherManagers;

    public bool monthOn;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FnMonthOnfromButton()


    {
        monthOn = true;
        for (int i = 0; i < weatherManagers.Length; i++)
        {
            weatherManagers[i].FnUpdateMonth();
            


        }

        for (int i = 0; i < weatherManagers.Length; i++)
        {
            weatherManagers[i].FnNewMonth();


            if (weatherManagers[i].intMonth == weatherManagers[i].intMonthPlanted + 1)
            {
                weatherManagers[i].gameObject.GetComponent<Interventions>().FnGrowToGrass();
            }

            if(weatherManagers[i].strCurrentMonth == "August")
            {
                weatherManagers[i].gameObject.GetComponent<Interventions>().FnGrowMature();
            }
        }

    }


}
