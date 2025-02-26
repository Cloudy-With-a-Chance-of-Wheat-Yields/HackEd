using UnityEngine;

public class Field : MonoBehaviour
{
    public bool canInteract = false;
    public Transform prefab;
    private Interventions interventions;
    private WeatherManager weatherManager;
    private void Start()
    {
        interventions = GetComponent<Interventions>();
        weatherManager = GetComponent<WeatherManager>();
        
    }
    private void Update()
    {
        if(ButtonManager.instance.currentTool == 1)
        {
            transform.GetChild(0).gameObject.SetActive(!weatherManager.isPlanted);
        }

        if (ButtonManager.instance.currentTool == 4)
        {
            transform.GetChild(0).gameObject.SetActive(weatherManager.isPlanted);
        }

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     if (weatherManager.strCurrentMonth == "October")
        //     {
        //         interventions.FnGrowToGrass();
        //     }
            
        // }

    }
    private void OnMouseEnter()
    {

        if (ButtonManager.instance.currentTool == 2 || ButtonManager.instance.currentTool == 3 ) 
        {
            if (weatherManager.isPlanted) transform.GetChild(0).gameObject.SetActive(true);

        }


    }

    private void OnMouseExit()
    {
        if (ButtonManager.instance.currentTool == 2 || ButtonManager.instance.currentTool == 3 && weatherManager.isPlanted == true)
        {
            if (weatherManager.isPlanted) transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        // sow
        if(ButtonManager.instance.currentTool == 1 && weatherManager.isPlanted == false)
        {   
            transform.Find("Crop").gameObject.SetActive(true);
            canInteract = false;
            interventions.FnInterventionSow();
        }
        // spray
        if (ButtonManager.instance.currentTool == 2)
        {
            interventions.FnInterventionSpray();
        }
        //Irrigate
        if (ButtonManager.instance.currentTool == 3)
        {
            interventions.FnInterventionIrrigate();
        }
        // harvest
        if (ButtonManager.instance.currentTool == 4 && weatherManager.isPlanted == true)
        {
            
            transform.Find("Crop").gameObject.SetActive(false);
            canInteract = false;
            interventions.FnInterventionHarvest();
        }
    
    }
}
