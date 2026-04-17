using UnityEngine;

public class bulllet : MonoBehaviour
{
    [SerializeField] private GameObject bullletHieuUng;
    public float Life_time = 3f;
    void Start()
    {
        Destroy(gameObject, Life_time);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if 
            ( collision.gameObject.CompareTag("Map") || (collision.gameObject.CompareTag("spear"))  || (collision.gameObject.CompareTag("TenLua") || collision.gameObject.CompareTag("Boss")) )
        {
            GameObject HieuUng = Instantiate
            (bullletHieuUng, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(HieuUng,0.3f);
            Destroy(gameObject);
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            Destroy(gameObject);
        }
        
    }


}
