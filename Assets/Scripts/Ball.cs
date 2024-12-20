using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
        [SerializeField] private float _speed;
        [SerializeField] private AudioClip _getCubeClip;
        [SerializeField] private AudioClip _collideBorderClip;
        //[SerializeField] private AudioClip _deathClip;
        [SerializeField] private Rigidbody _rigidbody;
        
        private Vector3 direction = new Vector3(0.5f, 0.5f, 0);
        private int _count = 0;
        private bool _isActive = false;
        private AudioSource _audioSource;

        private void Start()
        {
                _audioSource = GetComponent<AudioSource>();
                _audioSource.volume = 0.5f;
        }
        private void Update()
        {
                if (Input.GetKeyDown(KeyCode.Space) && _isActive == false)
                {
                        _isActive = true;
                        transform.SetParent(null);
                        _rigidbody.velocity = direction * _speed;
                
                        // transform.Translate(direction * _speed * Time.deltaTime);
                        if (Mathf.Abs(direction.x) > 0.9f || Mathf.Abs(direction.x) < 0.1f)
                        {
                                direction.x = 0.5f;
                        }
                        if (Mathf.Abs(direction.y) > 0.9f || Mathf.Abs(direction.y) < 0.1f)
                        {
                                direction.y = 0.5f;
                        }
                        
                }
        }

        private void OnCollisionEnter(Collision other)
        {
                if (other.gameObject.CompareTag("BorderSide"))
                {
                       direction.x = -direction.x; 
                       _audioSource.PlayOneShot(_collideBorderClip);
                }
                else if (other.gameObject.CompareTag("BorderUpDown"))
                {
                        direction.y = -direction.y; 
                        _audioSource.PlayOneShot(_collideBorderClip);
                }
                else if (other.gameObject.CompareTag("BorderGameOver"))
                {
                        _isActive = false;
                        //_audioSource.PlayOneShot(_deathClip);
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                if (other.collider.CompareTag("GetCube"))
                {
                        other.gameObject.SetActive(false);
                        _audioSource.PlayOneShot(_getCubeClip);
                        direction.x = -direction.x; 
                        _count++;
                        if (_count >= 45)
                        {
                                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        }
                        
                }
        }
}