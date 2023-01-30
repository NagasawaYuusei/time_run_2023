using UnityEngine;

public class PlayerSpeedController : MonoBehaviour
{
    #region �v���p�e�B
    public float PlayerSpeed => _playerSpeed;
    public float MaximizeplayerSpeed => _maximizePlayerSpeed;

    #endregion

    #region �ϐ�
    float _playerSpeed = 0;

    [SerializeField]
    float _maximizePlayerSpeed = 10f;

    [SerializeField]
    float _groundSpeed = 12f;

    [SerializeField]
    float _airSpeed = 1f;

    [SerializeField]
    float _wallRunSpeed = 1f;

    #endregion

    #region �C�x���g�o�^

    void Start()
    {
        PlayerStateController.Instance.OnPlayerStateChangeEnable += ChangeSpeed;
    }

    void OnDisable()
    {
        PlayerStateController.Instance.OnPlayerStateChangeEnable -= ChangeSpeed;
    }

    #endregion

    #region ���\�b�h

    void ChangeSpeed(PlayerStateController.PlayerStates state)
    {
        switch(state)
        {
            case PlayerStateController.PlayerStates.None:
                _playerSpeed = 0;
                break;
            case PlayerStateController.PlayerStates.Idle:
                _playerSpeed = _groundSpeed;
                break;
            case PlayerStateController.PlayerStates.Fly:
                _playerSpeed = _airSpeed;
                break;
            case PlayerStateController.PlayerStates.WallRun:
                _playerSpeed = _wallRunSpeed;
                break;
            case PlayerStateController.PlayerStates.Pause:
                _playerSpeed = 0;
                break;
            default:
                Debug.LogError("����ȃX�e�[�g�͂���܂���");
                break;
        }    
    }

    #endregion
}
