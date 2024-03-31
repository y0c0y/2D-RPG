using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public float speed;
    private Vector3 vector ; //ĳ���� �̵� ����
    private bool applyRunFlag = false;

    public float runSpeed;
    private float applyRunSpeed;

    public int WalkCount;
    private int currentWalkCount;

    private bool canMove = true;
    
    // Start is called before the first frame update
    void Start()
    {

        
    }

    IEnumerator MoveCoroutine() //�ڷ�ƾ �Լ� //���� ó���� ���� ���
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
            if(applyRunFlag) //Shift�� ������ ��
            {
                currentWalkCount++;
            }
            currentWalkCount++;
            yield return new WaitForSeconds(0.01f);

        }
        currentWalkCount = 0;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {   
        if(canMove)
        {
            //�� : -1, �� : 1
            //�� : 1, �� : -1 // "Vertical"
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {

                canMove = false;
                StartCoroutine(MoveCoroutine());

            }
        }
        
    }
}
