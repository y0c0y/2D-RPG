using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;//충돌 체크 레이어

    public float speed;
    private Vector3 vector ; //캐릭터 이동 방향
    private bool applyRunFlag = false;

    public float runSpeed;
    private float applyRunSpeed;

    public int WalkCount;
    private int currentWalkCount;

    private bool canMove = true;

    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        
    }

    IEnumerator MoveCoroutine() //코루틴 함수 //다중 처리를 위해 사용
    {
        while(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") !=0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if(vector.x != 0) 
            {
                vector.y = 0;
            }
            //if(vector.y != 0)
            //{
               // vector.x = 0;
            //}

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit; // A지점에서 B지점까지 충돌체크

            animator.SetBool("Walking", true);



            while (currentWalkCount < WalkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag) //Shift가 눌렸을 때
                {
                    currentWalkCount++;
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);

            }
            currentWalkCount = 0;

           
        }
        animator.SetBool("Walking", false);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {   
        if(canMove)
        {
            //좌 : -1, 우 : 1
            //상 : 1, 하 : -1 // "Vertical"
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {

                canMove = false;
                StartCoroutine(MoveCoroutine());

            }
        }
        
    }
}
