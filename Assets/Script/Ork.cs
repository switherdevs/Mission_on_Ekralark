using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using System.ComponentModel.Design;
using NUnit.Framework.Constraints;
public class Ork : MonoBehaviour
{

    [SerializeField] private float move = 2f;
    [SerializeField] private float StopTime = 1f;
    [SerializeField] private Transform checkTuong;
    [SerializeField] private LayerMask checkLayer;
    // tan cong
    [SerializeField] private Transform FirePoit;
    [SerializeField] private GameObject PreFabDan;
    [SerializeField] private float TocDoBan = 1f;
    [SerializeField] private float CoolDown = 1f;
    [SerializeField] private LayerMask playerLayer;
    //kiemtra
    private Rigidbody2D rb;
    private bool MatPhai = true;
    private bool Dangdung = false;
    private float LuuThoiGianBan = 0f;
    //Animation
    private Animator clipBienAnimation;

    [SerializeField] private AnimationClip idle;
    [SerializeField] private AnimationClip DiBo;
    [SerializeField] private AnimationClip Dead;
    //amthanh
    [SerializeField] private AudioSource Amthanhtong;
    [SerializeField] private AudioClip Chet;
    [SerializeField] private AudioClip TenLua;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        clipBienAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        if (Dangdung) return;

        bool isPlayer_TrongTam = KiemPlayer();

        if (isPlayer_TrongTam)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            StopAnimation();
            if (Time.time >= LuuThoiGianBan)
            {
                PreFabTenLua();
                StopAnimation();
                LuuThoiGianBan = Time.time + CoolDown;
            }
        }
        else
        {
            tuantra();
        }

    }
    void tuantra()
    {
        bool HitWall = Physics2D.OverlapCircle(checkTuong.position, 0.1f, checkLayer);

        if (HitWall)
        {
            if (!Dangdung)
            {
                StartCoroutine(WaitAndClip());
            }
            return;
        }

        Animationplay(DiBo);
        float Huong = MatPhai ? 1 : -1;
        rb.linearVelocity = new Vector2(move * Huong, rb.linearVelocity.y);

    }
    IEnumerator WaitAndClip()
    {
        Dangdung = true;
        rb.linearVelocity = Vector2.zero;

        Animationplay(idle);

        yield return new WaitForSeconds(StopTime);

        MatPhai = !MatPhai;
        transform.localScale = new Vector3(MatPhai ? 1 : -1, 1, 1);

        Dangdung = false;
    }

    bool KiemPlayer()
    {
        Vector2 Direction = MatPhai ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(FirePoit.position, Direction, TocDoBan, playerLayer);
        Debug.DrawRay(FirePoit.position, Direction * TocDoBan, Color.red);
        return hit.collider != null;
    }

    void PreFabTenLua()
    {
        if (PreFabDan != null)
        {
            GameObject VienDan = Instantiate(PreFabDan, FirePoit.position, FirePoit.rotation);
            Amthanhtong.PlayOneShot(TenLua);
                if (MatPhai)
            {
                VienDan.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                VienDan.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }
    }
    void Animationplay(AnimationClip animation)
    {
        if (animation == null || clipBienAnimation == null) return;
       clipBienAnimation.speed = 1; 
       clipBienAnimation.Play(animation.name);   

    }
    void StopAnimation()
    {
        if (clipBienAnimation != null ) 
        clipBienAnimation.speed = 0;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dan"))
        {
            StopAnimation();
            Amthanhtong.PlayOneShot(Chet);
            Animationplay(Dead);
            Destroy(gameObject, 0.6f);
            Destroy(collision.gameObject);

        }
    }
}
