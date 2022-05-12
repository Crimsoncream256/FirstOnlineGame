using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int _fruitOrFork;
    private string _log;
    public Text _message;
    public GameObject _fruit;
    public GameObject _fork;


    void Start()
    {
        _fruitOrFork = Random.Range(1, 3);
        Debug.Log(_fruitOrFork);
        Debug.Log(Random.Range(1, 3));
        

        switch (_fruitOrFork)
        {
            case 1:
                _log = "Fruit";
                Instantiate(_fruit);
                break;
            case 2:
                _log = "Fork";
                Instantiate(_fork);
                break;
            default:
                _log = "–³";
                break;
        }
        Debug.Log("‚ ‚È‚½‚Í" +_log+ "‚Å‚·");
        _message.text = "‚ ‚È‚½‚Í"+_log+"‚Å‚·";

        

    }


}
