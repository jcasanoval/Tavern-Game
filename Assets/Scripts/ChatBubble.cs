using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatBubble : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    [SerializeField]
    private Sprite bubbleSprite;
    
    [SerializeField]
    private Sprite bubbleSpriteWithKey;

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }

    public void SetBubbleKey(bool hasKey)
    {
        Transform[] components = this.GetComponentsInChildren<Transform>();
        foreach (Transform component in components)
        {
            if (component.name == "Container")
            {
                component.GetComponentInChildren<Image>().sprite = hasKey ? bubbleSpriteWithKey : bubbleSprite;
            }
        }
    }
}
