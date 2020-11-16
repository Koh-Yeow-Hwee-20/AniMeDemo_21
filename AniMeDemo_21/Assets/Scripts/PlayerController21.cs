using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController21 : MonoBehaviour
{
    float speed = 5.0f;
    float jumpForce = 5.0f;

    public Rigidbody playerRb;
    public Animator playerAnim;
    bool isOnPlayPlane = true;
    bool isDeath = false;
    int Trackdeath = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Make ParentCube not moved when in DeathState.
        if (isDeath == false)
        {
            //Moving and Facing Forward
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                playerAnim.SetBool("IsMove", true);
            }

            //Moving and Facing Backwards
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                playerAnim.SetBool("IsMove", true);
            }

            //IdleState
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                playerAnim.SetBool("IsMove", false);
            }

            //Moving and Facing Left
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                transform.rotation = Quaternion.Euler(0, -90, 0);
                playerAnim.SetBool("IsMove", true);
            }

            //Moving and Facing Right
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                playerAnim.SetBool("IsMove", true);
            }

            //IdleState
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                playerAnim.SetBool("IsMove", false);
            }

            //Make ParentCube jump and Flip when Jumped
            if (Input.GetKeyDown(KeyCode.Space) && isOnPlayPlane)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerAnim.SetTrigger("trigFlip");
                isOnPlayPlane = false;
            }
            //DeathState by pressing K
            if (Input.GetKeyDown(KeyCode.K))
            {
                //Counter for ParentCube to DeathState when K is pressed 10 times
                Trackdeath--;
                if (Trackdeath == 0)
                {
                    playerAnim.SetTrigger("trigDeath");
                    isDeath = true;
                }
            }
        }  
    }

    //Make ParentCube jump 1 time only 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayPlane"))
        {
            isOnPlayPlane = true;
        }
    }
}
