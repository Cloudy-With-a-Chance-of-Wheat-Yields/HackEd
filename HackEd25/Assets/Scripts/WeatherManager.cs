using Unity.Hierarchy;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    // one weather manager is needed per crop


    [Header("Data")]
    //[SerializeField] DataManager dataManager;
    [SerializeField] DataImport dataImport;
    [SerializeField] GameOver gameOver;
    [SerializeField] int intMonth;
    [SerializeField] string[] strMonth;
    [SerializeField] int[] intWeekStartforMonth;
    public string strCurrentMonth;
    //[SerializeField] int intWeeksinMonth;


    [Header("Crop")]
    [SerializeField] Crop crop;
    [SerializeField] Crop debugCrop;
    [Header("Weather Input")]
    public float[] fltMaxTempCur = new float[5];
    public float[] fltMinTempCur = new float[5];
    public float[] fltRainCur = new float[5];
    public float[] fltRainDurCur = new float[5];

    [Header("Output")]
    public float fltGrowthMonth ;
    public float fltGrowthCul ;
    public float fltHealthMonth;
    public float fltHealthCul ;
    public bool isDiseased;
    public bool isPlanted;
    public int intMonthPlanted;

    [Header ("Working")]
    [SerializeField] float fltDiseaseProbability;
    [SerializeField] float tempTmpHarm;
    [SerializeField] float tempRainHarm;
    [SerializeField] float temptmp;
    [SerializeField] float raintmp;
    [SerializeField] float fltDiseaseEffect;


    [Header("Interventions")]
    public bool isSpray;
    public bool isIrrigate;
    [SerializeField] float fltIrrigateWater;
    



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
    public void FnNewMonth()
    {
        intMonth += 1;

        if (intMonth >12)

        {
            gameOver.FnGameOver();

        }
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
      
        if(isIrrigate)
        {
            fltRainCur[i] += fltIrrigateWater;
        }


        // basic growth on basis of temp
         temptmp = (fltMaxTempCur[i] - crop.fltTempMin) / (crop.fltTempMax - crop.fltTempMin);
        temptmp *= crop.fltTempGrowEffect;

        // grouth modified by water

        if (fltRainCur[i] < crop.fltRainMin)
        {
            raintmp = fltRainCur[i]/crop.fltTempMin;
            raintmp *= crop.fltRainGrowEffect + crop.fltRainGrowEffect * raintmp ;

        }

        else
        {
            raintmp = 1;
        }


        if(temptmp < 0)
        { temptmp = 0; }

        if(temptmp >1)
        { temptmp = 1; }

        fltGrowthMonth += crop.fltMonthGrow[intMonth - intMonthPlanted+1] * temptmp * raintmp;

        

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

        if (fltGrowthCul>1)
        {
            fltGrowthCul = 1;
        }




       

        if (!isDiseased && !isSpray)
        {
            if (Random.Range(0,1.0f) < fltDiseaseProbability)
            {
                isDiseased = true;  
            }


        }

        else
        {
            fltHealthCul += fltDiseaseEffect;

        }
        Debug.Log("month " + fltHealthMonth);

        Debug.Log("cul " + fltHealthCul);
        fltHealthCul -= fltHealthMonth;
    }

}
