using UnityEngine;

public class MonthOnGlobal : MonoBehaviour
{
    [SerializeField] WeatherManager[] weatherManagers;
    
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

        for (int i = 0; i < weatherManagers.Length; i++)
        {
            weatherManagers[i].FnUpdateMonth();


        }

        for (int i = 0; i < weatherManagers.Length; i++)
        {
            weatherManagers[i].FnNewMonth();


        }

    }


}
