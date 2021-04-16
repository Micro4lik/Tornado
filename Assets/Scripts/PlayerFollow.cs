using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [Range(0.01f, 1f)] [SerializeField] private float smoothFactor = 1f;
    [SerializeField] private bool lookAtPlayer;

    private Vector3 _cameraOffset;

    private void Start()
    {
        _cameraOffset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        var newPos = playerTransform.position + _cameraOffset;
        transform.position = Vector3.Lerp(transform.position, newPos, smoothFactor);

        if (lookAtPlayer)
            transform.LookAt(playerTransform);
    }
}