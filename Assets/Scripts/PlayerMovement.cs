using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

  public float speed;
  public float jumpSpeed;
  public Animator anim;

  private int jumps;
  private Rigidbody2D rb;
  private BoxCollider2D boxCollider2d;

  [SerializeField] private LayerMask layerMask;

  // Start is called before the first frame update
  void Start(){
      rb = GetComponent<Rigidbody2D>();
      boxCollider2d = GetComponent<BoxCollider2D>();

      jumps = 1;
  }

  void OnTriggerEnter2D(Collider2D other){
    if(other.gameObject.tag == "BluePortal"){
      gameObject.layer = 8;
      layerMask = (1 << 10) | 1;
      anim.SetInteger("Colour", 1);
      anim.SetTrigger("ColourChange");
    } else if(other.gameObject.tag == "PinkPortal"){
      gameObject.layer = 9;
      layerMask = (1 << 11) | 1;
      anim.SetInteger("Colour", 0);
      anim.SetTrigger("ColourChange");
    }
  }

  private void Movement(){
    if (Input.GetJoystickNames()[0] != ""){
      float horizontalInput = Input.GetAxis("Horizontal");

      anim.SetFloat("Walking", speed * horizontalInput);
      rb.velocity = new Vector2(speed * horizontalInput, rb.velocity.y);

    } else if(Input.GetKey(KeyCode.A)){
      anim.SetFloat("Walking", speed);
      rb.velocity = new Vector2(-speed, rb.velocity.y);

    } else{
      if(Input.GetKey(KeyCode.D)){
        anim.SetFloat("Walking", speed);
        rb.velocity = new Vector2(speed, rb.velocity.y);

      } else{
        anim.SetFloat("Walking", 0);
        rb.velocity = new Vector2(0, rb.velocity.y);
      }
    }


  }

  void isGrounded(){
    RaycastHit2D boxCastResults;
    boxCastResults = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, boxCollider2d.bounds.extents.y, layerMask);

    if(boxCastResults.collider != null){
      jumps = 1;
      anim.SetBool("OnGround", true);
    } else {
      anim.SetBool("OnGround", false);
      anim.SetInteger("Jump", 0);
    }
  }

  // Update is called once per frame
  void Update(){
    isGrounded();

    if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))&& jumps > 0){
      anim.SetBool("OnGround", false);
      anim.SetInteger("Jump", 1);
      rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
      jumps--;
    }


    Movement();
    anim.SetFloat("VerticalVelocity", rb.velocity.y);

  }
}
