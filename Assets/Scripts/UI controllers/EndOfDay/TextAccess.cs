using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAccess : MonoBehaviour
{
    public TextMeshPro moneyGainedText;
    public TextMeshPro moneySpentText;
    public TextMeshPro TotalText;
    public TextMeshPro ClientsServedText;
    public TextMeshPro ClientsLostText;
    public TextMeshPro PopularityText;
    
    public float MoneyGained{
        get => float.Parse(moneyGainedText.text);
        set => moneyGainedText.text = value.ToString();
    }
    public float MoneySpent{
        get => float.Parse(moneySpentText.text);
        set => moneySpentText.text = value.ToString();
    }

    public float Total{
        get => float.Parse(TotalText.text);
        set => TotalText.text = value.ToString();
    }

    public float ClientsServed{
        get => float.Parse(ClientsServedText.text);
        set => ClientsServedText.text = value.ToString();
    }

    public float ClientsLost{
        get => float.Parse(ClientsLostText.text);
        set => ClientsLostText.text = value.ToString();
    }

    public float Popularity{
        get => float.Parse(PopularityText.text);
        set => PopularityText.text = value.ToString();
    }






}
