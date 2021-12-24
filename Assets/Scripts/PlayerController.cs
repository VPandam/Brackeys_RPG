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

    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main;

        playerMotor = GetComponent<PlayerMotor>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, groundMask))
            {
                playerMotor.MoveToPoint(hit.point);
            };
        }
        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                //Check if its interactable
            };
        }
    }
}
