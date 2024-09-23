using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceHolder : MonoBehaviour
{
    public SpriteHolder[] spriteHolders;









    public SpriteHolder GetRandomSpriteHolder()
    {
        return spriteHolders[Random.Range(0, spriteHolders.Length)];
    }

}
