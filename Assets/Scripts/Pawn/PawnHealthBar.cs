using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnHealthBar : MonoBehaviour {

    [HideInInspector]
    public Pawn pawn;
    [HideInInspector]
    public Transform lookatObject;
    public Transform mask;

    private void FixedUpdate() {
        if (lookatObject == null) {
            lookatObject = CameraManager.instance.worldCamera.transform;
        }

        transform.LookAt(lookatObject);

        SetHealth();
    }

    void SetHealth() {
        float health = 100;
        float baseHealth = 100;

        if (pawn != null) {
            health = pawn.Health;
            baseHealth = pawn.BaseHealth;
        }

        float normalizedHealth = health / baseHealth;
        normalizedHealth = Mathf.Clamp(normalizedHealth, 0, 1);

        Vector3 scale = mask.transform.localScale;
        scale.x = normalizedHealth;
        mask.transform.localScale = scale;
    }
}
