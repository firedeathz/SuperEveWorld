using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]
public class RigidBodyPhysics : MonoBehaviour {

	public float speed = 10.0F;
	public float rotateSpeed = 3.0F;
	public float gravity = 10.0F;
	public float maxVelocityChange = 10.0F;
	private bool canJump = true;
	public float jumpHeight = 2.0F;
	private bool grounded = false;
	public AudioClip jumpSound; 
	private Collision obj;

	void Awake ()
	{
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}
	
	void FixedUpdate ()
	{
		if(!HUD._waitingToDie) {
			transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

			if (grounded)
			{
				// Calculate how fast we should be moving
				Vector3 targetVelocity = new Vector3(0, 0, Input.GetAxis("Vertical"));
				targetVelocity = transform.TransformDirection(targetVelocity);
				targetVelocity *= speed;
				
				// Calculate a force that attempts to reach our target velocity
				Vector3 velocity = rigidbody.velocity;
				Vector3 velocityChange = (targetVelocity - velocity);

				velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
				velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
				velocityChange.y = 0;
				
				//Apply the force
				rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
				
				// Jump
				if (canJump && Input.GetButton("Jump"))
				{
					audio.PlayOneShot(jumpSound);
					rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				}
			}
			
			// We apply gravity manually for more tuning control
			rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
			
			grounded = false;
		}
		else {
			// Disable all movement and make the player disappear (Has to be done here because Die is static
			rigidbody.isKinematic = true;
			gameObject.transform.localScale = new Vector3(0,0,0);

			//Prevent dying twice at the same time
			StartCoroutine(FixPlayer());
		}
	}

	IEnumerator FixPlayer() {
		yield return new WaitForSeconds (2);
		if(HUD._waitingToDie) {
			Respawn();
		}
	}

	void Respawn() {
		HUD._dead = false;
		HUD._waitingToDie = false;
		gameObject.transform.localScale = new Vector3(2,2,2);
		gameObject.rigidbody.isKinematic = false;
		gameObject.transform.position = HUD._checkpoint;
		gameObject.transform.rotation = Quaternion.identity;
		GameObject.Find("MainCamera").transform.rotation = Quaternion.identity;
	}
	
	void OnCollisionStay (Collision other)
	{
		if(other.gameObject.tag != "Wall") {
			grounded = true;	
		}	
	}
	
	float CalculateJumpVerticalSpeed ()
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}
