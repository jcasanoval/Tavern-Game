using UnityEngine;

public class IButtonController : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public float delay = 0f;

    private bool hovering = false;

    private bool Hovering
    {
        get { return hovering; }
        set
        {
            hovering = value;
            if (hovering)
            {
                underline.SetActive(true);
            }
            else
            {
                underline.SetActive(false);
            }
        }
    }

    private GameObject underline;

    Camera menuCamera;

    void Start()
    {
        menuCamera = FindAnyObjectByType<MenuCamera>().GetComponent<Camera>();
        underline = transform.Find("Underline").gameObject;
        underline.SetActive(false);
    }

    protected virtual void OnClick()
    {
        Debug.Log("Button clicked: " + gameObject.name);
    }

    void Update()
    {
        if (delay > 0){
            delay -= Time.unscaledDeltaTime;
        }

        ray = menuCamera.ScreenPointToRay(Input.mousePosition);
        var isHovering = false;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                isHovering = true;
            }
        }
        if (Hovering != isHovering)
        {
            Hovering = isHovering;
        }
        if (Input.GetMouseButtonDown(0) && Hovering && delay <= 0)
        {
            delay = 1.5f;
            OnClick();
        }
    }
}
