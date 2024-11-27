using UnityEngine;
using UnityEngine.UI;

public class AdviceManager : MonoBehaviour
{
    public Image AdviceDisplay;

    public void ShowSprite(Sprite newSprite)
    {
        if (newSprite != null)
        {
            AdviceDisplay.sprite = newSprite;
            
            var tempColor = AdviceDisplay.color;
            tempColor.a = 1;
            AdviceDisplay.color = tempColor;
        }
    }

    public void HideAdvice()
    {
        var tempColor = AdviceDisplay.color;
        tempColor.a = 0;
        AdviceDisplay.color = tempColor;
    }
}
