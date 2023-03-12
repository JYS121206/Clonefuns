using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Welcome,
        Idle,
        Moveing,
        Wait1,
        Wait2,
        Wait3,
    }

    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private PlayerState _state = PlayerState.Welcome;

    private Vector3 destiniation;
    private bool isMove;
    private Camera cam;
    private Animator anim;
    private float restTime = 6.0f;
    private Vector3 startPos;
    private Quaternion startRot;
    GameObject destPoint;
    [SerializeField] private Vector3 _delta = new Vector3(0, 0.3f, 0);

    private void Awake()
    {
        cam = Camera.main;
        anim = GetComponentInChildren<Animator>();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void Start()
    {
        //Managers.Input.KeyAction -= MoveKeyCode;
        //Managers.Input.KeyAction += MoveKeyCode;
    }

    private void Update()
    {
        SetPlayerState();
        MoveMouse();
    }

    public void SetDestiniation(Vector3 dest)
    {
        destiniation = new Vector3(dest.x, transform.position.y, dest.z);
        isMove = true;
    }

    public void Move()
    {
        if (Vector3.Distance(transform.position, destiniation) <= 0.1f)
        {
            if (isMove)
                _state = PlayerState.Idle;

            isMove = false;

            if (destPoint != null)
                destPoint.SetActive(false);
        }

        if (!isMove) return;

        if(destPoint == null)
            destPoint = Managers.Resource.Instantiate("objPoint");

        destPoint.SetActive(true);
        destPoint.transform.position = destiniation + _delta;
        _state = PlayerState.Moveing;
        Vector3 dir = (destiniation - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        transform.position = Vector3.Lerp(transform.position, destiniation, _speed * Time.deltaTime);
    }

    /// <summary> 마우스로 이동 </summary>
    public void MoveMouse()
    {
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            RaycastHit hit;

            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
                SetDestiniation(hit.point);
        }
        Move();
    }

    /// <summary> 키보드로 이동 </summary>
    public void MoveKeyCode()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.1f);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.1f);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.1f);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.1f);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
    }

    public void SetPlayerState()
    {
        switch (_state)
        {
            case PlayerState.Welcome:
                anim.SetTrigger("Start");
                Invoke("Rest", restTime);
                break;
            case PlayerState.Idle:
                anim.SetBool("Run", false);
                Invoke("Rest", restTime);
                break;  
            case PlayerState.Moveing:
                anim.SetBool("Run", true);
                CancelInvoke();
                anim.SetInteger("Rest", 0);
                break;
            case PlayerState.Wait1:
                anim.SetInteger("Rest", 1);
                break;
            case PlayerState.Wait2:
                anim.SetInteger("Rest", 2);
                break;
            case PlayerState.Wait3:
                anim.SetInteger("Rest", 3);
                break;
        }
    }

    public void Rest()
    {
        if (_state == PlayerState.Idle || _state == PlayerState.Welcome)
        {
            int rand = Random.Range(3, 6);
            Debug.Log($"{(PlayerState)rand}");
            _state = (PlayerState)rand;
        }
    }

    public void ReSetPos()
    {
        if (destPoint != null)
            destPoint.SetActive(false);

        isMove = false;
        _state = PlayerState.Idle;
        transform.position = startPos;
        transform.rotation = startRot;
    }
}