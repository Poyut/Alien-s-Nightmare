using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField]
    private Button BtnBuyLife;
    [SerializeField]
    private Button BtnBuyDamage;
    [SerializeField]
    private Button BtnBuyAmmo;
    [SerializeField]
    private Button BtnResume;
    [SerializeField]
    private int LifePrice;
    [SerializeField]
    private int DamagePrice;
    [SerializeField]
    private int AmmoPrice;
    [SerializeField]
    private GameObject ScoreTxt;
    [SerializeField]
    private GameObject LifeTxt;
    [SerializeField]
    private GameObject DamageTxt;
    [SerializeField]
    private GameObject AmmoTxt;
    [SerializeField]
    private Animator anim;

    private GameManager Joueur;

    void Start()
    {
        BtnBuyLife.onClick.AddListener(BuyLife);
        BtnBuyDamage.onClick.AddListener(BuyDamage);
        BtnBuyAmmo.onClick.AddListener(BuyAmmo);
        BtnResume.onClick.AddListener(Resume);

        Joueur = GameManager.Instance;
    }

    private void Update()
    {
        string Score = ("money : " + Joueur.Money);
        ScoreTxt.GetComponent<Text>().text = Score;

        string Life = (Joueur.PlayerLife + " / " + Joueur.MaxLife);
        LifeTxt.GetComponent<Text>().text = Life;

        string Damage = (Joueur.Damage.ToString());
        DamageTxt.GetComponent<Text>().text = Damage;

        string Ammo = (Joueur.Ammo.ToString());
        AmmoTxt.GetComponent<Text>().text = Ammo;

    }

    private void BuyLife()
    {
        if(Joueur.Money >= LifePrice)
        {
            Joueur.Money -= LifePrice;
            Joueur.MaxLife++;
            Joueur.PlayerLife++;
        }
    }

    private void BuyDamage()
    {
        if(Joueur.Money >= DamagePrice)
        {
            Joueur.Money -= DamagePrice;
            Joueur.Damage++;
        }
    }

    private void BuyAmmo()
    {
        if(Joueur.Money >= AmmoPrice)
        {
            Joueur.Money -= AmmoPrice;
            Joueur.Ammo += 10;
        }
    }

    private void Resume()
    {
        anim.SetTrigger("Fade");
        SceneManager.LoadScene("Game");
    }
}
