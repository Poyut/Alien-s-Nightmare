using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Button resumeBtn;
    [SerializeField]
    private Button quitBtn;

    public Player player;
    void Start()
    {
        resumeBtn.onClick.AddListener(ResumeBtn);
        quitBtn.onClick.AddListener(QuitBtn);
    }

    void Update()
    {
        
    }

    private void ResumeBtn()
    {
        player.ResumeGame();
    }

    private void QuitBtn()
    {
        Application.Quit();
    }
}
