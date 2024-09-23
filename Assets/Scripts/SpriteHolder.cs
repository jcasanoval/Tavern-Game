using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHolder : MonoBehaviour
{
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite GetRandomSprite()
    {
        return sprites[Random.Range(0, sprites.Length)];
    }

    public Sprite GetSprite(int index)
    {
        return sprites[index];
    }

    public int GetSpriteCount()
    {
        return sprites.Length;
    }

    public Sprite GetProfile()
    {
        return sprites[0];
    }

    public Sprite GetDrinking(bool forward)
    {
        if (forward)
        {
            return sprites[2];
        }
        else
        {
            return sprites[4];
        }
    }

    public Sprite GetSitting(bool forward)
    {
        if (forward)
        {
            return sprites[1];
        }
        else
        {
            return sprites[3];
        }
    }
}
