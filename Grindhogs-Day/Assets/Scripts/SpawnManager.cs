using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour {
	
	public GameObject pastLife;
	public int lifeBuffer = 5; // number of lives that can be held at a time
	public int spawnWait = 5; // time between death and 

    public GameObject[] savedObjects;
    public int numObjs;

	ArrayList avatars; // Stores inputQs for each life
	Vector2 initPos;
	Quaternion initRot;
	bool spawnPastLives = false;
	float opacityMult = 0.2f;

	void Awake() {
		DontDestroyOnLoad(this.gameObject);
		if (FindObjectsOfType(GetType()).Length > 1)
	    {
	        Destroy(gameObject);
	    }
        opacityMult = 1.0f/lifeBuffer;
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
			int i=0;
			foreach(Queue avatarQ in avatars){
				i+=1;
				SpawnAvatar(avatarQ,i);				
			}
			spawnPastLives = false;
        }
	}

	void AddAvatar(Queue InputQ){
		avatars.Add(InputQ);
		if(avatars.Count>lifeBuffer){
			avatars.RemoveAt(0);
		}
	}

	void SpawnAvatar(Queue InputQ, int iter){
		GameObject lastLife = Instantiate(pastLife,initPos,initRot);
		lastLife.GetComponent<AvatarController>().InitiateQ(InputQ);
		lastLife.GetComponent<AvatarController>().SetAlpha(1f - (opacityMult * (avatars.Count - iter))); // make the avatars fade
	}

	public void KillPlayer (Queue InputQ) {
		AddAvatar(InputQ);
		StartCoroutine(DeathDelay());
	}

	IEnumerator DeathDelay () {
		yield return new WaitForSeconds(spawnWait);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene after death
	}
}
