using Unity.Mathematics;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // bien sung
    [SerializeField] private Transform DuongDan;
    [SerializeField] private GameObject prefadDan;
    [SerializeField] private float TocDoBan = 0.1f;
    [SerializeField] private float TocDoDan = 20;
    // bien animatitor
    [SerializeField] private Animator Annimator_nhanvat;
    public BienDungChung animation_chung;

    //LuomSung
    [SerializeField] private GameObject PickUp_gun;
    //hieuung
    [SerializeField] private GameObject HieuUng;
    //amthanh 
    [SerializeField] private AudioSource AmThanhTong;
    [SerializeField] private AudioClip TiengSung;
    private float BienLuuTiengSung;

    private bool CoSung = false;
    private float DiemXa = 0f;

    void Start()
    {
        Annimator_nhanvat = GetComponent<Animator>();

    }

    void Update()
    {
        if (CoSung && Input.GetMouseButtonDown(0) && Time.time >= DiemXa)
        {
            Ban();
            AmThanhTong.PlayOneShot(TiengSung);
            DiemXa = Time.time + TocDoBan;

        }
    }
    void Ban()
    {
        float direction = math.sign(transform.localScale.x);
        GameObject Dan = Instantiate(prefadDan, DuongDan.position, DuongDan.rotation);
        Rigidbody2D dan = Dan.GetComponent<Rigidbody2D>();
        if (dan != null)
        {
            dan.linearVelocity = new Vector2 ( direction * TocDoDan,0 );
        }
        
        Instantiate(HieuUng, DuongDan.position, DuongDan.rotation);
        if (CoSung && Input.GetMouseButtonDown(0) && Time.time >= BienLuuTiengSung)
        {
            BienLuuTiengSung = Time.time + 0.1f;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gun") && !CoSung)
        {
            NhatSung(other.gameObject);
        }
    }

    void NhatSung(GameObject Gun)
    {
        CoSung = true;
        Destroy(Gun);
    }
}
