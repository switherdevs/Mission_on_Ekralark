using UnityEngine;

public class TenLua : MonoBehaviour
{
    [SerializeField] private float Speed = 20f;
    [SerializeField] private LayerMask WallCheck;
    [SerializeField] private float Life = 5f;
 
    private Rigidbody2D rb;
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();   

       rb.linearVelocity = transform.right * Speed;
      
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Map"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("spear"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Dan"))
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        Destroy (gameObject,Life);
    }
}
