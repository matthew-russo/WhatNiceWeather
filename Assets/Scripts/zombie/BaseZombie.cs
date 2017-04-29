using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseZombie : MonoBehaviour {

    protected NavMeshAgent _navMeshAgent;
    protected Animator _animator;
    protected GameObject _player;
    protected PlayerHealth _playerHealth;
    protected GameObject _gun;

    protected List<AudioClip> _zombieGroans = new List<AudioClip>();
    protected List<AudioClip> _playerOuchs = new List<AudioClip>();

    public float health;
    protected float speed;

    public bool isDying;

    protected virtual void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _gun = GameObject.FindGameObjectWithTag("currentGun");
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        foreach (AudioClip a in Resources.LoadAll("Sounds/ZombieGroans/", typeof(AudioClip)))
        {
            _zombieGroans.Add(a);
        }
        foreach (AudioClip a in Resources.LoadAll("Sounds/ouch/", typeof(AudioClip)))
        {
            _playerOuchs.Add(a);
        }
    }

    protected void Update()
    {
        if (isDying)
        {
            return;
        }
        _navMeshAgent.destination = _player.transform.position;
        if (_navMeshAgent.remainingDistance < 5f)
        {
            _animator.SetInteger("MoveTowardsPlayer", Random.Range(1, 5));
            _animator.SetInteger("Attack", 0);
            _animator.SetInteger("Attack", Random.Range(1,3));
        }
        else
        {
            _animator.SetInteger("Attack", 0);
            _animator.SetInteger("MoveTowardsPlayer", Random.Range(1,5));
        }
        if (health <= 0)
        {
            Die();
        }
    }

    protected void OnCollisionEnter(Collision col)
    {
        if (isDying)
        {
            return;
        }
        if (col.gameObject.tag == "bullet")
        {
            TakeDamage(_gun.GetComponentInChildren<GunBase>().damagePerHit);
        }
        if (col.gameObject.tag == "Player")
        {
            //if (!_playerHealth.invincibilityPeriod)
            //{
                _playerHealth.currentHealth += 1f;
                AudioManager.Instance.PlayOneShot(_playerOuchs[Random.Range(0, _playerOuchs.Count)], .2f);
            //}
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDying)
        {
            return;
        }
        _animator.ResetTrigger("TakeDamage");
        _animator.SetTrigger("TakeDamage");
        AudioManager.Instance.PlayOneShot(_zombieGroans[Random.Range(0,_zombieGroans.Count)], .1f);
        health -= damage;
    }

    protected void Die()
    {
        _animator.SetInteger("Die", Random.Range(1,4));
        Destroy(_navMeshAgent);
        Destroy(GetComponent<Collider>());
        StartCoroutine(DestroyBody(10f));
        GameManager.Instance.killCount++;
    }

    protected IEnumerator DestroyBody(float timer)
    {
        isDying = true;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
