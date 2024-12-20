using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 15.0f;
    [SerializeField] private float _left = -6.0f;
    [SerializeField] private float _right = 6.0f;

    private void Update()
    {
        float xDirection = Input.GetAxis("Horizontal");
        
        Vector3 movement = new Vector3(xDirection, 0, 0);
        transform.Translate(movement * _speed * Time.deltaTime);
        var targetPosition = transform.position;
        targetPosition.x = Mathf.Clamp(transform.position.x, _left, _right);
        transform.position = targetPosition;
    }
}
