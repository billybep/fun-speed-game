
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _speedGainPerSecond = 0.2f;
    [SerializeField] private float _turnSpeed = 200f;

    private int steerValue;

    // Update is called once per frame
    void Update()
    {
        _speed += _speedGainPerSecond * Time.deltaTime;

        transform.Rotate(0f, steerValue * _turnSpeed * Time.deltaTime, 0f);

        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        checkPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("Scene_MainMenu");
        }
    }

    public void Steer(int value)
    {
        steerValue = value;
    }

    void checkPosition()
    {
        if (transform.position.y <= -10.0f || transform.position.y >= 5.0f )
        {
            SceneManager.LoadScene("Scene_MainMenu");
            Debug.Log("Gameover");
        }

        if (transform.position.x >= 70.0f || transform.position.x <= -155.0f || transform.position.z >= 105.0f || transform.position.z <= -75.0f )
        {
            SceneManager.LoadScene("Scene_MainMenu");
            Debug.Log("Gameover");
        }
    }
}
