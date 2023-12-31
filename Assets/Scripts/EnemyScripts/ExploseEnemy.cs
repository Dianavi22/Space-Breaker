using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploseEnemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]  private float m_ExploseEnemySpeed = 5f;
    [SerializeField] private Transform _targetPlayer;
    private Vector3 _enemyDir;

    [Header("HP")]
    [SerializeField] private int m_MaxHpEnemy;
    [SerializeField] private int m_CurrentHpEnemy;


    [Header("DamagePlayer")]
    public PlayerHealth _damage;
    public float explosionRadius = 4f;

    private GameManager _gameManager;
   

    [SerializeField] ParticleSystem _explosionParticules;
    [SerializeField] ParticleSystem _explosionFlash;
    [SerializeField] GameObject _gfxSpeedEnemy;
    [SerializeField] Collider _collider;

    [SerializeField] AudioClip _killSpeedEnemy;
    private AudioSource _audioSource;

    private bool _isDestroy = false;

    private void Awake()
    {
        try
        {
            _targetPlayer = FindObjectOfType<Player>().transform;
            _damage = FindObjectOfType<Player>().GetComponent<PlayerHealth>();
            _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
            _audioSource = GetComponent<AudioSource>();
        }
        catch
        {
            return;
        }

    }
    void Start()
    {
        m_CurrentHpEnemy = m_MaxHpEnemy;
    }

    void Update()
    {

        transform.Translate(Vector3.forward * m_ExploseEnemySpeed * Time.deltaTime);

        //transform.position = Vector3.MoveTowards(this.transform.position, _targetPlayer.position, m_ExploseEnemySpeed * Time.deltaTime);
        _enemyDir = Vector3.MoveTowards(this.transform.position, _targetPlayer.position, m_ExploseEnemySpeed * Time.deltaTime);
        if (!_isDestroy)
        {
            gameObject.transform.LookAt(_targetPlayer);

        }

        if (_gameManager.isPhase2 )
        {
            m_ExploseEnemySpeed = 7.5f;
        }
      
    }

    void ExplodeEnemy()
    {
        if (!_gameManager.isPhase2)
        {
            _audioSource.PlayOneShot(_killSpeedEnemy, 0.04f);
        }
        else
        {
            _audioSource.PlayOneShot(_killSpeedEnemy, 0.1f);

        }
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                _gameManager.playerScore = _gameManager.playerScore + 100;
               // Destroy(gameObject);
                if(collider.gameObject.tag == "Enemy")
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }

    void ExplodePlayer()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy" || collider.tag == "Player" && !_gameManager.isSecretEnd)
            {
                    Destroy(gameObject);
                if (collider.tag == "Enemy")
                {
                    Destroy(collider.gameObject);
                    _gameManager.combo++;
                }
                else
                {
                    _damage.TakeExplode();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            PrepareExplode();
            ExplodeEnemy();
            Invoke("DestroySpeedEnemy", 1.5f);
            
        }
        if (collision.collider.CompareTag("Player"))
        {
            PrepareExplode();
            ExplodePlayer();
            Invoke("DestroySpeedEnemy", 1.5f);
        }
        if (collision.collider.CompareTag("Ulti"))
        {
            PrepareExplode();
            ExplodeEnemy();
            Invoke("DestroySpeedEnemy", 1.5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void PrepareExplode()
    {
        _isDestroy = true;
        _explosionParticules.Play();
        _explosionFlash.Play();
        _gfxSpeedEnemy.SetActive(false);
        _collider.enabled = false;
        m_ExploseEnemySpeed = 0;
    }

    private void DestroySpeedEnemy()
    {
        _gameManager.combo++;
        Destroy(gameObject);
        _gameManager.speedEnemyKilled++;
        _gameManager.ultCharge++;
        _gameManager.playerLevelUpgrade = _gameManager.playerLevelUpgrade + 5;
    }
}
