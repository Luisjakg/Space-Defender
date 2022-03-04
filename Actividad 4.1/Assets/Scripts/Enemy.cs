using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private int scoreValue = 150;
    [SerializeField] private float health = 100;
    [SerializeField] private float shotCounter;
    [SerializeField] private float minTimeBetweenShots = 0.3f;
    [SerializeField] private float maxTimeBetweenShots = 3f;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private float durationOfExplosion = 1f;


    [Header("Prefabs")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject explosionVfxPrefab; 
    [SerializeField] private AudioClip deathSound; 
    [SerializeField] [Range(0,1)] private float deathSoundVolume = 0.75f;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] [Range(0,1)]private float shootSoundVolume = 0.75f; 
    // Start is called before the first frame update
    
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();

    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            laserPrefab,
            transform.position,
            Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionVfxPrefab, 
            transform.position,
            transform.rotation);
        Destroy(explosion,durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound,Camera.main.transform.position, deathSoundVolume);
    }
}
