using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private PlayerDataSO data;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private CoinManager coinManager;

    AudioManager audioManager;

    private Rigidbody2D rb;
    private int jumpCount;
    private int maxJumps = 1;
    private bool isGrounded;
    private bool takingDamage;
    private bool m_FacingRight = true;
    private float currjumpForce = 10f;
    private bool bisAttacking;
    private int health;

    public bool attackCondition;
    public bool isDead;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        health = data.maxHealth;
        currjumpForce = data.maxJumpForce;
        healthbar.UpdateHealthBar(data.maxHealth, health);
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!isDead)
        {
            if (!bisAttacking)
            {
                // Movement and cast a raycast to check if grounded
                Movement();
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, data.lengthRayCast, data.layerMask);
                isGrounded = hit.collider != null;

                if (isGrounded)
                {
                    // If is grounded reset jump count
                    jumpCount = 0;
                }
                if (Input.GetKeyDown(data.jumpKey) && !takingDamage && jumpCount < maxJumps)
                {
                    // If jump key is pressed and player is not taking damage and jump count is less than max jumps, jump
                    audioManager.PlaySFX(audioManager.jumpSfx);
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                    rb.AddForce(new Vector2(0f, currjumpForce), ForceMode2D.Impulse);
                    jumpCount++;
                }
            }
            // Setting Attack condition
            bool condition = !bisAttacking && isGrounded;
            attackCondition = condition;
            // Atacar
            if (Input.GetKeyDown(data.attackKey) && attackCondition)
            {
                audioManager.PlaySFX(audioManager.ShootSfx);
                Attacking();
            }
        }
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("takingDamage", takingDamage);
        animator.SetBool("attacking", bisAttacking);
    }
    public void Movement()
    {
        float velocityX = Input.GetAxis("Horizontal") * Time.deltaTime * data.velocity;

        animator.SetFloat("Movement", velocityX * data.velocity);

        if (velocityX < 0 && m_FacingRight)
        {
            Flip();
        }
        if (velocityX > 0 && !m_FacingRight)
        {
            Flip();
        }

        Vector3 position = transform.position;

        if (!takingDamage)
            transform.position = new Vector3(velocityX + position.x, position.y, position.z);
    }
    public void TakingDamage(Vector2 direction, int damageAmount)
    {
        if (!takingDamage)
        {
            takingDamage = true;
            health -= damageAmount;
            healthbar.UpdateHealthBar(data.maxHealth, health);
            if (health <= 0)
            {
                audioManager.Stop();
                audioManager.PlaySFX(audioManager.LooseSfx);
                deathPanel.SetActive(true);
                animator.SetBool("isDead", isDead);
                isDead = true;
                Time.timeScale = 0;
            }
            if (!isDead)
            {
                Vector2 rebound = new Vector2(transform.position.x - direction.x, 0.5f).normalized;
                rb.AddForce(rebound * data.reboundForce, ForceMode2D.Impulse);
            }
        }   
    }
    public void DeactiveDamage()
    {
        takingDamage = false;
        rb.velocity = Vector2.zero;
    }
    public void Attacking()
    {
        bisAttacking = true;
    }

    public void DeactiveAttack()
    {
        bisAttacking = false;
        animator.SetBool("attacking", bisAttacking = false);
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int coinLayer = LayerMask.NameToLayer("Coin");

        if (other.gameObject.layer == coinLayer)
        {
            Destroy(other.gameObject);
            audioManager.PlaySFX(audioManager.coinsSfx);
            coinManager.coinCount++;
        }

        int DeathLayer = LayerMask.NameToLayer("DeathZone");
        if (other.gameObject.layer == DeathLayer)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }
    public void StartJumpBoost()
    {
        StartCoroutine(JumpBoost());
    }

    public void Addhealth(int healthToAdd)
    {
        health += healthToAdd;
        healthbar.UpdateHealthBar(data.maxHealth, health);
        if (health > data.maxHealth)
        {
            health = data.maxHealth;
            healthbar.UpdateHealthBar(data.maxHealth, health);
        }
    }
    private IEnumerator JumpBoost()
    {
        currjumpForce = currjumpForce + data.jumpBoostForce;
        yield return new WaitForSeconds(data.jumpBoostTime);
        currjumpForce = data.maxJumpForce;
    }
}
