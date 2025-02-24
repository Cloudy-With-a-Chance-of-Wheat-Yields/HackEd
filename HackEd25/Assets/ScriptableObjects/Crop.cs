using UnityEngine;

[CreateAssetMenu(fileName = "Crop", menuName = "Scriptable Objects/Crop")]
public class Crop : ScriptableObject
{
    public string strName;
    public float[] fltMonthGrow = new float[13];
    public float fltRainMin;
    public float fltRainMax;
    public float fltTempMin;
    public float fltTempMax;

    public float fltRainEffect;
    public float fltTempEffect;

    public float fltRainHarmUpperThreshold;
    public float fltRainHarmLowerThreshold;
    public float fltTempHarmThreshold;

    public float fltDiseasedChance;

}
