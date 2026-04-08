using UnityEngine;

public class jump_pad : MonoBehaviour
{
    [SerializeField] private float Jump = 20f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, 0);

                rb.AddForce(Vector2.up * Jump, ForceMode2D.Impulse);
            }
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
