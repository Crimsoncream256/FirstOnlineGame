using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class GenerateAndFollow : MonoBehaviour
{
    [SerializeField]
    GameObject m_testPlayerPrefab = default;

    public List<GameObject> _playerPrefabs;

    [SerializeField]
    float m_moveSpeed = 5.0f;

    [SerializeField]
    CinemachineVirtualCamera m_camera = default;

    GameObject _player;

    void Start()
    {
        _player = Instantiate(m_testPlayerPrefab);
        m_camera.Follow = _player.transform;
        m_camera.LookAt = _player.transform;
    }

    void Update()
    {
        //_player.transform.position += Vector3.right * m_moveSpeed * Time.deltaTime;
    }
}