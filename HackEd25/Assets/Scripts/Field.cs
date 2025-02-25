using UnityEngine;

public class Field : MonoBehaviour
{
    public bool canInteract = false;
    public Transform prefab;

    private void Update()
    {
        if(ButtonManager.instance.currentTool == 1 || ButtonManager.instance.currentTool == 4)
        {
            transform.GetChild(0).gameObject.SetActive(canInteract);
        }
       
    }
    private void OnMouseEnter()
    {
        //transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        //transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        // sow
        if(ButtonManager.instance.currentTool == 1)
        {
            transform.Find("Crop").gameObject.SetActive(true);
            canInteract = false;
        }
        // spray
        if (ButtonManager.instance.currentTool == 2)
        {

        }
        //Irrigate
        if (ButtonManager.instance.currentTool == 3)
        {

        }
        // harvest
        if (ButtonManager.instance.currentTool == 4)
        {
            transform.Find("Crop").gameObject.SetActive(false);
            canInteract = false;
        }
    
    }
}
