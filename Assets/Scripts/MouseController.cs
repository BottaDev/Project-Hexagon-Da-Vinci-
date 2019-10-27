using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public Transform center;
    float radius = 0.1f;

    private Transform pivot;

    void Start(){
        pivot = center.transform;
        transform.parent = pivot;
        transform.position += Vector3.up * radius;
    }

    void Update(){

        Vector3 orbVector = Camera.main.WorldToScreenPoint(center.position);
        orbVector = Input.mousePosition - orbVector;
        float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;

        pivot.position = center.position;
        pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
