private var target : Transform;
var projectilePrefab : Transform;
private var time = 0;
var damp = 2F;

private var shoot1 : Transform;
private var shoot2 : Transform;
private var shoot3 : Transform;

function Start() {
	target = GameObject.Find("MyEve").transform;
	shoot1 = GameObject.Find("ShootPoint1").transform;
	shoot2 = GameObject.Find("ShootPoint2").transform;
	shoot3 = GameObject.Find("ShootPoint3").transform;
}

function Update () {
	if(target) {
		var rotate = Quaternion.LookRotation(target.position - transform.position);
		rotate.x = 0;
		rotate.z = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * damp);
		
		if((target.position - gameObject.transform.position).magnitude < 37) {
			var seconds : int = Time.time;
			Shoot(seconds);
		}
	}
}

function Shoot(seconds) {
	var canShoot = (seconds % 2);
	
	if(canShoot) {
		if(time != seconds) {
			var projectile1 = Instantiate(projectilePrefab, 
			                             shoot1.position, 
			                             Quaternion.identity);	

			projectile1.rigidbody.AddForce(shoot1.forward * 20, ForceMode.Impulse);
			
			var projectile2 = Instantiate(projectilePrefab, 
			                             shoot2.position, 
			                             Quaternion.identity);	

			projectile2.rigidbody.AddForce(shoot2.forward * 20, ForceMode.Impulse);
			
			var projectile3 = Instantiate(projectilePrefab, 
			                             shoot3.position, 
			                             Quaternion.identity);	

			projectile3.rigidbody.AddForce(shoot3.forward * 20, ForceMode.Impulse);
		}
		time = seconds;
	}
}