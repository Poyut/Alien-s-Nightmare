using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private GameObject Projectile;
    [SerializeField]
    private AudioClip[] ShootingSound;
    [SerializeField]
    private AudioClip HealingSound;
    [SerializeField]
    private AudioClip ReloadSound;
    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private GameObject Explosion;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private AudioSource SoundBox;

    private bool isDead=false;
    private Renderer spRender;
    private Collider2D collider;
    private int Weapon=1;
    private Rigidbody2D r2;
    private AudioSource m_AudioSource;
    private bool CanShoot=true;
    private float lastShootTime ;

    private float shootDelay = 0.2f;

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Awake()
    {
        //GameManager.Instance.PlayerLife = 3;
        r2 = GetComponent<Rigidbody2D>();
        m_AudioSource = GetComponent<AudioSource>();
        spRender = GetComponent<Renderer>();
        collider = GetComponent<Collider2D>();
    }


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        r2.velocity = (movement * Speed);

        if(GameManager.Instance.PlayerLife<=0 && !isDead)
        {
            Die();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (GameManager.Instance.Magazine <= 0)
        {
            //CanShoot = false;
            StartCoroutine(Reload());
        }


        if (Input.GetKey(KeyCode.Space) && CanShoot && Time.time >= lastShootTime + shootDelay)
        {
            lastShootTime = Time.time;
            switch (Weapon)
            {
                case 1:
                    ShootWeapon1();
                    break;
                case 2:
                    ShootWeapon2();
                    break;
            }

            GameManager.Instance.AmmoUsed++;
        }

        if (Input.GetButtonDown("Fire2") && GameManager.Instance.Magazine < GameManager.Instance.Ammo)
        {
            StartCoroutine(Reload());
        }
    }

    private void ShootWeapon1()
    {
        GameManager.Instance.Magazine--;
        m_AudioSource.PlayOneShot(ShootingSound[Random.Range(0, ShootingSound.Length)]);
        Instantiate(Projectile, transform.position,Quaternion.identity);
    }
    private void ShootWeapon2()
    {
        GameManager.Instance.Magazine--;
        m_AudioSource.PlayOneShot(ShootingSound[Random.Range(0, ShootingSound.Length)]);
        Instantiate(Projectile, transform.position, Quaternion.Euler(new Vector3(transform.position.x,transform.position.y,transform.position.z + 20)));
        Instantiate(Projectile, transform.position, Quaternion.identity);
        Instantiate(Projectile, transform.position, Quaternion.Euler(new Vector3(transform.position.x, transform.position.y, transform.position.z - 20)));
    }
    private void Bomb()
    {
        for(int i=0;i<360;i += 10)
            Instantiate(Projectile, transform.position, Quaternion.Euler(new Vector3(transform.position.x, transform.position.y, transform.position.z + i)));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy_Projectile" && GameManager.Instance.PlayerLife > 0)
        {
            GameManager.Instance.PlayerLife--;
            if(GameManager.Instance.PlayerLife >= 1)
                StartCoroutine(Flash());

        }
        if(collision.gameObject.tag == "Item")
        {
            ItemsChoice(collision.gameObject.name);
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator Reload()
    {
        CanShoot = false;
        UI.GetComponent<UI>().Reload();
        GameManager.Instance.Magazine = GameManager.Instance.Ammo;
        m_AudioSource.PlayOneShot(ReloadSound);
        if (Weapon == 2)
            Weapon = 1;

        yield return new WaitForSeconds(3);
        CanShoot = true;
    }

    private IEnumerator Flash()
    {
        for (int i = 0; i < 2; i++)
        {
            spRender.enabled = false;
            collider.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spRender.enabled = true;
            yield return new WaitForSeconds(0.1f);
            spRender.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spRender.enabled = true;
            yield return new WaitForSeconds(0.1f);
            spRender.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spRender.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        collider.enabled = true;
    }

    private void ItemsChoice(string t_ItemName)
    {
        switch (t_ItemName)
        {
            case "Heart(Clone)":
                Heal();
                break;
            case "Weapon2(Clone)":
                ChangeWeapon();
                break;
            case "Bomb(Clone)":
                Bomb();
                break;
            case "Shop":
                SceneManager.LoadScene("Shop");
                break;

        }
    }

    private void Heal()
    {
        m_AudioSource.PlayOneShot(HealingSound);
        GameManager.Instance.Score += 50;

        if(GameManager.Instance.PlayerLife < GameManager.Instance.MaxLife)
            GameManager.Instance.PlayerLife++;
    }
    private void ChangeWeapon()
    {
        Weapon = 2;
        GameManager.Instance.Magazine = GameManager.Instance.Ammo;
    }

    private void Die()
    {
        spRender.enabled = false;
        isDead = true;
        Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        StartCoroutine(EndSceneLoad());
    }

    private IEnumerator EndSceneLoad()
    {
        anim.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EndMenu");
        //Destroy(this.gameObject);
    }

    private void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GetComponent<AudioSource>().Pause();
        SoundBox.Pause();
        CanShoot = false;
        GameIsPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GetComponent<AudioSource>().Play();
        SoundBox.Play();
        CanShoot = true;
        GameIsPaused = false;
    }
}
