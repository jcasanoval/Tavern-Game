using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public GameObject indicator;

    void Update()
    {
        if (target != null)
        {
            // Calculate direction to the target
            Vector3 direction = (target.position - player.position).normalized;

            // Align the arrow to point in the direction
            transform.rotation = Quaternion.LookRotation(direction);

            // Position the arrow in front of the player along the direction of the target
            float distanceInFront = 0.6f;
            transform.position = player.position + direction * distanceInFront;

            // Hide the arrow if the target is close to the player
            float distance = Vector3.Distance(player.position, target.position);
            indicator.SetActive(distance > 2f);
        }
        else if (indicator.activeSelf)
        {
            indicator.SetActive(false);
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void ClearTarget()
    {
        target = null;
    }
}
