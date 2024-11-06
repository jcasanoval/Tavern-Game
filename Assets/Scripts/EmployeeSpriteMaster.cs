using System;
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
        Tuple<string, int>[] linkedTags = new Tuple<string, int>[CharacterParts.Length];
        for (int i = 0; i < CharacterParts.Length && i < spriteHolder.Length; i++)
        {
            bool found = false;
            foreach (Tuple<string, int> linkedTag in linkedTags)
            {
                if(linkedTag != null){
                    if (linkedTag.Item1 != "" && linkedTag.Item1 == spriteHolder[i].pairTag)
                    {
                        SetSprite(spriteHolder[i], CharacterParts[i], false, linkedTag.Item2);
                        found = true;
                        break;
                    }
                }
            }
            if (!found)
            {
                int res = SetSprite(spriteHolder[i], CharacterParts[i]);
                linkedTags[i] = new Tuple<string, int>(spriteHolder[i].pairTag, res);
            }
        }
    }

    public int SetSprite(SpriteHolder spriteHolder, GameObject gameObject, bool random = true, int index = 0)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (random)
        {
            int rand = UnityEngine.Random.Range(0, spriteHolder.GetSpriteCount());
            spriteRenderer.sprite = spriteHolder.GetSprite(rand);
            return rand;
        }
        else
        {
            spriteRenderer.sprite = spriteHolder.GetSprite(index);
            return index;
        }
    }

    public void Start()
    {
        LoadSprites();
    }

    public void Update()
    {
        
    }
}
