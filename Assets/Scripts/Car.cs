
using UnityEngine;

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
    }

    public void Steer(int value)
    {
        steerValue = value;
    }
}
