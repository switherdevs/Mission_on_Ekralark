using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_game : MonoBehaviour
{
    [SerializeField]
    private Object SceneName;
    [SerializeField]
    private SpriteRenderer Win;

    void Start()
    {
        Win.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && SceneName != null) 
        {

            Win.gameObject.SetActive(true);
            Debug.Log("You win");
            Invoke("ChangMap", 3f);
        }
    }

    void ChangMap()
    {
        SceneManager.LoadScene(SceneName.name);
        Time.timeScale = 1;
    }

    

    void Update()
    {
        
    }
}
