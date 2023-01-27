using UnityEngine;

public class PlayerSpeedController : MonoBehaviour
{
    #region プロパティ
    public static float PlayerSpeed => _playerSpeed;
    public static float MaximizeplayerSpeed => _maximizePlayerSpeed;

    #endregion

    #region 変数
    static float _playerSpeed = 0;

    [SerializeField]
    static float _maximizePlayerSpeed = 10f;

    [SerializeField]
    float _groundSpeed = 12f;

    [SerializeField]
    float _airSpeed = 1f;

    [SerializeField]
    float _wallRunSpeed = 1f;

    #endregion

    #region イベント登録

    void OnEnable()
    {
        PlayerStateController.Instance.OnPlayerStateChangeEnable += ChangeSpeed;
    }

    void OnDisable()
    {
        PlayerStateController.Instance.OnPlayerStateChangeEnable -= ChangeSpeed;
    }

    #endregion

    #region メソッド

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
                Debug.LogError("そんなステートはありません");
                break;
        }    
    }

    #endregion
}
