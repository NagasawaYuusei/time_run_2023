using UnityEngine;

public class PlayerState : MonoBehaviour
{

    [SerializeField]
    LayerMask _groundLayer;

    [SerializeField]
    Vector3 _groundDetectionSize;

    [SerializeField]
    Vector3 _groundDetectionCentor;

    Vector3 _playerCentor;

    [SerializeField]
    bool _isGizmo;


    Transform _transform;

    void Start()
    {
        Setup();
    }

    void Update()
    {
        State();
    }

    void Setup()
    {
        _transform = GetComponent<Transform>();
    }

    void State()
    {
        _playerCentor = _transform.position + _groundDetectionCentor;
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
}
