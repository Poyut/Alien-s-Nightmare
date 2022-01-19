using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField]
    private Button RetryBtn;
    [SerializeField]
    private GameObject AmmoUsedTxt;
    [SerializeField]
    private GameObject ScoreTxt;
    [SerializeField]
    private Animator anim;

    private void Start()
    {
        RetryBtn.onClick.AddListener(Play);
        DisplayUI();
        GameManager.Instance.Restart();
    }

    private void Play()
    {
        StartCoroutine(CoroutineRetry());
    }

    private IEnumerator CoroutineRetry()
    {
        anim.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void DisplayUI()
    {
        string Score = ("Score : " + GameManager.Instance.Score);
        ScoreTxt.GetComponent<UnityEngine.UI.Text>().text = Score;
        string AmmoUsed = ("Ammo used : " + GameManager.Instance.AmmoUsed);
        AmmoUsedTxt.GetComponent<UnityEngine.UI.Text>().text = AmmoUsed;
    }
}
