using UnityEngine;
using UnityEngine.UI;

public class AdviceManager : MonoBehaviour
{
    public Image AdviceDisplay;

    public void ShowSprite(Sprite newSprite)
    {
        if (newSprite != null)
        {
            Debug.Log("Showing advice");
            AdviceDisplay.sprite = newSprite;
            
            var tempColor = AdviceDisplay.color;
            tempColor.a = 1;
            AdviceDisplay.color = tempColor;
        }
    }

    public void HideAdvice()
    {
        Debug.Log("Hiding advice");
        var tempColor = AdviceDisplay.color;
        tempColor.a = 0;
        AdviceDisplay.color = tempColor;
    }
}
