using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour {
	
	public GameObject pastLife;

	ArrayList avatars; // Stores inputQs for each life
	Vector2 initPos;
	Quaternion initRot;
	bool spawnPastLives = false;

	void Awake() {
		DontDestroyOnLoad(this.gameObject);
		if (FindObjectsOfType(GetType()).Length > 1)
	    {
	        Destroy(gameObject);
	    }
	}

	void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    	if(avatars!=null){
        	spawnPastLives = true;
    	}
    }

	// Use this for initialization
	void Start () {
		if(avatars==null)
			avatars = new ArrayList();

		initPos = GameObject.Find("Player").transform.position;		// spawn point
		initRot = GameObject.Find("Player").transform.rotation;		// spawn rotation
	}

	void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
	
	// Update is called once per frame
	void Update () {
		if(spawnPastLives && avatars.Count>0){
			foreach(Queue avatarQ in avatars){
				SpawnAvatar(avatarQ);
			}
			spawnPastLives = false;
		}
	}

	void AddAvatar(Queue InputQ){
		avatars.Add(InputQ);
		if(avatars.Count>5){
			avatars.RemoveAt(0);
		}
	}

	void SpawnAvatar(Queue InputQ){
		GameObject lastLife = Instantiate(pastLife,initPos,initRot);
		lastLife.GetComponent<AvatarController>().InitiateQ(InputQ);
	}

	public void KillPlayer (Queue InputQ) {
		AddAvatar(InputQ);
		StartCoroutine(DeathDelay());
	}

	IEnumerator DeathDelay () {
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene after death
	}
}
