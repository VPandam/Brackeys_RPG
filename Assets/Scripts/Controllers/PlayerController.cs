using UnityEngine.EventSystems;
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
        //Checks if the cursor is hovering a UI item, if it is cancel the update execution.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //Checks if we are pressing rightclick in groundmask.
        //If so, move the player to that point.
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

        //Checks if we are clicking leftclick into a GO with a interactable script.
        //If so, set focus on it.
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
    //Set the focus.
    //Tells to the interactable object is being focused by the player.
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
    //Set the focus.
    //Tells to the interactable object is being defocused by the player.
    public void RemoveFocus()
    {
        if (currentFocus != null)
        {

            currentFocus.onDefocused();
            currentFocus = null;
        }
    }
}
