using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Camera cam;

    public bool grabbed = false;

    private Vector2 mousePos = new Vector2();
    private Vector3 velocity = new Vector3();

    public float dragSpeed = 1;
    private Vector3 dragOrigin;

    private void Start() {
        cam = CameraManager.instance.worldCamera;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            dragOrigin = GetMousePosition();
            grabbed = true;
            return;
        }

        if (Input.GetMouseButtonUp(0)) {
            grabbed = false;
        }

        if (grabbed) {
            Vector3 pos = Camera.main.ScreenToViewportPoint(GetMousePosition() - dragOrigin);
            velocity = new Vector3(-pos.x * dragSpeed, 0, -pos.y * dragSpeed);
            dragOrigin = GetMousePosition();
        }

        transform.Translate(velocity, Space.World);

        velocity = Vector3.Lerp(velocity, Vector3.zero, 10 * Time.deltaTime);
        /*if (Input.GetMouseButtonDown(0)) {
            grabbed = true;
            mousePos = GetMousePosition();
        }

        if (Input.GetMouseButtonUp(0)) {
            grabbed = false;
        }

        

        Vector2 mouseDelta = new Vector2();

        if (grabbed) {
            Vector2 mPos = GetMousePosition();

            mouseDelta = mPos - mousePos;
            Debug.Log(mouseDelta);

            mousePos = mPos;
        }

        velocity.x -= mouseDelta.x * Time.deltaTime;
        velocity.z -= mouseDelta.y * Time.deltaTime;

        Vector3 pos = transform.position;
        pos += velocity;
        transform.position = pos;

        velocity = Vector3.Lerp(velocity, Vector3.zero, 10 * Time.deltaTime);*/
    }

    Vector3 GetMousePosition() {
        Vector2 mousePos = Input.mousePosition;
        //Vector3 worldMousePos = cam.screen

        return mousePos;
    }

}
