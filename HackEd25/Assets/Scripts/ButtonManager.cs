using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    public int currentTool; // sow1, spray2, irrigate3, harvest4

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {

    }
    public void ToolSelected()
    {
        Field[] fields = FindObjectsByType<Field>(FindObjectsSortMode.None);
        foreach(Field _field in fields)
        {
            if(currentTool == 2 || currentTool == 3)
            {
                _field.transform.GetChild(0).gameObject.SetActive(false);
            }
            _field.canInteract = true;
        }
    }
    public void Sow()
    {
        ToolSelected();
        Debug.Log("Sow Seeds");
        currentTool = 1;
    }

    public void Spray()
    {
        Debug.Log("Spray pestercide");
        currentTool = 2;
        ToolSelected();
    }

    public void Irrigate()
    {
        Debug.Log("Irrigate the field");
        currentTool = 3;
        ToolSelected();
    }

    public void Harvest()
    {
        ToolSelected();
        Debug.Log("Harvest yeild");
        currentTool = 4;
    }


}
