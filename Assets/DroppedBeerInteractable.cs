using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedBeerInteractable : Interactable{

    private HandController handController;
    public Sprite hoverIcon;
    private PlayerInteraction playerInteraction;

    public static DroppedBeerInteractable originalCopy;
    public bool original = false;

    public bool animating = false;

    public Vector3 originalPosition;
    void Awake()
    {
        handController = FindObjectOfType<HandController>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        originalPosition = transform.position;
        if(original){
            originalCopy = this;
            original = false;
        }
    }

    void Update()
    {
        if(animating){
            return;
        }
        transform.position = new Vector3(originalPosition.x, originalPosition.y + Mathf.Sqrt(Mathf.Abs(Time.time - Mathf.Ceil(Time.time) + .5f)), originalPosition.z);
    }
    public override bool Interact()
    {
        if (handController.HasFreeHands())
        {
            handController.HoldMug();
            playerInteraction.HideHover(this);
            playerInteraction.RemoveInteractable(this);
            Destroy(gameObject);
            return true;
        }

        return false;
    }

    public override Sprite GetHoverIcon()
    {
        return hoverIcon;
    }

    public static void Spawn(Vector3 position){
        DroppedBeerInteractable droppedBeer = Instantiate(originalCopy, position, Quaternion.identity);
        droppedBeer.transform.position = position;
        droppedBeer.originalPosition = position;
        droppedBeer.StartCoroutine(droppedBeer.AnimateDrop());
    }

    public IEnumerator AnimateDrop(){
        animating = true;
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        float time = 0;
        while (time < .15f)
        {
            transform.position = new Vector3(transform.position.x + direction.x * Time.deltaTime*5, transform.position.y + Time.deltaTime*3, transform.position.z + direction.z * Time.deltaTime*5);
            time += Time.deltaTime;
            yield return null;
        }
        while (time < .30f)
        {
            transform.position = new Vector3(transform.position.x + direction.x * Time.deltaTime*5, transform.position.y - Time.deltaTime*3, transform.position.z + direction.z * Time.deltaTime*5);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
        originalPosition = transform.position;
        animating = false;

    }
}
