using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EmployeeSpriteMaster : MonoBehaviour
{
    public GameObject[] CharacterParts;
    public PartsHolder partsHolder;

    public float time = 0.0f;


    public void LoadSprites()
    {
        SpriteHolder[] spriteHolder = partsHolder.GetParts();
        for (int i = 0; i < CharacterParts.Length && i < spriteHolder.Length; i++)
        {
            SetSprite(spriteHolder[i], CharacterParts[i]);
        }
    }

    public void SetSprite(SpriteHolder spriteHolder, GameObject gameObject)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteHolder.GetRandomSprite();
    }

    public void Start()
    {
        LoadSprites();
    }

    public void Update()
    {
        time += Time.deltaTime;
        if (time > 5.0f)
        {
            LoadSprites();
            time = 0.0f;
        }
    }
}
