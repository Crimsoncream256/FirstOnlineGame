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

    void Start()
    {
        //Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        //ユニティちゃんの現在より少し前の位置を保存
        playerPos = transform.position;
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



}