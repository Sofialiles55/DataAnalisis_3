using UnityEngine;
using Gamekit3D;

public class EnemyDeathReporter : MonoBehaviour
{
    [Header("Optional ID (else uses object name)")]
    public string enemyId = "";

    bool logged = false;
    Damageable damageable;

    void Awake()
    {
        damageable = GetComponent<Damageable>();
        if (!damageable) damageable = GetComponentInChildren<Damageable>();
        if (!damageable) damageable = GetComponentInParent<Damageable>();
    }

    void OnEnable()
    {
        logged = false;
        if (damageable != null)
            damageable.OnDeath.AddListener(OnEnemyDeath);
    }

    void OnDisable()
    {
        if (damageable != null)
            damageable.OnDeath.RemoveListener(OnEnemyDeath);
    }

    void OnEnemyDeath()
    {
        if (logged) return;
        logged = true;

        if (Eventlogger.Instance == null) return;

        string id = string.IsNullOrWhiteSpace(enemyId) ? gameObject.name : enemyId;

        Eventlogger.Instance.LogEnemyDeath(transform.position, id);
    }
}
