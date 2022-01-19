using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Radius of action
    public float radius = 3f;

    public GameObject player;

    public Transform interactionTransform;

    public bool isFocus;

    bool hasInteracted = false;

    private void Awake()
    {

        if (interactionTransform == null)
            interactionTransform = this.transform;

    }
    public void OnFocused(GameObject player)
    {
        isFocus = true;
        this.player = player;
        hasInteracted = false;
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

    public virtual void Interact() { }
    private void OnDrawGizmosSelected()
    {

        if (interactionTransform == null)
            interactionTransform = this.transform;

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(this.interactionTransform.position, radius);
    }



    public void onDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

}
