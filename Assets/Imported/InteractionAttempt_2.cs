using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InteractionAttempt_2 : MonoBehaviour
{
    [SerializeField] private Transform mainCameraTransform;
    [SerializeField] private Transform grabPointTransform;
    public LayerMask Interactable;
    
    public ObjectGrabable objectGrabable;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(objectGrabable == null)
            {
                float interactRange = 5f;
                if(Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out RaycastHit hitInfo, interactRange, Interactable))
                {  
                    if(hitInfo.transform.TryGetComponent(out objectGrabable))
                    {
                        objectGrabable.Grab(grabPointTransform);
                    }
                }
            }
            else
            {
                objectGrabable.Drop();
                objectGrabable = null;
            }
        }
        if(objectGrabable != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                objectGrabable.Throw();
                objectGrabable = null;
            }
            if(Input.GetMouseButton(1))
            {
                objectGrabable.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
            }
        }
    }
}
