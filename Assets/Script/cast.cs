using UnityEngine;

public class cast : MonoBehaviour
{
    public Transform vitri;
    public float PickupRage = 2.0f;
    private GameObject carry;
    private Rigidbody2D rigItem;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(carry == null)
            {
                pickup();
            }
            else 
            {
                drop();
            }
        }
        
    }
    void pickup()
    {
        Collider2D[] item = Physics2D.OverlapCircleAll(transform.position, PickupRage);
        
        foreach (Collider2D collider in item)
        {

            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            Transform itemPo = collider.GetComponent<Transform>();
            
            rigItem = rb;
            if (collider.GetComponent<Rigidbody2D>() != null && collider.gameObject != gameObject)
            {
                carry = collider.gameObject;
                rb.simulated = false;
                if (rb != null)
                {

                    itemPo.SetParent(vitri);

                    itemPo.localPosition = Vector3.zero;

                    itemPo.localRotation = Quaternion.identity;
                }
                break;
            }
        }
    }
    void drop()
    {
            if (rigItem != null)
            {
                 rigItem.simulated = true;
                 rigItem.transform.SetParent(null);
            }

       carry = null;
        rigItem = null;
    }
}
