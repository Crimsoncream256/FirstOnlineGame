using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{

    //Rigidbodyを変数に入れる
    Rigidbody rb;
    //移動スピード
    float speed = 3.0f;
    //ジャンプ力
    float jumpForce = 400.0f;
    //ユニティちゃんの位置を入れる
    Vector3 playerPos;
    //地面に接触しているか否か
    bool Ground = true;
    int key = 0;

    public new AudioSource audio;
    public AudioClip _bite;
    public AudioClip _nakama;

    public PlayerController2 _p2;
    public NPCObject _npc;
    public bool on_damage = false;

    public float bounce = 5.0f;


    void Start()
    {
        //Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        //ユニティちゃんの現在より少し前の位置を保存
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
        //A・Dキー、←→キーで横移動
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;

        //W・Sキー、↑↓キーで前後移動
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
                //jumpForceの分だけ上方に力がかかる
                rb.AddForce(transform.up * jumpForce);
                Ground = false;
            }

        }

        //現在の位置＋入力した数値の場所に移動する
        rb.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed));

        //ユニティちゃんの最新の位置から少し前の位置を引いて方向を割り出す
        Vector3 direction = transform.position - playerPos;

        //移動距離が少しでもあった場合に方向転換
        if (direction.magnitude >= 0.01f)
        {
            //directionのX軸とZ軸の方向を向かせる
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }
        else
        {
            key = 0;
        }

        //ユニティちゃんの位置を更新する
        playerPos = transform.position;

    }

    //ジャンプ後、Planeに接触した時に接触判定をtrueに戻す
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
        if (other.gameObject.tag == "Fruit")
        {
            if (this.gameObject.tag == "Fork")
            {
                Debug.Log(transform.name + ": もぐもぐ");
                Vector3 norm = other.contacts[0].normal;
                Vector3 vel = other.rigidbody.velocity.normalized;
                vel += new Vector3(-norm.x * 2, 0f, -norm.z * 2);
                other.rigidbody.AddForce(vel * bounce, ForceMode.Impulse);
                audio.PlayOneShot(_bite);
            }
            else
            {
                Debug.Log(transform.name + ": なかま");
                audio.PlayOneShot(_nakama);
            }
        }

        if (other.gameObject.tag == "Fork")
        {
            if (this.gameObject.tag == "Fruit" && !on_damage)
            {
                rb.AddForce(2,2,2, ForceMode.Impulse);

                Debug.Log(transform.name + ": ぎゃー");
                audio.PlayOneShot(_bite);

                OnDamageEffect();

            }
            else
            {
                Debug.Log(transform.name + ": なかま");
                audio.PlayOneShot(_nakama);
            }
        }
    }

    IEnumerator WaitForIt()
    {
        // 1秒間処理を止める
        yield return new WaitForSeconds(1);

        // １秒後ダメージフラグをfalseにする
        on_damage = false;
    }

    void OnDamageEffect()
    {
        // ダメージフラグON
        on_damage = true;



        /*
        // プレイヤーの位置を後ろに飛ばす
        float s = 100f * Time.deltaTime;
        transform.Translate(Vector3.up * s);

        // プレイヤーのlocalScaleでどちらを向いているのかを判定
        if (transform.localScale.x >= 0)
        {
            transform.Translate(Vector3.left * s);
        }
        else
        {
            transform.Translate(Vector3.right * s);
        }
        */

        // コルーチン開始
        StartCoroutine("WaitForIt");
    }

}