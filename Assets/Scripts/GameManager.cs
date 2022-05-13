using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    public int _fruitOrFork;
    private string _log;
    public Text _message;
    public GameObject _fruit;
    public GameObject _fork;

    public List<GameObject> _playerPrefabs;

    GameObject _player;

    [SerializeField]
    CinemachineVirtualCamera m_camera = default;


    void Start()
    {
        _fruitOrFork = Random.Range(1, 3);
        Debug.Log(_fruitOrFork);
        Debug.Log(Random.Range(1, 3));
        

        switch (_fruitOrFork)
        {
            case 1:
                _log = "Fruit";
                _player = Instantiate(_fruit);
                break;
            case 2:
                _log = "Fork";
                _player = Instantiate(_fork);
                break;
            default:
                _log = "–³";
                break;
        }
        Debug.Log("‚ ‚È‚½‚Í" +_log+ "‚Å‚·");
        _message.text = "‚ ‚È‚½‚Í"+_log+"‚Å‚·";

        m_camera.Follow = _player.transform;
        m_camera.LookAt = _player.transform;

    }


}
