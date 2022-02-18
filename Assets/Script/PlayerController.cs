using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using Quaternion = System.Numerics.Quaternion;

public class PlayerController : MonoBehaviour
{
    private float mySpeedX;
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] float arrowSpeed; 
    [SerializeField] GameObject arrow;
    [SerializeField] bool attacked;
    [SerializeField] float currentAttackTimer;
    [SerializeField] float defaultAttackTimer;
    [SerializeField] int maxArrow;
    [SerializeField] Text MaxArrowValue;
    [SerializeField] AudioClip dieSource;
    [SerializeField] GameObject winPanel, losePanel;
    private bool win;
    private Vector3 defaultLocalScale;
    private Rigidbody2D myBody;
    public bool onGround;
    private bool canDoubleJump;
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        defaultLocalScale = transform.localScale;
        myAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mySpeedX = Input.GetAxis("Horizontal");
        
        HaraketEtme();
        DogruYon();
        Ziplama();
        OkAtma();
        myAnimator.SetFloat("mySpeed",Mathf.Abs(mySpeedX));
        OkSayisiniEkranaYazdir();
    }
    public void DogruYon()
    {
        if (mySpeedX > 0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y);
        }
        else if (mySpeedX < 0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y);
        }
    }

    public void HaraketEtme()
    {
        myBody.velocity = new Vector2(mySpeedX * speed, myBody.velocity.y);
    }

    public void Ziplama()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (onGround == true)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                canDoubleJump = true;
                myAnimator.SetTrigger("onGround");
            }
            else
            {
                if (canDoubleJump == true)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                    canDoubleJump = false;
                    myAnimator.SetTrigger("onGround");
                }
            }
        }
    }

    public void OkHaraket()
    {
        GameObject ok = Instantiate(arrow, transform.position, UnityEngine.Quaternion.identity);
        ok.transform.parent = GameObject.Find("Arrows").transform;
        if (transform.localScale.x > 0)
        {
            ok.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed * 5f, 0f);
        }
        else
        {
            ok.transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y);
            ok.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed * -5f, 0f);
        }
    }
    public void OkAtma()
    {
        if (attacked == false)
        {
            if (Input.GetMouseButtonDown(0) && maxArrow > 0)
            {
                myAnimator.SetTrigger("Attacked");
                attacked = true;
                Invoke("OkHaraket", 0.3f);
                maxArrow--;
            }
        }
        if (attacked == true)
        {
            currentAttackTimer -= Time.deltaTime;
        }
        else
        {
            currentAttackTimer = defaultAttackTimer;
        }
        if (currentAttackTimer <= 0)
        {
            attacked = false;
        }
    }

    public void Die()
    {
        myAnimator.SetTrigger("Die");
        myAnimator.SetFloat("mySpeed",0);
        myBody.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
        GameObject.Find("SoundController").GetComponent<AudioSource>().clip = null;
        GameObject.Find("SoundController").GetComponent<AudioSource>().PlayOneShot(dieSource);
        GetComponent<TimeController>().enabled = false;
        StartCoroutine(Wait(false));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("Trigger"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            GetComponent<TimeController>().enabled = false;
            StartCoroutine(Wait(true));
        }
    }

    public void OkSayisiniEkranaYazdir()
    {
        MaxArrowValue.text = maxArrow.ToString();
    }

    IEnumerator Wait(bool win)
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0;

        if (win == true)
        {
            winPanel.SetActive(true);
        }
        else if (win == false)
        {
            losePanel.SetActive(true);
        }
    }
}
