using UnityEngine;
using System.Collections;

public class Directory : MonoBehaviour {

	private static Directory _instance;
	public static Directory Instance
	{ get {return _instance; } }

	public BulletShooter bulletShooter;
	public ScoreHandler scoreHandler;
	public PauseController pauseController;
	public LivesManager livesManager;
	public EnemyPool enemyPool;
	public BossManager bossManager;
	public ExplosionPool explosionPool;
	public SoundPool soundPool;

	void Awake()
	{
		if(_instance != null && _instance != this) Destroy(gameObject);
		_instance = this;
	}

}
