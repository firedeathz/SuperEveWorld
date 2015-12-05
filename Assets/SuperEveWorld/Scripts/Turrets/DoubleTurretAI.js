private var target : Transform;
var projectilePrefab : Transform;
private var time1 = 0;
private var time2 = 50;
var damp = 2F;

function Start() {
	target = GameObject.Find("MyEve").transform;
}

function Update () {
	if(target) {
		if((target.position - gameObject.transform.position).magnitude < 20) {
			var rotate = Quaternion.LookRotation(target.position - transform.position);
			rotate.x = 0;
			rotate.z = 0;
			transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * damp);
		}
		var seconds : int = Time.time;
		Shoot(seconds);
	}
}

function Shoot(seconds) {
	var canShoot = (seconds % 2);
		
	if(canShoot) {
		if(time1 != seconds) {
			var projectile1 = Instantiate(projectilePrefab, 
			                             transform.Find("EnemyProjectileSpawn1").transform.position, 
			                             Quaternion.identity);	

			projectile1.rigidbody.AddForce(transform.forward * 20, ForceMode.Impulse);
		}
		time1 = seconds;
	}
	else {
		if(time2 != seconds) {
			var projectile2 = Instantiate(projectilePrefab, 
			                             transform.Find("EnemyProjectileSpawn2").transform.position, 
			                             Quaternion.identity);	

			projectile2.rigidbody.AddForce(transform.forward * 20, ForceMode.Impulse);
		}
		time2 = seconds;
	}
}