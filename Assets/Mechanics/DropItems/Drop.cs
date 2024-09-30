using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] GameObject _groundCheck;
    private float _gravity = 10f;
    [SerializeField] LayerMask _layerMask;
    void Update()
    {
        if (Physics.CheckSphere(_groundCheck.transform.position, 0.1f, _layerMask)) _gravity = 0f;

        transform.position -= new Vector3(0f, _gravity * Time.deltaTime, 0f);
    }
}
