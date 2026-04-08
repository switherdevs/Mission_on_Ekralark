using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class DoController : MonoBehaviour , IPointerDownHandler , IPointerUpHandler
{
    private SpriteRenderer SpriteRenderer;
    private AudioSource amthanh;

    [SerializeField]
    private Sprite click;

    [SerializeField]
    private Sprite unclick;

    [SerializeField]
    private AudioClip play;

    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        amthanh = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Object clicked:"+gameObject.name);
        SpriteRenderer.sprite = click;
        amthanh.PlayOneShot(play);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        SpriteRenderer.sprite = unclick;
    }


}
