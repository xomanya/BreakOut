using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
        [SerializeField] private float _speed;
        [SerializeField] private AudioClip _getCubeClip;
        [SerializeField] private AudioClip _collideBorderClip;
        [SerializeField] private AudioClip _deathClip;
        
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
                }

                if (_isActive == true)
                {
                        transform.Translate(direction * _speed * Time.deltaTime);
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
                        _audioSource.PlayOneShot(_deathClip);
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
        }

        private void OnTriggerEnter(Collider other)
        {
                if (other.CompareTag("GetCube"))
                {
                        other.gameObject.SetActive(false);
                        _audioSource.PlayOneShot(_getCubeClip);
                        direction.x = -direction.x; 
                        _count++;
                }

                if (_count >= 45)
                {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
        }
}