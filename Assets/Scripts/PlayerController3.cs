using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    float inputHorizontal;
    float inputVertical;
    Rigidbody rb;
    public float _jumpForce = 100.0f;


    [SerializeField] bool _ground = true;

    public float moveSpeed = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_ground)
            {
                //jumpForce�̕���������ɗ͂�������
                rb.AddForce(transform.up * _jumpForce);
                _ground = false;
            }

        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("�Ȃ񂩓�����");
        if (col.gameObject.tag == "Ground")
        {
            Debug.Log("���߂�");
            if (!_ground)
                _ground = true;
        }
    }
}
