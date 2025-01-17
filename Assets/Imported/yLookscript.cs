using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FIRST TRY LESSSGOOOOOOOOOO
public class yLookscript : MonoBehaviour
{
    [SerializeField] private float ySens = 100f;
    float yMouse;
    private void Update()
    {
        yMouse = Input.GetAxis("Mouse Y") * ySens;
        transform.Rotate(Time.deltaTime * -yMouse * Vector3.right);
    }  
}
