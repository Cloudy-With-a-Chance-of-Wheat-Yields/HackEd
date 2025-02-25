using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    // one weather manager is needed per crop


    [Header("Data")]
    [SerializeField] DataManager dataManager;
    [SerializeField] DataImport dataImport;
    [SerializeField] int intMonth;
    [SerializeField] string[] strMonth;
    [SerializeField] int[] intWeekStartforMonth;
    public string strCurrentMonth;
    //[SerializeField] int intWeeksinMonth;


    [Header("Crop")]
    [SerializeField] Crop crop;
    [SerializeField] Crop debugCrop;
    [Header("Weather Input")]
    [SerializeField] float[] fltMaxTempCur = new float[5];
    [SerializeField] float[] fltMinTempCur = new float[5];
    [SerializeField] float[] fltRainCur = new float[5];
    [SerializeField] float[] fltRainDurCur = new float[5];

    [Header("Output")]
    public float fltGrowthMonth ;
    public float fltGrowthCul ;
    public float fltHealthMonth;
    public float fltHealthCul ;
    public bool isDiseased;
    public bool isPlanted;

    [Header ("Working")]
    [SerializeField] float fltDiseaseProbability;
    [SerializeField] float tempTmpHarm;
    [SerializeField] float tempRainHarm;
    [SerializeField] float temptmp;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         fltMaxTempCur = new float[5];
          fltMinTempCur = new float[5];
         fltRainCur = new float[5];
         fltRainDurCur = new float[5];
        fltHealthCul = 1.0f;
       
      
    }

    private void Update()
    {
        //debugging controls

        if(Input.GetKeyDown(KeyCode.P))
            {
            isPlanted = true;
            FnPlant(debugCrop);

        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            FnNewMonth();


            if (isPlanted)
            {
                FnUpdateMonth();

            }
        }
    }


    public void FnPlant(Crop newCrop)
    {
        crop = newCrop;


    }

    // Update is called once per frame
    void FnNewMonth()
    {
        intMonth += 1;
        strCurrentMonth = strMonth[intMonth];



        FnLoadMonthdata(intMonth);
                
    }


    void FnUpdateMonth()
    {

        for (int i = 0; i < 4; i++)
        {

            FnProcessWeek(i);
                    

        }

        FnProcessMonthEnd();
    }



    void FnLoadMonthdata(int intMonthTmp)
    {

        //Current Version

        for (int i = 0; i < 4; i++)
        {
            Debug.Log(dataImport.info[intWeekStartforMonth[intMonthTmp] + i].max_temperature);

            fltMaxTempCur[i] = dataImport.info[intWeekStartforMonth[intMonthTmp] + i].max_temperature;
            fltMinTempCur[i] = dataImport.info[intWeekStartforMonth[intMonthTmp] + i].min_temperature;
            fltRainCur[i] = dataImport.info[intWeekStartforMonth[intMonthTmp] + i].total_precipitation;
            fltRainDurCur[i] = dataImport.info[intWeekStartforMonth[intMonthTmp] + i].total_precipitation;



            /*
            fltMaxTempCur[i] = dataManager.fltMaxTemp[intWeekStartforMonth[intMonthTmp] + i];
            fltMinTempCur[i] = dataManager.fltMinTemp[intWeekStartforMonth[intMonthTmp] + i];
            fltRainCur[i] = dataManager.fltRain[intWeekStartforMonth[intMonthTmp] + i];
            fltRainDurCur[i] = dataManager.fltRainTime[intWeekStartforMonth[intMonthTmp] + i];
            */


        }
        fltGrowthMonth = 0;
        fltHealthMonth = 0;
        fltDiseaseProbability = 0;

    }


    void FnProcessWeek(int i)
    {
      
        
         temptmp = (fltMaxTempCur[i] - crop.fltTempMin) / (crop.fltTempMax - crop.fltTempMin);

        if(temptmp < 0)
        { temptmp = 0; }

        if(temptmp >1)
        { temptmp = 1; }

        fltGrowthMonth += crop.fltMonthGrow[intMonth] * temptmp;

         tempTmpHarm = (fltMaxTempCur[i] - crop.fltTempHarmThreshold);

        if (tempTmpHarm > 0)
        {
            fltHealthMonth += tempTmpHarm * crop.fltTempEffect;

        }

         tempRainHarm = Mathf.Max((fltRainCur[i] - crop.fltRainHarmUpperThreshold), crop.fltRainHarmLowerThreshold - fltRainCur[i] );

        if (tempRainHarm > 0)
        {

            fltHealthMonth = tempRainHarm * crop.fltRainEffect;

            fltDiseaseProbability += tempRainHarm * crop.fltRainEffect * crop.fltDiseasedChance;

        }



    }

    void FnProcessMonthEnd()
    {
        fltGrowthMonth /= 4;

        fltGrowthCul += fltGrowthMonth;
        Debug.Log("month "+ fltHealthMonth);
        fltHealthCul -= fltHealthMonth;
        Debug.Log("cul "  + fltHealthCul);

        if (!isDiseased)
        {
            if (Random.Range(0,1.0f) < fltDiseaseProbability)
            {
                isDiseased = true;  
            }


        }



    }

}
