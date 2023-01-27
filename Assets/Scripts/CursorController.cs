using UnityEngine;
//using Cinemachine;

//ŽŽ—p
public class CursorController : MonoBehaviour
{
    [SerializeField] bool _isCursorMove;
    //[SerializeField] CinemachineVirtualCamera _vcam;

    //void Start()
    //{
    //    var vPOV = _vcam.GetCinemachineComponent<CinemachinePOV>();
    //    vPOV.m_HorizontalAxis.Value = 0;
    //    vPOV.m_VerticalAxis.Value = 0;
    //}

    void Update()
    {
        if( _isCursorMove)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
