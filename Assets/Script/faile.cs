using UnityEngine;
using UnityEngine.SceneManagement;

public class faile : MonoBehaviour
{
    [SerializeField]
    private Object MapHienTai;
    [SerializeField]
    private SpriteRenderer Fauile;
    void Start()
    {
        Fauile.gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Trap") )
        {
            Fauile.gameObject.SetActive (true);
            
            Invoke("ChangeMap", 3f);
        }
    }

    void ChangeMap()
    {
        Time.timeScale = 1;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    void Update()
    {
        
    }
}
