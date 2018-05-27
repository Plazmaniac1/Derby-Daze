using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private const float yAngleMin = 0f;
    private const float yAngleMax = 50f;

    public Transform lookAt;
    public Transform camTransform;

    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    //private float sensitivityX = 4.0f;
    //private float sensitivityY = 1.0f;

	// Use this for initialization
	void Start () {
        camTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);
	}

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
