using UnityEngine;

public class PiFieldColour : MonoBehaviour
{
    public int intFieldIndex;
    [SerializeField] WeatherManager weatherManager;
    [SerializeField] DataImport dataImport;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {
    
        if ( weatherManager.isPlanted)
        {
            dataImport.field_colours[intFieldIndex] = 255;


        }

        else if (weatherManager.isDiseased)
        {
            dataImport.field_colours[intFieldIndex] = 1;
        }

        else
        {
            dataImport.field_colours[intFieldIndex] = 0;

        }

        dataImport.SetFieldColours();

    }



}
