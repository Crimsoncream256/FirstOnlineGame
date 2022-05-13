using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float _speed = 2.0f;
    public float _brake = 0.5f;
    public float _jumpForce = 5.0f;

    private Vector3 _height;
    private Rigidbody _rB;
    private Vector3 rbVelo;
    Vector3 _moveDirection;
    public float _moveTurnSpeed = 10f;

    [SerializeField] bool _ground = true;
    int _key = 0;

    public new AudioSource audio;
    public AudioClip _bite;
    public AudioClip _nakama;

    public PlayerController2 _p2;
    public NPCObject _npc;
    public bool on_damage = false;

    public float bounce = 5.0f;
    int _hp = 20;
    public Text _hpTextUI;
    string _hpText;
    public GameObject _hpTextObj;
    public string objname = "";

    void Start()
    {
        Debug.Log(transform.name + ": " + _hp);
        _rB = GetComponent<Rigidbody>();
        Debug.Log(transform.tag);
        audio = gameObject.AddComponent<AudioSource>();
        _ground = true;
    }

    void FixedUpdate()
    {

        rbVelo = Vector3.zero;
        float x = Input.GetAxis("Horizontal");

        //transform.position += transform.forward * 10f * Time.deltaTime;

        float z = Input.GetAxis("Vertical");

        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);

        _moveDirection =  new Vector3(x * _speed, 0, z * _speed);

        if (_moveDirection.magnitude > 0.01f && !(Input.GetKey(KeyCode.LeftShift)))
        {
            Quaternion moveRot = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, moveRot, Time.deltaTime * _moveTurnSpeed);
        }

        rbVelo = _rB.velocity;
        _rB.AddForce(x * _speed - rbVelo.x * _brake, 0, z * _speed - rbVelo.z * _brake, ForceMode.Impulse);
        //_rB.AddForce(x * horizontalRotation * _speed, 0, z * horizontalRotation * _speed, ForceMode.Impulse);
        //_rB.AddForce(horizontalRotation * transform.forward * x *_speed * z *_speed, ForceMode.Impulse);


        _height = this.GetComponent<Transform>().position;
        if (_height.y <= -3.0f)
        {
            this.gameObject.SetActive(false);
        }


            if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_ground)
            {
                //jumpForce�̕���������ɗ͂�������
                _rB.AddForce(transform.up * _jumpForce);
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

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Fruit")
        {
            if (this.gameObject.tag == "Fork")
            {
                Debug.Log(transform.name + ": ��������");
                Vector3 norm = other.contacts[0].normal;
                Vector3 vel = other.rigidbody.velocity.normalized;
                vel += new Vector3(-norm.x * 2, 0f, -norm.z * 2);
                other.rigidbody.AddForce(vel * bounce, ForceMode.Impulse);
                audio.PlayOneShot(_bite);
            }
            else
            {
                Debug.Log(transform.name + ": �Ȃ���");
                audio.PlayOneShot(_nakama);
            }
        }

        if (other.gameObject.tag == "Fork")
        {
            if (this.gameObject.tag == "Fruit" && !on_damage)
            {

                Debug.Log(transform.name + ": ����[");
                audio.PlayOneShot(_bite);
                _hp -= 5;
                Debug.Log(transform.name + ": " + _hp);
                OnDamageEffect();

            }
            else if (this.gameObject.tag == "Fruit" && on_damage)
            {
                Debug.Log(transform.name + ": ���G����");
            }
            else
            {
                Debug.Log(transform.name + ": �Ȃ���");
                audio.PlayOneShot(_nakama);
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

    void OnDamageEffect()
    {
        // �_���[�W�t���OON
        on_damage = true;

        // �R���[�`���J�n
        StartCoroutine("WaitForIt");
    }
}
