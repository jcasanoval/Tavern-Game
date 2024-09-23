using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    private bool isOpen;
    public float nightTime = 30f;

    void Start()
    {
        Open();
    }

    public void Open()
    {
        isOpen = true;
        StartCoroutine(Close());
    }

    public bool IsOpen()
    {
        bool customersExist = FindObjectsOfType<Customer>().Length > 0;

        return isOpen || customersExist;
    }

    public bool IsClosing()
    {
        return !isOpen;
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(nightTime);

        isOpen = false;
    }
}
