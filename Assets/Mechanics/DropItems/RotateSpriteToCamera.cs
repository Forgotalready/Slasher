using UnityEngine;

public class RotateSpriteToCamera : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    void Update()
    {
        transform.rotation = _mainCamera.transform.rotation;
    }
}
