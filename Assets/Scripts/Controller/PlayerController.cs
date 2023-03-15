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

    private Camera cam;
    private Animator anim;

    private bool isMove;
    private Vector3 destiniation;
    private GameObject destPoint;

    private float restTime = 6.0f;

    private Vector3 startPos;
    private Quaternion startRot;

    private void Awake()
    {
        cam = Camera.main;
        anim = GetComponentInChildren<Animator>();
        startPos = transform.position;
        startRot = transform.rotation;
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
        destPoint.transform.position = destiniation + new Vector3(0, 0.3f, 0);

        _state = PlayerState.Moveing;

        Vector3 dir = (destiniation - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        transform.position = Vector3.Lerp(transform.position, destiniation, _speed * Time.deltaTime);
            
    }

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
            _state = (PlayerState)rand;
        }
    }

    public void ResetPos()
    {
        if (destPoint != null)
            destPoint.SetActive(false);

        isMove = false;
        _state = PlayerState.Idle;
        transform.position = startPos;
        transform.rotation = startRot;
    }
}