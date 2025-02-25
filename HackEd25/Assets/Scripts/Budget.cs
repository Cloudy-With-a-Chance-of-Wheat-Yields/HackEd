using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class Budget : MonoBehaviour
{

    [SerializeField] float fltSowCost =100 ;
    [SerializeField] float fltSprayCost =50;
    [SerializeField] float fltIrrigateCost =200;
    [SerializeField] float fltHarvestCost =200 ;
    [SerializeField] float fltHarvest = 2000;
    [SerializeField] float fltStartingMoney = 2500;
    public float fltMoney;
    [SerializeField] bool isOverdraftAllowed = true;
    public bool isOperationAllowed;
    public TMP_Text txtMoney;
    public Slider sldEnv;
    [SerializeField] float fltEnviromentalCred;
    [SerializeField] float fltEnviromentalCredMax=20;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fltMoney = fltStartingMoney;
        fltEnviromentalCred = fltEnviromentalCredMax;
        FnUpdateMoneyUI();
    }

   
    public void FnSowCharge()
    {
        isOperationAllowed = true; 
        if (isOverdraftAllowed || fltMoney > fltSowCost)
        {
            Debug.Log("Sow cost called");
            
            fltMoney -= fltSowCost;
            FnUpdateMoneyUI();
        }
        else
        {
            isOperationAllowed =false;

        }
      
    }

    public void FnSprayCharge()
    {
        isOperationAllowed = true;
        if (isOverdraftAllowed || fltMoney > fltSprayCost)
        {
            fltMoney -= fltSprayCost;
            fltEnviromentalCred -= 1;
            FnUpdateMoneyUI();
            FnUpdateEnvCred();
        }
        else
        {
            isOperationAllowed = false;

        }

    }

    public void FnIrrigateCharge()
    {
        isOperationAllowed = true;
        if (isOverdraftAllowed || fltMoney > fltIrrigateCost)
        {
            fltMoney -= fltIrrigateCost;
            FnUpdateMoneyUI();
        }
        else
        {
            isOperationAllowed = false;

        }

    }

    public void FnHarvestCharge()
    {
        isOperationAllowed = true;
        if (isOverdraftAllowed || fltMoney > fltHarvestCost)
        {
            fltMoney -= fltHarvestCost;
            FnUpdateMoneyUI();
        }
        else
        {
            isOperationAllowed = false;

        }

    }

    public void FnHarvestRevenue(float Growth, float Health)
    {

        fltMoney += fltHarvest * Growth * Health;
        FnUpdateMoneyUI();



    }


    public void FnUpdateMoneyUI()
    {
        txtMoney.text = "Money: " + Mathf.FloorToInt(fltMoney);


    }

    public void FnUpdateEnvCred()
    {
        sldEnv.value = fltEnviromentalCred;

    }

}
