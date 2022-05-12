using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _speed = 2.0f;
    public float _brake = 0.5f;
    public float _jumpForce = 1.0f;

    private Vector3 _height;
    private Rigidbody _rB;
    private Vector3 rbVelo;
    Vector3 _moveDirection;
    public float _moveTurnSpeed = 10f;

    public bool _ground = true;
    int _key = 0;

    void Start()
    {
        _rB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        rbVelo = Vector3.zero;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        _moveDirection = new Vector3(x * _speed, 0, z * _speed);
        if (_moveDirection.magnitude > 0.01f && !(Input.GetKey(KeyCode.LeftShift)))
        {
            Quaternion moveRot = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, moveRot, Time.deltaTime * _moveTurnSpeed);
        }

        rbVelo = _rB.velocity;
        _rB.AddForce(x * _speed - rbVelo.x * _brake, 0, z * _speed - rbVelo.z * _brake, ForceMode.Impulse);


        _height = this.GetComponent<Transform>().position;
        if (_height.y <= -3.0f)
        {
            this.gameObject.SetActive(false);
        }


            if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_ground)
            {
                //jumpForce‚Ì•ª‚¾‚¯ã•û‚É—Í‚ª‚©‚©‚é
                _rB.AddForce(transform.up * _jumpForce);
                _ground = false;
            }

        }

    }

    void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.tag == "Goal")
        {
            //other.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
            rB.AddForce(-rbVelo.x * 0.8f, 0, -rbVelo.z * 0.8f, ForceMode.Impulse);
            //goalText.enabled = true;
            goalOn = true;
        }
        */

        if (other.gameObject.tag == "Ground")
        {
            if (!_ground)
                _ground = true;
        }

    }
}
