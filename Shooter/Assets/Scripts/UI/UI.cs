using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject ScoreTxt;
    [SerializeField]
    private GameObject MagazineTxt;
    [SerializeField]
    private GameObject PlayerLifeTxt;
    [SerializeField]
    private GameObject ReloadAnim;

    private bool isCreated;

    // Start is called before the first frame update
    void Start()
    {
        DisplayUI();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DisplayUI();
    }

    private void DisplayUI()
    {
        string Score = ("Score : " + GameManager.Instance.Score);
        ScoreTxt.GetComponent<UnityEngine.UI.Text>().text = Score;

        string Magazine = ("Ammo : " + GameManager.Instance.Magazine);
        MagazineTxt.GetComponent<UnityEngine.UI.Text>().text = Magazine;

        string PlayerLife = ("Vie : " + GameManager.Instance.PlayerLife + " / " + GameManager.Instance.MaxLife);
        PlayerLifeTxt.GetComponent<UnityEngine.UI.Text>().text = PlayerLife;
    }

    public void Reload()
    {
        Instantiate(ReloadAnim,transform);
    }
}
