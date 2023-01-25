using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    [SerializeField]
    LayerMask _groundLayer;

    [SerializeField]
    Vector3 _groundDetectionSize;

    [SerializeField]
    Vector3 _groundDetectionCentor;

    Vector3 _playerCentor;

    [SerializeField]
    bool _isGizmo;

    Rigidbody _rb;

    [SerializeField] float _groundDrag;
    [SerializeField] float _airDrag;

    Transform _transform;
    bool _isWallRun;

    PlayerStatesEnum _playerStatesEnum;

    public bool IsWallRun => _isWallRun;

    void Awake()
    {
        if(Instance && Instance.gameObject)
        {
            Debug.Log("Instanceï°êîÇ†ÇÈÇÊÅ[");
        }
        Instance = this;
    }

    void Start()
    {
        Setup();
    }

    void Update()
    {
        State();
        DragControl();
    }

    void Setup()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    void State()
    {
        _playerCentor = _transform.position + _groundDetectionCentor;
        //Debug.Log(_rb.velocity);
    }

    void DragControl()
    {
        if (IsGround())
        {
            _rb.drag = _groundDrag;
        }
        else
        {
            _rb.drag = _airDrag;
        }
    }

    void ChangeWallRunState(bool on)
    {
        _isWallRun = on;
    }

    public bool IsGround()
    {
        Collider[] collision = Physics.OverlapBox(_playerCentor, _groundDetectionSize, Quaternion.identity, _groundLayer);
        if (collision.Length != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_isGizmo)
        {
            Gizmos.DrawCube(_playerCentor, _groundDetectionSize);
        }
    }

    public enum PlayerStatesEnum
    {
        None,
        Idle,
        WallRun,
        Fly,
        Pause
    }
}
