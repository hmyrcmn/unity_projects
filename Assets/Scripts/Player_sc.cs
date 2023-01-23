using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_sc : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private int score = 0;

    [SerializeField]
    private float speed = 3.5f;

    private float speedMultiplier = 2;

    private float bonusDuration = 5.0f;

    private bool isTripleShotActive = false;
    //private bool isSpeedBonusActive = false;
    private bool isShieldBonusActive = false;

    [SerializeField]
    private GameObject rightEngine, leftEngine;

    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    private GameObject tripleShotPrefab;

    [SerializeField]
    private GameObject shieldVisualizer;

    [SerializeField]
    private float fireRate = 0.5f;
    [SerializeField]
    private float nextFire = 0f;

    [SerializeField]
    private AudioClip laserSoundClip;

    private AudioSource audioSource;

    private SpawnManager_sc sm_sc;

    private UIManager_sc uiManager_sc;

    // Start is called before the first frame update
    void Start()
    {
        sm_sc = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager_sc>();
        uiManager_sc = GameObject.Find("Canvas").GetComponent<UIManager_sc>();
        audioSource = GetComponent<AudioSource>();

        if (sm_sc == null) {
            Debug.Log("Spawn Manager Script is NULL!!");
        }

        if (uiManager_sc == null) {
            Debug.Log("UI Manager Script is NULL!!");
        }

        if (audioSource == null) {
            Debug.Log("Audio Source is NULL!");
        } else {
            audioSource.clip = laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            FireLaser();
        }
    }

    public void TripleShotActive() {
        isTripleShotActive = true;
        StartCoroutine( TripleShotBonusDisableRoutine() );
    }
 
    public void SpeedBonusActive() {
        //isSpeedBonusActive = true;
        speed *= speedMultiplier;
        StartCoroutine( SpeedBonusDisableRoutine() );
    }

    public void ShieldBonusActive() {
        isShieldBonusActive = true;
        shieldVisualizer.SetActive(true);
        StartCoroutine( ShieldBonusDisableRoutine() );
    }

    IEnumerator TripleShotBonusDisableRoutine() {
        yield return new WaitForSeconds(bonusDuration);
        isTripleShotActive = false;
    }

    IEnumerator SpeedBonusDisableRoutine() {
        yield return new WaitForSeconds(bonusDuration);
        //isSpeedBonusActive = false;
        speed /= speedMultiplier;
    }

    IEnumerator ShieldBonusDisableRoutine() {
        yield return new WaitForSeconds(bonusDuration);
        isShieldBonusActive = false;
        shieldVisualizer.SetActive(false);
    }

    public void Damage() {

        if (isShieldBonusActive != true) {
            --lives;
            uiManager_sc.UpdateLives(lives);
            if (lives == 2) {
                rightEngine.SetActive(true);
            } else if (lives == 1) {
                leftEngine.SetActive(true);
            }
        } else {
            isShieldBonusActive = false;
            shieldVisualizer.SetActive(false);
            return;
        }
        
        if (lives < 1) {
            sm_sc.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void AddScore(int points) {
        score += points;
        uiManager_sc.UpdateScore(score);
    }

    void FireLaser() {
        if (isTripleShotActive == true) {
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        } else {
            Instantiate(laserPrefab, transform.position+new Vector3(0, 1.35f, 0), Quaternion.identity);
        }
        audioSource.Play();
    }

    void calculateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(Time.deltaTime * speed * direction);

        //Set Y position
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0) , 0);
        //Set X position
        if (transform.position.x >= 11.3f) {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        } else if (transform.position.x <= -11.3f) {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
}
