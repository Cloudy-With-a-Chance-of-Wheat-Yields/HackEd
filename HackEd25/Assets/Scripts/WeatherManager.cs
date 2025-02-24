using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{

    [Header("Data")]
    [SerializeField] DataManager dataManager;
    [SerializeField] int intMonth;
    [SerializeField] string[] strMonth = new string[13];
    [SerializeField] int[] intWeekStartforMonth = new int[13];
    public string strCurrentMonth;
    [SerializeField] int intWeeksinMonth;


    [Header("Crop")]
    [SerializeField] Crop crop;
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
        intWeekStartforMonth = new int[13];
    }

    private void Update()
    {
        //debugging controls


        if (Input.GetKeyDown(KeyCode.Space))
        {
            FnNewMonth();
        }
    }


    // Update is called once per frame
    void FnNewMonth()
    {
        intMonth += 1;
        strCurrentMonth = strMonth[intMonth];


        FnLoadMonthdata(intMonth);

        for (int i = 0; i < 4; i++)
        {

            FnProcessWeek(i);

            FnProcessMonthEnd();




        }





    }


    void FnLoadMonthdata(int intMonthTmp)
    {

        //Current Version

        for (int i = 0; i < 4; i++)
        {

            fltMaxTempCur[i] = dataManager.fltMaxTemp[intWeekStartforMonth[intMonthTmp + i]];
            fltMinTempCur[i] = dataManager.fltMinTemp[intWeekStartforMonth[intMonthTmp + i]];
            fltRainCur[i] = dataManager.fltRain[intWeekStartforMonth[intMonthTmp + i]];
            fltRainDurCur[i] = dataManager.fltRainTime[intWeekStartforMonth[intMonthTmp + i]];

        }
    }


    void FnProcessWeek(int i)
    {
        fltGrowthMonth = 0;
        fltHealthMonth = 0;
        fltDiseaseProbability = 0;
        
         temptmp = (fltMaxTempCur[i] - crop.fltTempMin) / (crop.fltTempMax - crop.fltTempMin);

        if(temptmp < 0)
        { temptmp = 0; }

        if(temptmp >1)
        { temptmp = 1; }

        fltGrowthMonth = crop.fltMonthGrow[intMonth] * temptmp;

         tempTmpHarm = (fltMaxTempCur[i] - crop.fltTempHarmThreshold);

        if (tempTmpHarm > 0)
        {
            fltHealthMonth -= tempTmpHarm * crop.fltTempEffect;


        }

         tempRainHarm = Mathf.Max((fltRainCur[i] - crop.fltRainHarmUpperThreshold), crop.fltRainHarmLowerThreshold - fltRainCur[i] );

        if (tempRainHarm > 0)
        {

            fltHealthMonth -= tempRainHarm * crop.fltRainEffect;

            fltDiseaseProbability += tempRainHarm * crop.fltDiseasedChance;

        }



    }

    void FnProcessMonthEnd()
    {
        fltGrowthMonth /= 4;

        fltGrowthCul += fltGrowthMonth;
        fltHealthCul += fltHealthMonth;

        if (!isDiseased)
        {
            if (Random.Range(0,1.0f) < fltDiseaseProbability)
            {
                isDiseased = true;  
            }


        }



    }
}
