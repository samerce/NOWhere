using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRotator : MonoBehaviour
{
    public float speed = 1f;
    public bool yAxis = true;

    public void RotateMesh(float speed) {
        if(yAxis) {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        } else {
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void Update() {
        RotateMesh(speed);
    }
}
