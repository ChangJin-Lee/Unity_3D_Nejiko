using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //5.3

public class GameController : MonoBehaviour
{
	public NejikoController nejiko;
	public Text scoreLabel;
	public LifePanel lifePanel;

	public void Update()
	{
		// ���ھ� ���̺��� ������Ʈ
		int score = CalcScore();
		scoreLabel.text = "Score : " + score + "m";

		// ������ �г��� ������Ʈ
		lifePanel.UpdateLife(nejiko.Life());

		// �������� �������� 0�� �Ǹ� ���� ����
		if (nejiko.Life() <= 0)
		{
			// �� ������ ������Ʈ�� �����
			enabled = false;

			// ���� ���ھ ������Ʈ
			if (PlayerPrefs.GetInt("HighScore") < score)
			{
				PlayerPrefs.SetInt("HighScore", score);
			}

			// 2���Ŀ� ReturnToTitle�� ȣ��
			Invoke("ReturnToTitle", 2.0f);
		}
	}

	int CalcScore()
	{
		// �������� ���� �Ÿ��� ���ھ�� �Ѵ�
		return (int)nejiko.transform.position.z;
	}

	void ReturnToTitle()
	{
		// Ÿ��Ʋ ������ ��ȯ
		//Application.LoadLevel("Title");  //5.2
		SceneManager.LoadScene("Title");   //5.3
	}
}
