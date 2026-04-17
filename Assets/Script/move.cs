using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    // animation_chung
    [SerializeField]
    public BienDungChung animation_chung;
    [SerializeField]
    public Object MapHienTai;
    //âm thanh
    [SerializeField] private AudioSource nhacnen;
    [SerializeField] private AudioSource AmthanhTong;

    [SerializeField] private AudioClip nhac;
    [SerializeField] private AudioClip nhacFigt;

    [SerializeField] private AudioClip dibo;
    [SerializeField] private AudioClip chay;
    [SerializeField] private AudioClip LuomSung;
    [SerializeField] private AudioClip Nhay;
    [SerializeField] private AudioClip TiengSung;
    [SerializeField] private AudioClip kickSound;
    [SerializeField] private AudioClip Heal;
    [SerializeField] private AudioClip WinGame;
    [SerializeField] private AudioClip Deadsound;


    // các biến khác trong game
    [Header("Speed")]
    [SerializeField]
    public float speed = 5f;

    [SerializeField]
    public float RunSpeed = 10f;

    [Header("Jump")]
    [SerializeField]
    public float jumpForce = 12f;

    [SerializeField]
    public Transform GroundCheck;

    [SerializeField]
    public float GroundRadius = 0.2f;

    [SerializeField]
    public LayerMask GroundLayer;


    //animation
    Animator ani;
    AnimationClip CurrentAni;
    //heath
    [SerializeField] private float MaxMau = 100f;
    [SerializeField] private Slider ThanhMau;
    private float MauHienTai;
    private float ThoiGianDa;
    //rigid
    Rigidbody2D rb;
    //kick
    [SerializeField] private Transform diemda;
    [SerializeField] private float BanKinhcuDa = 0.5f;
    [SerializeField] private float LucDa = 10f;

    bool dead = false;
    bool isGrounded;
    bool hasGun;

    void Start()
    {
        MauHienTai = MaxMau;
        UpdateThanhMau();
        hasGun = false;
        nhacnen.clip = nhac;
        nhacnen.Play();
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    void UpdateThanhMau() 
    {
        ThanhMau.value = MauHienTai / MaxMau;
    }

    void Update()
    {
        if (dead) return;
        
        if(MauHienTai <= 0)
        {
            dead = true;
            StartCoroutine(die());
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            kick();
            ChangeAnimation(animation_chung.kick);
            AmthanhTong.PlayOneShot(kickSound);
            ThoiGianDa = Time.time + 0.3f;
        }
        
        float move = 0;

        if (Input.GetKey(KeyCode.A))
            move = -1;

        if (Input.GetKey(KeyCode.D))
            move = 1;

        float CurrentSpeed = Input.GetKey(KeyCode.LeftShift) ? RunSpeed : speed;

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        rb.linearVelocity = new Vector2(move * CurrentSpeed, rb.linearVelocity.y);

        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, GroundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce);
            AmThanh(Nhay);
        }




        if (!isGrounded)
        {
            DungAmThanh();
            if (rb.linearVelocityY > 0.1f)
            {
                ChangeAnimation(hasGun ? animation_chung.Gun_Nhay : animation_chung.Nhay);
            }
            if (rb.linearVelocityY < -0.25f)
            {

                if (hasGun)
                {
                    ChangeAnimation(animation_chung.Gun_Nhay);
                }
                else ChangeAnimation(animation_chung.Roi);
            }

        }
        else
        {
            if (move != 0)
            {
                if (isSprinting)
                {
                    ChangeAnimation(hasGun ? animation_chung.Gun_Run : animation_chung.Chay);
                    AmThanh(chay, true);
                }
                else
                {
                    ChangeAnimation(hasGun ? animation_chung.Gun_walk : animation_chung.Dibo);
                    AmThanh(dibo, true);
                }

            }
            else
            {
                if (hasGun && !ani.GetCurrentAnimatorStateInfo(0).IsName("shooting"))
                {
                    ChangeAnimation(animation_chung.idel_gun);
                }
                else
                {
                    ChangeAnimation(animation_chung.idle);
 
                }
                DungAmThanh();
            }
        }


        if (move > 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }

    }
    void TangMau(float TangMau)
    {
        MauHienTai += TangMau;
        MauHienTai = Mathf.Clamp(MauHienTai, 0, MaxMau);
        UpdateThanhMau();
    }
    void MatMau(float Damge)
    {
        MauHienTai -= Damge;
        MauHienTai = Mathf.Clamp(MauHienTai, 0, MaxMau);
        UpdateThanhMau();
    }
    void AmThanh(AudioClip clip, bool loop = false)
    {
        if (AmthanhTong == null || clip == null) return;

        if (loop)
        {
            if (AmthanhTong.clip != clip || !AmthanhTong.isPlaying)
            {
                AmthanhTong.clip = clip;
                AmthanhTong.loop = true;
                AmthanhTong.Play();
            }

        }
        else
        {
            AmthanhTong.PlayOneShot(clip);
        }
    }

    void DungAmThanh()
    {
        if (AmthanhTong != null && AmthanhTong.isPlaying)
        {
            if (AmthanhTong.clip == dibo || AmthanhTong.clip == chay)
            {
                AmthanhTong.clip = null;
                AmthanhTong.loop = false;
            }
        }

    }

    void ChangeAnimation(AnimationClip newAni)
    {

        if (newAni == null) return;

        if (Time.time < ThoiGianDa) return;

        if (animation_chung == null) return;

       


        ani.Play(newAni.name);
        CurrentAni = newAni;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gun") && !hasGun)
        {
            hasGun = true;
            if (animation_chung != null && animation_chung.idel_gun != null)
            {
                AmThanh(LuomSung);
                ChangeAnimation(animation_chung.idel_gun);
            }
        }
        if (other.CompareTag("Win"))
        {
            AmthanhTong.PlayOneShot(WinGame);
        }

        if (other.CompareTag("spear"))
        {
            MatMau(5f);
        }

        if (other.CompareTag("TenLua"))
        {
            MatMau(10f);
        }
        if (other.CompareTag("KamiKaze"))
        {
            MatMau(20f);
        }
        if (other.gameObject.CompareTag("Heal"))
        {
            AmThanh(Heal);
            TangMau(10f);
        }
        if (other.CompareTag("KichHoatBoss"))
        {
            nhacnen.clip = nhacFigt;
            nhacnen.Play();
        }
    }
    void kick()
    {
        Collider2D[] XacDinh = Physics2D.OverlapCircleAll(diemda.position,BanKinhcuDa);
        foreach(Collider2D vat in XacDinh)
        {
            Rigidbody2D rbVatThe = vat.GetComponent<Rigidbody2D>();
            if(rbVatThe != null && vat.gameObject != gameObject)
            {
                Vector2 Huongda = (vat.transform.position - transform.position).normalized;

                rbVatThe.AddForce(Huongda * LucDa,ForceMode2D.Impulse);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (diemda != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(diemda.position, BanKinhcuDa);
        }
    }
    IEnumerator die()
    {
        AmthanhTong.PlayOneShot(Deadsound);
        ChangeAnimation(animation_chung.Dead);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(MapHienTai.name);
    }

}