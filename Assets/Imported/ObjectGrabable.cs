using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabable : MonoBehaviour
{
    [SerializeField] private Rigidbody objectRB;
    [SerializeField] public Transform grabPointTransform;
    [SerializeField] private Vector3 newGravity;
    private void Awake()
    {
        objectRB = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if(grabPointTransform != null)
        {
            float lerpSpeed = 15f;
            Vector3 targetPosition = Vector3.Lerp(transform.position, grabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRB.MovePosition(targetPosition);
        }
    }
    public void Grab(Transform grabPointTransform)
    {
        this.grabPointTransform = grabPointTransform;
        objectRB.useGravity = false;
        objectRB.isKinematic = true;                //Solved the the jittering problem
        // objectRB.freezeRotation = true;
        // shoot.readyToShoot = false;
    }
    public void Drop()
    {
        grabPointTransform = null;
        objectRB.useGravity = true;
        objectRB.isKinematic = false;               //Solved the the jittering problem
        // objectRB.freezeRotation = false;
        // shoot.readyToShoot = true;
    }
    public void Throw()
    {
        newGravity = Vector3.up * -25;
        Physics.gravity = newGravity;
        float throwForce = 20f;
        objectRB.isKinematic = false;               //Solved the the jittering problem
        objectRB.useGravity = true;
        objectRB.velocity = grabPointTransform.forward * throwForce + grabPointTransform.up * 3;
        grabPointTransform = null;
        // shoot.readyToShoot = true;
    }    
}
