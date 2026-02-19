using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject scene;
    public SceneAsset _sceneAsset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenScene()
    {
        print("OpenScene");
        SceneManager.LoadScene(_sceneAsset.name);
    }
}
