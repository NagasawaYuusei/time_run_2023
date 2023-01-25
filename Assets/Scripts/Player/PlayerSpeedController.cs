using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedController : MonoBehaviour
{
    public static float PlayerSpeed => _playerSpeed;
    public static float MaximizeplayerSpeed;

    static float _playerSpeed = 0;
    [SerializeField] static float _maximizePlayerSpeed;

    [SerializeField] float _groundSpeed;
    [SerializeField] float _airSpeed;
    [SerializeField] float _wallRunSpeed;

    void Start()
    {
        
    }
}
