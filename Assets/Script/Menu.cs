using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UIElements;
using UnityEditor.SearchService;
public class Menu : MonoBehaviour
{
    [SerializeField] public Object map1;
    [SerializeField] public Object map2;
    [SerializeField] public Object map3;
    [SerializeField] public Object map4;
    [SerializeField] public Object map5;
    [SerializeField] public Object menu;
    private VisualElement root;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        Button resume = root.Q<Button>("resume");
        resume.clicked += resumee;

        Button MainMenu = root.Q<Button>("Menu");
        MainMenu.clicked += mainmenu;

        Button map1 = root.Q<Button>("map1");
        map1.clicked += Map1;

        Button map2 = root.Q<Button>("map2");
        map2.clicked += Map2;

        Button map3 = root.Q<Button>("map3");
        map3.clicked += Map3;

        Button map4 = root.Q<Button>("map4");
        map4.clicked += Map4;

        Button map5 = root.Q<Button>("map5");
        map5.clicked += Map5;
    }       
    void resumee()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    void mainmenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menu.name);
    }
    void Map1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(map1.name);
    }
    void Map2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(map2.name);
    }
    void Map3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(map3.name);
    }
    void Map4()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(map4.name);
    }
    void Map5()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(map5.name);
    }

    void Update()
    {
        
    }
}
