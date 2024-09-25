using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiled : MonoBehaviour
{

    public MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        MeshRenderer father = transform.parent.GetComponent<MeshRenderer>();
        meshRenderer.material.mainTextureScale = new Vector2(transform.localScale.x, father.transform.localScale.y*transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
