using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { OVER_TIME, INSTANTANEOUS, BOTH };

public interface IDamaging
{
    DamageType DamageType { get; }
    float DamageOverTime { get; }
    float DamageInstantaneous { get; }
    string IgnoreCollisionTag { get; }
    void RegisterHit(bool blocked);
    void UnregisterHit();
}
