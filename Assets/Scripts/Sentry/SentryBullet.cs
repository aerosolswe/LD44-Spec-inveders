using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryBullet : MonoBehaviour {

    private static WaitForSeconds lifetime = new WaitForSeconds(0.05f);

    public LineRenderer LineRenderer;

    public void SetPositions(Vector3 position0, Vector3 position1) {
        LineRenderer.positionCount = 2;
        LineRenderer.SetPosition(0, position0);
        LineRenderer.SetPosition(1, position1);

        StartCoroutine(DelayedDestroy());
    }

    IEnumerator DelayedDestroy() {
        yield return lifetime;
        Destroy(this.gameObject);
    }

}
