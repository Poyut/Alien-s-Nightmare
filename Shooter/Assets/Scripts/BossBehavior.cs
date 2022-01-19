using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehavior : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private GameObject EnemyProjectile;
    [SerializeField]
    private AudioClip[] ShootingSound;
    [SerializeField]
    private AudioClip[] HitSound;
    [SerializeField]
    private GameObject Explosion;
    [SerializeField]
    private Animator anim;

    private GameObject CheckpointTarget;
    private bool IsPlaced;
    private int LifePoint;
    private Renderer spRenderer;
    private float ShootInterval;
    private AudioSource m_AudioSource;
    private Rigidbody2D r2;
    private bool Direction;
    private float LastShootTime;
    private bool isDead=false;

    public Transition transition;


    void Start()
    {
        CheckpointTarget = GameObject.FindWithTag("Checkpoint");
        r2 = GetComponent<Rigidbody2D>();
        Direction = (Random.value > 0.5f);
        m_AudioSource = GetComponent<AudioSource>();
        spRenderer = GetComponent<Renderer>();
        LifePoint = GameManager.Instance.Level * 10;
    }

    void Update()
    {
        if (!IsPlaced)
        {
            SetPlace();
        }
        else if (IsPlaced)
        {
            if (Direction)
            {
                r2.velocity = (new Vector2(1, 0) * Speed);
            }
            else if (!Direction)
            {
                r2.velocity = (new Vector2(1, 0) * -Speed);
            }

            if (Time.time - LastShootTime >= ShootInterval)
            {
                LastShootTime = Time.time;
                Shoot();
            }

            if (LifePoint <= 0 && !isDead)
            {
                Die();
            }
        }
    }

    private void Shoot()
    {
        m_AudioSource.PlayOneShot(ShootingSound[Random.Range(0, ShootingSound.Length)]);
        Instantiate(EnemyProjectile, transform.position, Quaternion.Euler(new Vector3(transform.position.x, transform.position.y, transform.position.z + 20)));
        Instantiate(EnemyProjectile, transform.position, Quaternion.Euler(new Vector3(transform.position.x, transform.position.y, transform.position.z + 40)));
        Instantiate(EnemyProjectile, transform.position, Quaternion.identity);
        Instantiate(EnemyProjectile, transform.position, Quaternion.Euler(new Vector3(transform.position.x, transform.position.y, transform.position.z - 20)));
        Instantiate(EnemyProjectile, transform.position, Quaternion.Euler(new Vector3(transform.position.x, transform.position.y, transform.position.z - 40)));
        ShootInterval = Random.Range(0.2f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "LeftCollider")
        {
            Direction = true;
        }
        else if (collision.gameObject.tag == "RightCollider")
        {
            Direction = false;
        }

        if (collision.gameObject.tag == "Projectile" && IsPlaced)
        {
            BeingHit();
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.NumberOfMonsterOnMap--;
    }

    private void BeingHit()
    {
        StartCoroutine(FlashHit());
        m_AudioSource.PlayOneShot(HitSound[Random.Range(0, HitSound.Length)]);
        GameManager.Instance.Score += 10;
        LifePoint--;
    }


    private IEnumerator FlashHit()
    {
        for (int i = 0; i < 2; i++)
        {
            spRenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spRenderer.material.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Die()
    {
        transition.ActivateTransition();
        isDead = true;
        GameManager.Instance.Score += 250;
        GameManager.Instance.Money += 250;
        GameManager.Instance.Level++;
        GameManager.Instance.ActiveBoss = false;
        GameManager.Instance.EnemyKilled = 0;
        Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(this.gameObject);
        SceneManager.LoadScene("Shop");
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    private void SetPlace()
    {
        transform.position = Vector3.MoveTowards(transform.position, CheckpointTarget.transform.position, Speed * Time.deltaTime);
        if (transform.position.y == CheckpointTarget.transform.position.y)
        {
            IsPlaced = true;
        }
    }
}
