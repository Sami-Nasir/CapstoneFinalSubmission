using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedMouse;
    public float speed = 20;
    public float jumpForce=10;
    private float horizontal;
    private float vertical;
    private Rigidbody rigidBody;
    private bool isGround=true;
    private bool atHeight = false;
    private Animator anim;
    private float Speed = 0;
    public float health=3;
    private float mouseX;
    private float angle=180;
    private float Score;
    private CharacterController player;
    public CapsuleCollider col;
    public LayerMask groundLayer;
    public Transform players;
    public TextMeshProUGUI levelCompleted;
    public TextMeshProUGUI Coins;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI enoughCoins;
    public TextMeshProUGUI looser;
    private float timeWhenDisappear;
    private float timeToAppear = 2f;
    private bool isDead = false;
    public AudioSource soundSystem;
    public AudioClip coinSound;
    public AudioClip enemySound;
    public AudioClip jumpSound;
    public AudioClip healthUpSound;
    public AudioClip deadSound;
    public AudioClip coinDoubleSound;
    public AudioClip levelCompleteSound;
    public AudioClip dumpSound;
    public AudioClip bulletSound;
    public AudioClip backgroundMusic;
    public GameObject jumpEffect;
    public GameObject powerUpEffect;
    public GameObject enemyCollisionEffect;
    // Start is called before the first frame update
    void Start()
    {
        soundSystem = GetComponent<AudioSource>();
        levelCompleted.enabled = false;
        rigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetFloat("Blend", 0f);
        player = GetComponent<CharacterController>();
        Score = 0;
        enoughCoins.enabled = false;
        looser.enabled = false;
        soundSystem.Play();
    }
    // Update is called once per frame
    void Update()
    {
        AxisMovement();
        Rotation();
        Jump();
        Coins.SetText("  "+ Score);
        if (health >= 0)
        {
            Health.SetText("  " + health);
        }
        else
        {
            Health.SetText("  " + 0);
        }
        if (enoughCoins.enabled && (Time.time >= timeWhenDisappear))
        {
            enoughCoins.enabled = false;
        }
    }
    void AxisMovement()
    {
        if (!isDead) {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            Vector3 mov = new Vector3(horizontal, 0, vertical).normalized;
            if (horizontal == 0 && vertical == 0)
            {
                anim.SetFloat("Blend", 0.2f);
            }
            transform.Translate(mov * speed * Time.deltaTime);
            Animation();
        }
    }
    private bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundLayer);
    }
    private void HandleRotation()
    {
        if (!isDead)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                angle -= 5f;
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                angle += 5;
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
        }
    }
    private void Rotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(mouseX * speedMouse * Vector3.up * Time.deltaTime, Space.Self);
    }
    void Jump()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
            {
                soundSystem.PlayOneShot(jumpSound);
                anim.SetFloat("Blend", 1.95f);
                Vector3 pos = Vector3.up.normalized;
                rigidBody.AddForce(pos * jumpForce, ForceMode.Impulse);
                isGround = false;
                atHeight = true; Instantiate(jumpEffect, position, jumpEffect.transform.rotation);
            }
        }
    }
    private void Animation()
    {
        if ( Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) )
        {
            anim.SetFloat("Blend", 0.93f);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetFloat("Blend", 0.93f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Score += 1;
            soundSystem.PlayOneShot(coinSound);
        }
        if (other.gameObject.CompareTag("PowerUp"))
        {
            health = 3;
            soundSystem.PlayOneShot(healthUpSound);
            Instantiate(powerUpEffect, transform.position, powerUpEffect.transform.rotation);
        }
        if (other.gameObject.CompareTag("DoubleCoin"))
        {
            Score *= 2;
            soundSystem.PlayOneShot(coinDoubleSound);
            Instantiate(powerUpEffect, transform.position, powerUpEffect.transform.rotation);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            atHeight = false;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 1;
            soundSystem.PlayOneShot(enemySound);
            if (health == 0)
            {
                looser.enabled = true;
                anim.SetBool("isDead", true);
                StartCoroutine(Reload());
                isDead = true;
                soundSystem.PlayOneShot(deadSound);
            }
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
            Instantiate(enemyCollisionEffect, pos, enemyCollisionEffect.transform.rotation);
        }
        if(collision.gameObject.CompareTag("Bullet"))
        {
            health -= 1;
            soundSystem.PlayOneShot(bulletSound);
            if (health == 0)
            {
                looser.enabled = true;
                anim.SetBool("isDead", true);
                StartCoroutine(Reload());
                isDead = true;
                soundSystem.PlayOneShot(deadSound);
            }
        }
        if (collision.gameObject.CompareTag("Winner"))
        {
            if (Score >= 50)
            {
                soundSystem.PlayOneShot(levelCompleteSound);
                levelCompleted.enabled=true;
                Debug.Log("levelCompleted");
                Destroy(collision.gameObject);
                StartCoroutine(NextLevel());
            }
            else
            {
                soundSystem.PlayOneShot(dumpSound);
                EnableText();
                Debug.Log("Not have Enough Coins");
            }
        }
    }
    public void EnableText()
    {
        enoughCoins.enabled = true;
        timeWhenDisappear = Time.time + timeToAppear;
    }
    //We check every frame if the timer has expired and the text should disappear
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(4.0f);
        int number=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(number);
    }
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(4.0f);
        int number = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(number+1);
    }
    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetBool("isShoot", true);
        }
    }
}
