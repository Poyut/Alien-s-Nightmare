using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int NumberOfMonsterOnMap;
    public int Score;
    public int Magazine;
    public int PlayerLife;
    public int AmmoUsed;
    public bool ActiveBoss;
    public int Level;
    public int EnemyKilled;
    public int MaxLife;
    public int Damage;
    public int Ammo;
    public int Money;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Score = 0;
        NumberOfMonsterOnMap = 0;
        PlayerLife = 3;
        ActiveBoss = false;
        NumberOfMonsterOnMap = 0;
        Level = 1;
        MaxLife = 3;
        Damage = 1;
        Ammo = 30;
    }

    public void Restart()
    {
        Score = 0;
        NumberOfMonsterOnMap = 0;
        PlayerLife = 3;
        ActiveBoss = false;
        NumberOfMonsterOnMap = 0;
        Level = 1;
        MaxLife = 3;
        Damage = 1;
        Ammo = 30;
        Magazine = Ammo;
    }


}
