using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Animator PlayerAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
        PlayerAnim.SetBool("GameStart", true);
        PlayerAnim.applyRootMotion = false;
        Menu.SetActive(false);
    }
    
    public void RestartLevel()
    {
        string LevelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(LevelName);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ShowWinScreen()
    {
        WinScreen.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        LoseScreen.SetActive(true);
    }
}
