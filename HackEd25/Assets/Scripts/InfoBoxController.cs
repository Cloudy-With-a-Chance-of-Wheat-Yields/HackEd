using UnityEngine;
using TMPro;

public class InfoBoxController : MonoBehaviour
{
    public TMP_Text infoTitle;
    public TMP_Text infoText;
    public GameObject InfoBox; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseBox()
    {
        InfoBox.SetActive(false);
    }

    public void ShowSowingBox()
    {
        InfoBox.SetActive(true);
        infoTitle.text = "Sowing";
        infoText.text = "In the UK, winter wheat is sown in September or October. Seedlings will establish better if conditions are not too wet. Wheat can also be planted in the Spring - this is called spring wheat. Since spring wheat has less time to grow before the harvest period, there can be a lower yield compared to winter wheat.";
    }

    public void ShowHarvestBox()
    {
        InfoBox.SetActive(true);
        infoTitle.text = "Harvesting";
        infoText.text = "In the UK, wheat is usually harvested in August. Drier conditions help the grain to ripen and reduce the chances of disease, which can affect the harvest.";
    }

    public void ShowIrrigationBox()
    {
        InfoBox.SetActive(true);
        infoTitle.text = "Irrigation";
        infoText.text = "When there has been very little rain, the soil can become too dry. This is stressful for plants, which need water to grow. In this case, the crops can benefit from additional watering, such as from local rivers and reservoirs. This is called irrigation.";
    }
    public void ShowDiseasesBox()
    {
        InfoBox.SetActive(true);
        infoTitle.text = "Crop Diseases";
        infoText.text = "All crops are prone to becoming ill with diseases or being damaged by insects. When crops are damaged, they have less energy to produce a good yield. In the UK, wheat is often affected by fungal diseases like yellow rust and septoria. These diseases become more common in wet weather, which provides ideal conditions for their growth.";
    }

    public void ShowSprayingBox()
    {
        InfoBox.SetActive(true);
        infoTitle.text = "Spraying Pesticides";
        infoText.text = "Spraying pesticides can destroy diseases and pests, which prevents them from causing damage. Farmers often spray pesticides multiple times throughout a growing season - but this can be expensive. There must be a balance between protecting crops to obtain a better yield and spending money on these preventative measures, which reduces net profit.";
    }

    public void ShowForecastBox()
    {
        InfoBox.SetActive(true);
        infoTitle.text = "Weather Forecast";
        infoText.text = "Weather forecasts give an idea of the weather to come, but they are not always accurate. This can make it difficult for farmers to plan how to keep their crops healthy. You must weigh up whether the risk of drought or disease is high enough to justify the cost of interventions like irrigation and spraying pesticides, based on the weather forecast.";
    }
    
    public void ShowFundsBox()
    {
        InfoBox.SetActive(true);
        infoTitle.text = "Funds";
        infoText.text = "You expect to earn £2,000 per field, based on the current price of wheat. With 6 fields, your maximum possible earnings is £12,000. Your profit at the end will be determined by the sale of your wheat, minus the cost of seed and any interventions you have made (irrigation and spraying).";
    }
}
