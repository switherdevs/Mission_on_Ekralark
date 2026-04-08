using UnityEngine;

public class BoiCanh : MonoBehaviour
{
    [SerializeField]
    public Transform cam;
    public float Tranlate;
    void LateUpdate()
    {
        float camx = cam.position.x;
        float camy = cam.position.y;


        transform.position = new Vector3(camx * Tranlate,camy * Tranlate, transform.position.z);
    }
}
