using UnityEngine;

public class hieuung : MonoBehaviour
{
    [SerializeField]
    public float Life_time = 0.5f;
    void Start()
    {
        Destroy(gameObject, Life_time);
    }
}

