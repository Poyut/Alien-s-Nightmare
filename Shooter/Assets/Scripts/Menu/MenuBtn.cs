using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBtn : MonoBehaviour
{
    [SerializeField]
    private Button PlayBtn;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Image black;
    [SerializeField]
    private Button RetryBtn;

    private void Start()
    {
        PlayBtn.onClick.AddListener(Play);
    }

    private void Play()
    {
        StartCoroutine(FadeAndLoad());
    }
    private IEnumerator FadeAndLoad()
    {
        anim.SetTrigger("Fade");
        yield return new WaitForSeconds(1f); ;
        SceneManager.LoadScene("Game");

    }
    void FixedUpdate()
    {
        
    }
}
