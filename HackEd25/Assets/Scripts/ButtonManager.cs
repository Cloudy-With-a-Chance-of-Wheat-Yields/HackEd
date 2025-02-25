using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Material mat;
    public Vector3 mouseOffset = new Vector3(0, 6, 0);
    public Text sowText, sprayText, irrigateText, harvestText;
    public RectTransform sowTextTransform, sprayTextTransform, irrigateTextTransform, harvestTextTransform;

    void Update()
    {
        Text[] texts = { sowText, sprayText, irrigateText, harvestText };
        RectTransform[] transforms = { sowTextTransform, sprayTextTransform, irrigateTextTransform, harvestTextTransform };

        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i].gameObject.activeSelf)
            {
                FollowMouse(transforms[i]);
            }
        }
    }

    public void Sow()
    {
        Debug.Log("Sow Seeds");
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

    public void FollowMouse(RectTransform textTransform)
    {
        textTransform.position = Input.mousePosition + mouseOffset;
    }
}
