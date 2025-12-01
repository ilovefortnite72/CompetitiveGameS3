using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public enum AttackDirection
    {
        Right,
        Left
    }

    public AttackDirection attackDirection;

    Vector2 RattackOffset;
    Collider2D attackCollidor;


    private void Start()
    {
        attackCollidor = GetComponent<Collider2D>();
        RattackOffset = transform.localPosition;
    }

    
    public void StartAttack()
    {
        switch (attackDirection)
        {
            case AttackDirection.Right:
                AttackRight();
                break;
            case AttackDirection.Left:
                AttackLeft();
                break;
        }
    }


    public void AttackRight()
    {
        Debug.Log("Attack Right");
        attackCollidor.enabled = true;
    }

    public void AttackLeft()
    {
        Debug.Log("Attack Left");
        attackCollidor.enabled = true;
        transform.position = new Vector2(RattackOffset.x * -1, RattackOffset.y);
    }

    public void StopAttack()
    {
        Debug.Log("Stop Attack");
        attackCollidor.enabled = false;
    }
}
