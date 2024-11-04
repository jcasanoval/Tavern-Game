using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }
}
