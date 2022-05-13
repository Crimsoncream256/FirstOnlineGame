using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCObject : MonoBehaviour
{
    public PlayerController2 _p2;
    public NPCObject _npc;
    public bool on_damage = false;

    public new AudioSource audio;
    public AudioClip _bite;
    public AudioClip _nakama;

    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Fruit")
        {
            if (this.gameObject.tag == "Fork")
            {
                Debug.Log(transform.name + ": もぐもぐ");
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
            if (this.gameObject.tag == "Fruit")
            {
                Debug.Log(transform.name + ": ぎゃー");
                audio.PlayOneShot(_bite);

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
}
