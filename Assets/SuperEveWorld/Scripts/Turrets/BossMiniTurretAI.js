private var target : Transform;
var projectilePrefab : Transform;
private var time = 0;
var damp = 2F;

function Start() {
	target = GameObject.Find("MyEve").transform;
}

function Update () {
	if(target) {
		var rotate = Quaternion.LookRotation(target.position - transform.position);	
		rotate.x = 0;
		rotate.z = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * damp);

		if((target.position - gameObject.transform.position).magnitude < 30) {
			var seconds : int = Time.time;
			Shoot(seconds);
		}
	}
}

function Shoot(seconds) {
	var canShoot = (seconds % 2);
	
	if(canShoot) {
		if(time != seconds) {
			var projectile = Instantiate(projectilePrefab, 
			                             transform.Find("EnemyProjectileSpawn").transform.position, 
			                             Quaternion.identity);	

			projectile.rigidbody.AddForce(transform.forward * 20, ForceMode.Impulse);
		}
		time = seconds;
	}
}