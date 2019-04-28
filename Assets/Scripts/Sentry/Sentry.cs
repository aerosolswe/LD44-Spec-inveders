using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentry : MonoBehaviour {

    private WaitForSeconds attackRate;

    private List<Transform> targetsInRange = new List<Transform>();

    public SentryInfo SentryInfo;

    public Transform RotateTransform;
    public Transform FireTransform;

    public SentryBullet SentryBulletPrefab;

    [HideInInspector]
    public Transform currentTarget;
    [HideInInspector]
    public Pawn currentPawnTarget;

    public LayerMask attackables;

    public void Start() {
        StartCoroutine(AttackRoutine());

        attackRate = new WaitForSeconds(SentryInfo.attackRate);
    }

    public IEnumerator AttackRoutine() {
        bool attacking = true;

        while (attacking) {
            TryFire();

            yield return attackRate;
        }
    }

    public void Update() {
        targetsInRange.Clear();
        bool currentTargetOutOfReach = true;

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, SentryInfo.Range, attackables);

        if (colliders != null && colliders.Length > 0) {
            for (int i = 0; i < colliders.Length; i++) {
                targetsInRange.Add(colliders[i].transform);

                if (CurrentTarget != null && CurrentTarget == colliders[i].transform) {
                    currentTargetOutOfReach = false;
                }
            }
        }

        if (targetsInRange.Count == 0) {
            CurrentTarget = null;

            SmoothLook(null);
            return;
        }

        if (CurrentTarget == null) {
            SelectTarget();
        } else {
            if (currentTargetOutOfReach) {
                CurrentTarget = null;
                SelectTarget();
            } else {
                if (currentPawnTarget.Dead) {
                    currentTarget = null;
                    SelectTarget();
                }
            }
        }

        if (CurrentTarget != null) {
            SmoothLook(CurrentTarget);
        }
    }

    public void SmoothLook(Transform target) {
        if (target == null) {
            Quaternion rot = RotateTransform.localRotation;
            rot = Quaternion.Lerp(rot, Quaternion.identity, 10 * Time.deltaTime);
            RotateTransform.localRotation = rot;
        } else {

            Quaternion oldRot = RotateTransform.localRotation;

            RotateTransform.LookAt(CurrentTarget);
            Quaternion newRot = RotateTransform.localRotation;
            Quaternion rot = Quaternion.Lerp(oldRot, newRot, 10 * Time.deltaTime);
            RotateTransform.localRotation = rot;
        }

    }

    public void TryFire() {
        if (CurrentTarget == null) {
            return;
        }

        SentryBullet sb = Instantiate(SentryBulletPrefab, this.transform);
        sb.SetPositions(FireTransform.position, currentPawnTarget.transform.position);

        currentPawnTarget.Health -= SentryInfo.Damage;
    }

    public void SelectTarget() {
        List<Transform> candidates = SortListByRandom(targetsInRange);

        CurrentTarget = candidates[0];
    }

    public List<Transform> SortListByRandom(List<Transform> list) {
        List<Transform> dupList = new List<Transform>(list);

        for (int i = 0; i < dupList.Count; i++) {
            Transform temp = dupList[i];
            int randomIndex = Random.Range(i, dupList.Count);
            dupList[i] = dupList[randomIndex];
            dupList[randomIndex] = temp;
        }

        return dupList;
    }

    public List<Transform> SortListByDistance(List<Transform> list) {
        List<Transform> dupList = new List<Transform>(list);

        return dupList;
    }

    public Transform CurrentTarget {
        get {
            return currentTarget;
        }
        set {
            currentTarget = value;

            if (value == null) {
                currentPawnTarget = null;
            } else {
                currentPawnTarget = currentTarget.GetComponentInParent<Pawn>();
            }

        }
    }

    public void OnDrawGizmosSelected() {
        if (SentryInfo != null) {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, SentryInfo.Range);
        }

        Gizmos.color = Color.cyan;
        foreach (Transform target in targetsInRange) {
            if (target == null)
                continue;

            Gizmos.DrawLine(transform.position, target.position);
        }

        if (currentTarget != null) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, currentTarget.position);
        }
    }
}
