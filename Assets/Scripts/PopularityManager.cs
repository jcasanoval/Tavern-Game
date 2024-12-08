using UnityEngine;

public class PopularityManager : MonoBehaviour
{
    [SerializeField]
    [Range(0, 5)]
    private float initialPopularity = 2;

    [SerializeField]
    private float popularity;
    public float Popularity { get => popularity; }

    // Start is called before the first frame update
    public void Start()
    {
        popularity = initialPopularity;
    }

    public void IncreasePopularity(float amount)
    {
        popularity = Mathf.Clamp(popularity + amount, 0, 5);
    }
}
