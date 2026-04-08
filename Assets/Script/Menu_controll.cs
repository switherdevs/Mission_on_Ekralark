using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Menu_controll : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    void Start()
    {
        Menu.SetActive(false);
    }

    public void OnClick()
    {
        Menu.SetActive(true);
        Time.timeScale = 0f;
    }
}
