using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    Camera cam;
    Ray ray;
    RaycastHit hit;
    public LayerMask groundMask;
    PlayerMotor playerMotor;

    public Interactable currentFocus;

    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main;

        playerMotor = GetComponent<PlayerMotor>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, groundMask))
            {
                playerMotor.StopFollowTarget();
                RemoveFocus();
                playerMotor.MoveToPoint(hit.point);

            };
        }
        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            };
        }

    }
    void SetFocus(Interactable newFocus)
    {
        if (currentFocus != newFocus)
        {
            if (currentFocus != null)
            {
                currentFocus.onDefocused();
            }
            currentFocus = newFocus;
            playerMotor.SetTargetToFollow(currentFocus);

        }
        newFocus.OnFocused(this.gameObject);
    }
    public void RemoveFocus()
    {
        if (currentFocus != null)
        {

            currentFocus.onDefocused();
            currentFocus = null;
        }
    }
}
