using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    GameObject player;

    public Transform interactionTransform;

    bool isFocus;

    bool hasInteracted = false;

    private void Awake()
    {
        if (interactionTransform == null)
            interactionTransform = this.transform;

    }
    private void Update()
    {
        if (isFocus && hasInteracted == false)
        {
            float playerDistance = Vector3.Distance(interactionTransform.position, player.transform.position);
            if (playerDistance <= this.radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + this.gameObject.name);
    }
    private void OnDrawGizmosSelected()
    {

        if (interactionTransform == null)
            interactionTransform = this.transform;

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(this.interactionTransform.position, radius);
    }

    public void OnFocused(GameObject player)
    {
        isFocus = true;
        this.player = player;
        hasInteracted = false;
    }

    public void onDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

}
