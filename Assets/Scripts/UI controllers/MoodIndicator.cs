using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodIndicator : MonoBehaviour
{
    public List<Sprite> sprites;

    public Customer customer;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        customer = GetComponentInParent<Customer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (customer.isSitting)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            float patience = customer.PatienceLevel;
            int index = Mathf.RoundToInt(patience * (sprites.Count - 1));
            GetComponent<SpriteRenderer>().sprite = sprites[index];
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
