using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject endGame;

    public static GameManager Instance;

    [SerializeField]
    private Transform[] _spawnPositions;

    public GameObject retryButton;
    public bool collectibleSpawnCheck = true;

    private static int score;

    [SerializeField]
    private GameObject pointPrefab;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private TextMeshProUGUI _highscoreText;

    [SerializeField]
    private TextMeshProUGUI _yourscoreText;

    public int spawnDelay;

    private Transform spawnLocation;
   
    private void Awake()
    {
        score = 0;
        if(Instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        retryButton.GetComponent<Button>().onClick.AddListener(RetryGame);
    }

    void Start()
    {
        endGame.SetActive(false);   
        StartCoroutine(SpawnCaller());
        _highscoreText.text = PlayerPrefs.GetInt("HIGHSCORE").ToString();
        _yourscoreText.text = PlayerPrefs.GetInt("YOURSCORE").ToString();
        retryButton.SetActive(false);
    }
    IEnumerator SpawnCaller()
    {
        while (true)
        {
            if (collectibleSpawnCheck)
            {
                SpawnObstacle();
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    private void SpawnObstacle()
    {
        spawnLocation = _spawnPositions[Random.Range(0, _spawnPositions.Length)];
        GameObject spawnnedObj = Instantiate(pointPrefab, spawnLocation.position, Quaternion.identity, transform);
        collectibleSpawnCheck = false;
    }
    public void UpdateScore()
    {
        score++;
        _scoreText.text = score.ToString();
    }
    public void EndGame()
    {
        endGame.SetActive(true);
        retryButton.SetActive(true);
        _yourscoreText.text = score.ToString();
        if (score > PlayerPrefs.GetInt("HIGHSCORE", 0))
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
            PlayerPrefs.Save();
            _highscoreText.text = score.ToString();
        }
        StopAllCoroutines();
    }
    public void RetryGame()
    {
        ChangeScene.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
