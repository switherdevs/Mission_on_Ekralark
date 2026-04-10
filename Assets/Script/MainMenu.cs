using System.Xml;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Video;
public class MainMenu : MonoBehaviour
{
    [SerializeField] public Object map1;
    [SerializeField] public GameObject Map;
    [SerializeField] public VideoPlayer videolayer;
    [SerializeField] public GameObject obVideo;

    private VisualElement root;

    void Start()
    {
        gameObject.SetActive(true);
        Map.SetActive(false);
        obVideo.SetActive(false);
        if(videolayer != null)
        {
            videolayer.loopPointReached += (vp) =>
            {
                SceneManager.LoadScene(map1.name);
            };
        }
    }

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        Button startgame = root.Q<Button>("start");
        startgame.clicked += start;

        Button map = root.Q<Button>("Map");
        map.clicked += menuMap;
    }

    void menuMap()
    {
        Time.timeScale = 1f;
        Map.SetActive(true);
    }
    void start()
    {
        Time.timeScale = 1f;
        if(videolayer != null && obVideo != null)
        {
            root.style.display = DisplayStyle.None;
            obVideo.SetActive(true);
            videolayer.Play();
        }
        else
        {
            SceneManager.LoadScene(map1.name);
        }

    }

    void Update()
    {

    }
}
