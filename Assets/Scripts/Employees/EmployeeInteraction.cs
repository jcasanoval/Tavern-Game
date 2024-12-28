using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class EmployeeInteraction : Interactor
{

    public GameObject employee;
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
            interactable.BecomeFather(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            nearbyInteractables.Remove(interactable);
        }
    }

    // Update is called once per frame
    public override void TryToInteract()
    {
        if (nearbyInteractables.Count > 0)
        {
            var sortedInteractables = nearbyInteractables.OrderBy(i => Vector3.Distance(transform.position, i.transform.position));

            foreach (var interactable in sortedInteractables)
            {
                if (interactable.NPCInteract(employee))
                {
                    break;
                }
            }

        }
    }

    public override void RemoveInteractable(Interactable interactable)
    {
        if (nearbyInteractables.Contains(interactable)) {
            nearbyInteractables.Remove(interactable);
        }
    }
}
