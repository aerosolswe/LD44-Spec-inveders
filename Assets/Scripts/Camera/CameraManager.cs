using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHitInfo {

    public GameObject gameObject;
    public Vector3 point;
}

public class CameraManager : MonoBehaviour {

    public static CameraManager instance = null;

    [HideInInspector]
    public Camera worldCamera;

    private void Awake() {

        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        worldCamera = GetComponentInChildren<Camera>();
    }

    void Start(){
    }

    void Update(){
        /*if (Input.GetMouseButtonDown(0)) {
            RayHitInfo info = GetRayHitInfo();

            if (info != null) {
                Debug.Log("Hit " + info.gameObject + " at point " + info.point);
            }
        }*/
    }

    public RayHitInfo GetRayHitInfo() {
        RayHitInfo hitInfo = null;

        Ray ray = worldCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000)) {
            if (hit.transform == null)
                return null;

            hitInfo = new RayHitInfo();
            hitInfo.gameObject = hit.transform.gameObject;
            hitInfo.point = hit.point;

            return hitInfo;
        }

        return null;
    }
}
