using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public Material mat;
    public void Sow()
    {
        Debug.Log("Sow Seeds");
        Hightlight();
    }

    public void Spray()
    {
        Debug.Log("Spray pestercide");
    }

    public void Irrigate()
    {
        Debug.Log("Irrigate the field");
    }

    public void Harvest()
    {
        Debug.Log("Harvest yeild");

    }
    void Hightlight()
    {
        mat.color = Color.white;
    }
}
