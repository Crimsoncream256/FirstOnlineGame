using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{

    //Rigidbody��ϐ��ɓ����
    Rigidbody rb;
    //�ړ��X�s�[�h
    float speed = 3.0f;
    //�W�����v��
    float jumpForce = 400.0f;
    //���j�e�B�����̈ʒu������
    Vector3 playerPos;
    //�n�ʂɐڐG���Ă��邩�ۂ�
    bool Ground = true;
    int key = 0;

    public new AudioSource audio;
    public AudioClip _bite;

    public PlayerController2 _p2;
    public NPCObject _npc;
    public bool on_damage = false;



    void Start()
    {
        //Rigidbody���擾
        rb = GetComponent<Rigidbody>();
        //���j�e�B�����̌��݂�菭���O�̈ʒu��ۑ�
        playerPos = transform.position;
        Debug.Log(transform.tag);
        audio = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        GetInputKey();
        Move();
    }


    void GetInputKey()
    {
        //A�ED�L�[�A�����L�[�ŉ��ړ�
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;

        //W�ES�L�[�A�����L�[�őO��ړ�
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            key = 1;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            key = -1;
        }


    }


    void Move()
    {
        if (Ground)
        {
            if (Input.GetButton("Jump"))
            {
                //jumpForce�̕���������ɗ͂�������
                rb.AddForce(transform.up * jumpForce);
                Ground = false;
            }

        }

        //���݂̈ʒu�{���͂������l�̏ꏊ�Ɉړ�����
        rb.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed));

        //���j�e�B�����̍ŐV�̈ʒu���班���O�̈ʒu�������ĕ���������o��
        Vector3 direction = transform.position - playerPos;

        //�ړ������������ł��������ꍇ�ɕ����]��
        if (direction.magnitude >= 0.01f)
        {
            //direction��X����Z���̕�������������
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }
        else
        {
            key = 0;
        }

        //���j�e�B�����̈ʒu���X�V����
        playerPos = transform.position;

    }

    //�W�����v��APlane�ɐڐG�������ɐڐG�����true�ɖ߂�
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (!Ground)
                Ground = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Fruit")
        {
            if (this.gameObject.tag == "Fork")
            {
                Debug.Log(transform.name+": ��������");
                audio.PlayOneShot(_bite);
            }
            else
            {
                Debug.Log(transform.name + ": �Ȃ���");
            }
        }

        if(other.gameObject.tag == "Fork")
        {
            if(this.gameObject.tag == "Fruit")
            {
                Debug.Log(transform.name + ": ����[");
                audio.PlayOneShot(_bite);

            }
            else
            {
                Debug.Log(transform.name + ": �Ȃ���");
            }
        }
    }

    IEnumerator WaitForIt()
    {
        // 1�b�ԏ������~�߂�
        yield return new WaitForSeconds(1);

        // �P�b��_���[�W�t���O��false�ɂ���
        on_damage = false;
    }



}