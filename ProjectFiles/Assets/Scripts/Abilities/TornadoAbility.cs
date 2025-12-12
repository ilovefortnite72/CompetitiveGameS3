using UnityEngine;

public class TornadoAbility : MonoBehaviour
{
    public GameObject tornadoPrefab;
    public Transform spawnPoint;
    public float abilityCooldown = 2f;

    [Header("Fireball Settings")]
    public float speed = 10f;
    public float detectionRadius = 2f;
    public float travelTime = 8;
    public float burnDamage = 5f;
    public float burnDuration = 4f;
    public float burnTickRate = 1f;

    private bool isOnCooldown = false;
    private float cooldownTimer = 0f;

    void Update()
    {
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
            }
        }
    }


    public bool TryExecuteAbility(Vector2 spawnPosition, Vector2 targetPosition)
    {
        if (isOnCooldown)
        {
            return false;
        }

        if (tornadoPrefab == null)
        {
            Debug.Log("No Prefab Assigned for tornado Ability");
            return false;
        }

        Vector2 direction = (targetPosition - spawnPosition).normalized;

        ExecuteAbility(spawnPosition, targetPosition);

        isOnCooldown = true;
        cooldownTimer = abilityCooldown;

        return true;
    }

    private void ExecuteAbility(Vector2 spawnPosition, Vector2 targetPosition)
    {
        GameObject tornadoInstance = Instantiate(tornadoPrefab, spawnPoint.position, Quaternion.identity);

        TornadoBehaviour projectile = tornadoInstance.GetComponent<TornadoBehaviour>();

        Debug.Log(targetPosition);
        projectile.Initialize(targetPosition);

    }

    public bool IsOnCooldown()
    {
        return isOnCooldown;
    }

    public float GetCooldownRemaining()
    {
        return Mathf.Max(0f, cooldownTimer);
    }
}
