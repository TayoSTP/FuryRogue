using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class MainMenu : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.Playsound(AudioType.Menu, AudioSourceType.Game); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
    }
    public void OpenScene()
    {
        AudioManager.Instance.Playsound(AudioType.Button, AudioSourceType.Player);
        print("OpenScene");
        StartCoroutine(LoadingScene());
    }

    IEnumerator LoadingScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("BuildScene_final");
    }
}
