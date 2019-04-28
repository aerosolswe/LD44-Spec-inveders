using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sentry", menuName = "Sentries/Sentry", order = 1)]
public class SentryInfo : ScriptableObject {

    public int Damage = 25;
    public float Range = 3;
    public float attackRate = 0.3f;

}
