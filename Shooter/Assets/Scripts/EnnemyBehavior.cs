using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBehavior : MonoBehaviour
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
    private int NextStepLvl;
    [SerializeField]
    private GameObject Explosion;

    private GameObject CheckpointTarget;
    private int LifePoint;
    private bool IsPlaced;
    private Renderer spRenderer;
    private float ShootInterval;
    private AudioSource m_AudioSource;
    private Rigidbody2D r2;
    private bool Direction;
    private float LastShootTime;


    void Start()
    {
        r2 = GetComponent<Rigidbody2D>();
        m_AudioSource = GetComponent<AudioSource>();
        spRenderer = GetComponent<Renderer>();
        CheckpointTarget = GameObject.FindWithTag("Checkpoint");
        LifePoint = GameManager.Instance.Level+3;

        Direction = (Random.value > 0.5f);

        spRenderer.material.color = Color.grey;

        InvokeRepeating("ChangeDirection", 1f, 3f);

    }

    void Update()
    {
        if(!IsPlaced)
        {
            SetPlace();
        }
        else if(IsPlaced)
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
                if(GameManager.Instance.Level>=NextStepLvl)
                {
                    TripleShoot();
                }
                else
                {
                    Shoot();
                }
            }
        }

        if (LifePoint<=0)
        {
            Die();
        }
    }

    private void Shoot()
    {
        m_AudioSource.PlayOneShot(ShootingSound[Random.Range(0,ShootingSound.Length)]);
        Instantiate(EnemyProjectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        ShootInterval = Random.Range(0.5f, 2f);
    }

    private void TripleShoot()
    {
        m_AudioSource.PlayOneShot(ShootingSound[Random.Range(0, ShootingSound.Length)]);
        Instantiate(EnemyProjectile, transform.position, Quaternion.Euler(new Vector3(transform.position.x, transform.position.y, transform.position.z + 20)));
        Instantiate(EnemyProjectile, transform.position, Quaternion.identity);
        Instantiate(EnemyProjectile, transform.position, Quaternion.Euler(new Vector3(transform.position.x, transform.position.y, transform.position.z - 20)));
        ShootInterval = Random.Range(0.5f, 2f);
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
        m_AudioSource.PlayOneShot(HitSound[Random.Range(0,HitSound.Length)]);
        GameManager.Instance.Score += 10;
        LifePoint -= GameManager.Instance.Damage;
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
        GameManager.Instance.EnemyKilled++;
        GameManager.Instance.Score += 50;
        GameManager.Instance.Money += 100;
        Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void SetPlace()
    {
        transform.position = Vector3.MoveTowards(transform.position, CheckpointTarget.transform.position, Speed * Time.deltaTime);
        if(transform.position.y == CheckpointTarget.transform.position.y)
        {
            IsPlaced = true;
            spRenderer.material.color = Color.white;
        }

    }

    private void ChangeDirection()
    {
        Direction = (Random.value > 0.5f);
    }
}
