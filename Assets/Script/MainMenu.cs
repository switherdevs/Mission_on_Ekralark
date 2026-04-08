using System.Xml;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class MainMenu : MonoBehaviour
{
    [SerializeField] public Object map1;
    [SerializeField] public GameObject Map;
    private VisualElement root;

    void Start()
    {
        gameObject.SetActive(true);
        Map.SetActive(false);
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
        SceneManager.LoadScene(map1.name);
    }

    void Update()
    {

    }
}
