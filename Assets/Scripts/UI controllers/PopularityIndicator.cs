using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopularityIndicator : MonoBehaviour
{
    public List<Sprite> sprites;

    private PopularityManager popularityManager;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        popularityManager = FindAnyObjectByType<PopularityManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float popularity = popularityManager.Popularity;
        switch (popularity)
        {
            case < 2:
                spriteRenderer.sprite = sprites[0];
                break;
            case >= 2 and < 4f:
                spriteRenderer.sprite = sprites[1];
                break;
            case >= 4f:
                spriteRenderer.sprite = sprites[2];
                break;
        }
    }
}
