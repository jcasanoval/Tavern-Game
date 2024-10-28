using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceHolder : MonoBehaviour
{
    public SpriteHolder[] customerHolders;

    public SpriteHolder[] employeeHolders;

    public SpriteHolder GetRandomCustomerSpriteHolder()
    {
        return customerHolders[Random.Range(0, customerHolders.Length)];
    }

    public SpriteHolder GetRandomEmployeeSpriteHolder()
    {
        return employeeHolders[Random.Range(0, employeeHolders.Length)];
    }
}
