using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushNDestroy
{
	public class UIManager : MonoBehaviour
	{
        public GameObject healthBarPrefab;
		//public GameObject gameOverUI;

		private List<HealthBar> healthBar;
        private Transform healthBarContainer;

		private void Awake()
		{
			healthBar = new List<HealthBar>();
            healthBarContainer = new GameObject("HealthBarContainer").transform;
		}

		public void AddHealthUI(EntityEvents p)
        {
            GameObject newUIObject = Instantiate<GameObject>(healthBarPrefab, p.transform.position, Quaternion.identity, healthBarContainer);
            p.healthBar = newUIObject.GetComponent<HealthBar>(); //store the reference in the ThinkingPlaceable itself
            //p.healthBar.StartHealthBarUI(p);
			
			healthBar.Add(p.healthBar);
        }

		public void RemoveHealthUI(EntityEvents p)
		{
			healthBar.Remove(p.healthBar);
			
			Destroy(p.healthBar.gameObject);
		}

		// public void ShowGameOverUI()
		// {
		// 	gameOverUI.SetActive(true);
		// }

		public void OnRetryButton()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		}

		private void LateUpdate()
		{
			for(int i=0; i<healthBar.Count; i++)
			{
				healthBar[i].Move();
			}
		}
	}
}