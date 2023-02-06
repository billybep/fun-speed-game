using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _scoreMultiplier;

    private float _score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _score += Time.deltaTime * _scoreMultiplier;

        _scoreText.text = Mathf.FloorToInt(_score).ToString();
    }
}
