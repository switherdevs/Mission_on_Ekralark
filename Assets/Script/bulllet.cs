using UnityEngine;

public class bulllet : MonoBehaviour
{
    public float Life_time = 3f;
    void Start()
    {
        Destroy(gameObject, Life_time);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Map"))
        {
            Destroy(gameObject);
        }
    }

}
