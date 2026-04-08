using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // animation_chung
    [SerializeField]
    public BienDungChung animation_chung;

    //âm thanh
    [SerializeField] private AudioSource nhacnen;
    [SerializeField] private AudioSource AmthanhTong;
    [SerializeField] private AudioClip nhac;
    [SerializeField] private AudioClip dibo;
    [SerializeField] private AudioClip chay;
    [SerializeField] private AudioClip LuomSung;
    [SerializeField] private AudioClip Nhay;
    [SerializeField] private AudioClip TiengSung;

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

    private float khoangCach_tiengsung = 0;

    //animation
    Animator ani;
    AnimationClip CurrentAni;

    //rigid
    Rigidbody2D rb;
    bool isGrounded;
    bool hasGun;

    void Start()
    {
        hasGun = false;
        nhacnen.clip = nhac;
        nhacnen.Play();
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {

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


        if (hasGun && Input.GetMouseButtonDown(0) && Time.time >= khoangCach_tiengsung)
        {
            AmThanh(TiengSung);
            khoangCach_tiengsung = Time.time + 0.5f;
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


    }

}