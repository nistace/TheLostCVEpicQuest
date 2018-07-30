using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Animator playerAnimator;
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer playerBowingArmsSpriteRenderer;
    private AudioSourceManager audioSourceManager;
    private Collider2D playerCollider;
    private bool onGround = false;
    private bool onLadder = false;
    private bool jumping = false;
    private bool bowing = false;

    public GameObject playerBowingArms;
    public RectTransform screenRectTransform;
    public List<GameObject> walkThroughCollidersList = new List<GameObject>();
    public bool hasBow = false;
    public bool canMove = true;
    public float runSpeed = 5f;
    public float climbSpeed = 5f;
    public Vector2 jumpForce = new Vector2(0, 15);
    public CircleCollider2D groundCheck;
    public GameObject arrowPrefab;


    // Use this for initialization
    void Start() {
        this.playerAnimator = this.GetComponent<Animator>();
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        this.playerSpriteRenderer = this.GetComponent<SpriteRenderer>();
        this.audioSourceManager = this.GetComponent<AudioSourceManager>();
        this.playerCollider = this.GetComponent<Collider2D>();
        foreach (GameObject go in this.walkThroughCollidersList)
            foreach (Collider2D c in go.GetComponentsInChildren<Collider2D>())
                Physics2D.IgnoreCollision(this.playerCollider, c);
        this.playerBowingArmsSpriteRenderer = this.playerBowingArms.GetComponent<SpriteRenderer>();
        this.bowing = false;
        this.playerBowingArms.SetActive(this.bowing);
    }

    // Update is called once per frame
    void Update() {
        this.CheckBowing();
        if (this.canMove) {
            this.CheckHorizontalMovement();
            this.CheckVerticalMovement();
        }
        this.playerAnimator.SetBool("HorizontalMovement", Mathf.Abs(this.playerRigidbody.velocity.x) > .01f);
        this.playerAnimator.SetFloat("VerticalSpeed", this.playerRigidbody.velocity.y);
        this.playerAnimator.SetBool("VerticalMovement", Mathf.Abs(this.playerRigidbody.velocity.y) > .01f);
        this.playerAnimator.SetBool("OnGround", this.onGround);
        this.playerAnimator.SetBool("Jumping", this.jumping);
        this.playerAnimator.SetBool("OnLadder", this.onLadder);
        this.playerAnimator.SetBool("Bowing", this.bowing);
        if (this.onLadder && Mathf.Abs(this.playerRigidbody.velocity.y) < .01f)
            this.playerRigidbody.velocity = new Vector2(this.playerRigidbody.velocity.x, .51f);
    }

    private float GetBowingAngle() {
        return  Vector3.Angle(Input.mousePosition - this.screenRectTransform.position, Vector3.right);
    }

    private void CheckBowing() {
        if (this.hasBow) {
            if (Input.GetAxis("Shoot") > 0 && (this.onGround || this.onLadder)) {
                this.bowing = true;
                this.playerRigidbody.velocity = Vector2.zero;
                float angle = this.GetBowingAngle();
                this.playerSpriteRenderer.flipX = angle > 90;
                this.playerBowingArmsSpriteRenderer.flipX = angle > 90;
                this.playerBowingArms.transform.rotation = this.playerBowingArmsSpriteRenderer.flipX ? Quaternion.Euler(0, 0, 180 +angle) : Quaternion.Euler(0, 0, angle);
            } else if (this.bowing) {
                this.bowing = false;
                float angle = this.GetBowingAngle();
                GameObject arrow = Instantiate(this.arrowPrefab, this.transform.position + Quaternion.Euler(0, 0, angle) * Vector2.right, Quaternion.Euler(0, 0, angle - 90));
                Rigidbody2D arrowRigidbody = arrow.GetComponent<Rigidbody2D>();
                arrowRigidbody.AddForce(Quaternion.Euler(0, 0, angle) * Vector2.right * 1200);
                Destroy(arrow, 5f);
                this.audioSourceManager.PlayAudioClip("bow");
            }
            this.canMove = !this.bowing;
            this.playerBowingArms.SetActive(this.bowing);
        }
    }

    private void CheckVerticalMovement() {
        this.jumping &= this.playerRigidbody.velocity.y > 0;
        if ((this.onGround || this.onLadder) && !this.jumping && Input.GetAxis("Jump") > 0) {
            this.onGround = false;
            this.jumping = true;
            this.audioSourceManager.PlayAudioClip("jump");
            this.playerRigidbody.velocity = new Vector2(this.playerRigidbody.velocity.x, 0);
            this.playerRigidbody.AddForce(this.jumpForce * Input.GetAxis("Jump"));
        } else if (this.onLadder) {
            float climb = Input.GetAxis("Vertical");
            if (!this.jumping)
                this.playerRigidbody.velocity = new Vector2(this.playerRigidbody.velocity.x, 0);
            if (climb != 0) {
                this.playerRigidbody.velocity = new Vector2(this.playerRigidbody.velocity.x, climb * this.climbSpeed);
                this.jumping = false;
            }
        }
    }

    private void CheckHorizontalMovement() {
        float move = Input.GetAxis("Horizontal");
        this.playerRigidbody.velocity = new Vector2(move * this.runSpeed, this.playerRigidbody.velocity.y);
        if (move != 0) {
            this.playerSpriteRenderer.flipX = move < 0;
        }
    }

    protected internal void SetOnGround(bool onGround) {
        this.onGround = onGround;
    }

    protected internal void SetOnLadder(bool onLadder) {
        this.onLadder = onLadder;
    }

}
