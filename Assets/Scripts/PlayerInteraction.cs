using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInteraction : MonoBehaviour
{
    private TutorialManager tutorialManager;
    [SerializeField]
    private SpriteRenderer hoverIcon;
    private Interactable lastHover;
    
    void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    private Interactable currentInteractable
    {
        get
        {
            if (nearbyInteractables.Count == 0) return null;

            return nearbyInteractables.OrderBy(i => Vector3.Distance(transform.position, i.transform.position)).FirstOrDefault();
        }
    }

    [SerializeField]
    private List<Interactable> nearbyInteractables = new List<Interactable>();

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            nearbyInteractables.Add(interactable);

            ShowHover(interactable);
        }
    }

    public void ShowHover(Interactable interactable)
    {
        Sprite icon = interactable.GetHoverIcon();
        if (icon != null)
        {
            hoverIcon.sprite = icon;
            hoverIcon.enabled = true;
            lastHover = interactable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            nearbyInteractables.Remove(interactable);

            HideHover(interactable);
        }
    }
    
    public void HideHover(Interactable interactable)
    {
        if (lastHover == interactable)
        {
            hoverIcon.enabled = false;
        }
    }

    public void HideHover()
    {
        hoverIcon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearbyInteractables.Count > 0)
        {
            var sortedInteractables = nearbyInteractables.OrderBy(i => Vector3.Distance(transform.position, i.transform.position));

            foreach (var interactable in sortedInteractables)
            {
                if (interactable.Interact())
                {
                    break;
                }
            }

        }
    }

    public void RemoveInteractable(Interactable interactable)
    {
        if (nearbyInteractables.Contains(interactable)) {
            nearbyInteractables.Remove(interactable);
        }
    }
}
