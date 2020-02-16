using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	//handle to text
	[SerializeField] private Text _scoreText;
	[SerializeField] private Sprite[] _liveSprites;
	[SerializeField] private Image _livesDisplay;
	[SerializeField] private Text _gameOverText;
	[SerializeField] private Text _restartText;
	[SerializeField] private bool _isGameOver;
	[SerializeField] private Text _GrenadeControl;
	[SerializeField] private bool _haveGrenade;
	// Start is called before the first frame update
	void Start()
	{
		//assign text component to handle
		
		_scoreText.text = "Score: " + 0;
		_gameOverText.gameObject.SetActive(false);
		_restartText.gameObject.SetActive(false);

	}

	// Update is called once per frame
	void Update()
	{
		if ((_isGameOver == true) && Input.GetKey(KeyCode.R))
		{

			SceneManager.LoadScene("Game");
		}
	}
	public void UpdateScore(int playerScore)
	{
		_scoreText.text = "Score: " + playerScore.ToString();
	}
	public void UpdateLives(int currentLives)
	{
		//access img sprite
		//give it a new one based on the currentLives index
		_livesDisplay.sprite = _liveSprites[currentLives];
	}
	public void GameOverDisplay()
	{

		_isGameOver = true;
		_gameOverText.gameObject.SetActive(true);
		_restartText.gameObject.SetActive(true);
		StartCoroutine(GameOverDisplayFlicker());
	}
	IEnumerator GameOverDisplayFlicker()
	{
		while (true)
		{

			yield return new WaitForSeconds(.5f);

			_gameOverText.gameObject.SetActive(false);
			yield return new WaitForSeconds(.5f);

			_gameOverText.gameObject.SetActive(true);
		}

	}
	public void GrenadeCollected()
	{
			_haveGrenade = true;
			_GrenadeControl.gameObject.SetActive(true);
	}
	public void GrenadeUsed()
	{
		_haveGrenade = false;
		_GrenadeControl.gameObject.SetActive(false);
	}
}
