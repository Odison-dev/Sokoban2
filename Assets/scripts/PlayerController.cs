using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock.LowLevel;
using UnityEngine.UIElements.Experimental;

public class PlayerController : MonoBehaviour
{
    public PlayerControl IptControl;

    public bool isMoving = false;
    //public bool canMove = true;

    public float walkSpeed = 10f;
    public Vector3 dir;
    public Vector3 posOffset;
    private Vector3 posOffset2;
    private Vector3 targetPos;
    public LayerMask Colliderable;
    private void Awake()
    {
        IptControl = new PlayerControl();

        posOffset2 = new Vector3(Mathf.Round(transform.position.x) - transform.position.x, Mathf.Round(transform.position.y) - transform.position.y, 0);
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0) + posOffset;
    }



    private void OnEnable()
    {
        IptControl.Enable();
        IptControl.Player.Move.started += OnMoveInputStarted;
        IptControl.Player.Move.canceled += OnMoveInputCancled;
    }

    private void OnDisable()
    {
        IptControl.Disable();
        IptControl.Player.Move.started -= OnMoveInputStarted;
        IptControl.Player.Move.canceled -= OnMoveInputCancled;
    }

    private void OnMoveInputStarted(InputAction.CallbackContext context)
    {
        // 只有不在移动中时才接收新的输入
        if (!isMoving)
        {
            Vector2 ipt = context.ReadValue<Vector2>();
            dir = new Vector3(ipt.x, ipt.y, 0);

            // 方向不为零时开始移动
            if (dir != Vector3.zero && canMove(dir))
            {
                isMoving = true;
                targetPos = transform.position + dir;
                
            }
        }
    }
    private void OnMoveInputCancled(InputAction.CallbackContext context)
    {
        if (Mathf.Approximately(transform.position.x - posOffset.x, Mathf.Round(transform.position.x - posOffset.x)) && Mathf.Approximately(transform.position.y - posOffset.y, Mathf.Round(transform.position.y - posOffset.y)))
        {
            isMoving = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {

        Vector2 ipt = IptControl.Player.Move.ReadValue<Vector2>();
        dir = new Vector3(ipt.x, ipt.y, 0);

        if (isMoving)
        {
            Move(dir);
            
            float distance = Vector3.Distance(transform.position, targetPos);
            if (distance < 0.01f)
            {
                // 确保精确到达目标位置
                transform.position = targetPos;
                isMoving = false;
            }
        }

        else
        {
            //CheckPush(dir);
            ReleaseBoxes();
        }

    }

    //玩家移动
    private void Move(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, walkSpeed * Time.deltaTime);
    }

    private void CheckPush(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, Colliderable);
        if (hit)
        {
            if (hit.transform.CompareTag("Pushable"))
            {
                hit.transform.SetParent(transform, true);
            }
        }

    }
    
    //临时方法
    private void ReleaseBoxes()
    {
        foreach(Transform child in this.transform)
        {
            child.SetParent(null, true);
        }
    }

    //检测玩家能否移动
    bool canMove(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, Colliderable);
        if (hit)
        {
            
            
            
            if (hit.transform.CompareTag("Wall"))
            {
                return false;
            }
            else if (hit.transform.CompareTag("Pushable"))
            {

                hit.transform.SetParent(transform, true);

                return true;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }

    }
} 
