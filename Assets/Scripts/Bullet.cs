using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 20f;
    [SerializeField] private int maxBounces = 5;
    private int currentBounces;

    void OnEnable()
    {
        currentBounces = 0;
        rb.linearVelocity = transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        currentBounces++;
        if(currentBounces >= maxBounces)
        {
            rb.linearVelocity = Vector2.zero;
            gameObject.SetActive(false);
            GameManager.Instance.ChangeState(GameState.Aiming);
        }
    }
}
