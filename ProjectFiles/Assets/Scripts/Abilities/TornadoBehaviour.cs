using UnityEngine;

public class TornadoBehaviour : MonoBehaviour
{
    public float lifetime = 5f;
    public float pushForce = 5f;
    public float pushRadius = 2f;
    public bool hasExploded = false;
    private float travelTimer = 5;
    private float travelTime;

    private float speed;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    public void Initialize(Vector2 direction)
    {

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }

        rb.AddForce(direction * speed);
    }

    private void Update()
    {
        if (hasExploded)
        {
            return;
        }
        PushEnemies();
        travelTimer += Time.deltaTime;

        if (travelTimer >= travelTime)
        {
            Deactivate();
            travelTimer = travelTime;
        }
    }

    private void Deactivate()
    {
        Destroy(gameObject);
    }

    private void PushEnemies()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, pushRadius);
        {
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Rigidbody2D rb = hit.attachedRigidbody;
                    if (rb)
                    {
                        Vector2 pushDir = (hit.transform.position - transform.position).normalized;
                        rb.AddForce(pushDir * pushForce, ForceMode2D.Force);
                    }
                }
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, pushRadius);
    }
}
