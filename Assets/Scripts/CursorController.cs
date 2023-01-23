using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] bool _isCursorMove;

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
