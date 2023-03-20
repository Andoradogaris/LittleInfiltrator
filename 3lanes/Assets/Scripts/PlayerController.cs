using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float frontRatio;
    [SerializeField]
    private float backRatio;
    [SerializeField]
    private float sideRatio;

    [SerializeField]
    private GameObject body;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float timer;
    [HideInInspector]
    public bool canMove;

    void Update()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                Move(1);
                body.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Move(2);
                body.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Move(3);
                body.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                Move(4);
                body.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }
    }

    void Move(int direction) 
    { 
        if(direction == 1)
        {
            transform.position += new Vector3(-1f, 0f, 0f) * Time.deltaTime * frontRatio;
        }
        else if(direction == 2)
        {
            transform.position += new Vector3(1f, 0f, 0f) * Time.deltaTime * backRatio;
        }
        else if(direction == 3)
        {
            transform.position += new Vector3(0f, 0f, 1f) * Time.deltaTime * sideRatio;
        }
        else
        {
            transform.position += new Vector3(0f, 0f, -1f) * Time.deltaTime * sideRatio;
        }
        anim.SetBool("isRunning", true);
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(0.1f);
        canMove = false;
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
