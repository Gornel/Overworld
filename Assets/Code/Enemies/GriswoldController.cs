using UnityEngine;
using System.Collections;

public class GriswoldController : EnemyController {

	protected Animator animator;
	public AudioClip attackSound;
	public float turningDelay = 5f;
	public float normalSpeed = 0.2f;
	public float aggroDistance = 15f;
	public float attackRange = 2f;
	public int attackDamage = 1;
	public float IQ = 5;
	private float moveSpeed = 0;
	private bool attacking = false;
	private CharacterMotor motor;
	private bool isClose = false;
	protected bool facing_front = false;
	protected bool facing_back = false;
	protected bool facing_left = false;
	protected bool facing_right = false;
	
	void Awake ()
	{
	}
	
	// Use this for initialization
	public override void Start ()
	{
		base.Start();
		motor = GetComponent<CharacterMotor>();
		animator = GetComponent<Animator>();
		StartCoroutine("AI");
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		base.Update();
		
		//rotate sprite towards player
		Vector3 vel = transform.rotation * Vector3.forward * moveSpeed;
		motor.inputMoveDirection = vel;
		//decide if sprite is in attack range
		if (Vector3.Distance(transform.position, player.transform.position) < attackRange && PlayerRelativePosition.z > 0)
		{
			isClose = true;
		} else {
			isClose = false;
		}
		//decide what animation should be playing
		HandleAnimation();
		
	}
	
	IEnumerator AI()
	{
		while (true) 
		{
			/*while (isClose)
			{
				//float attackTime = (AttackAnimation.hitFrame / AttackAnimation.GetFrames()) * AttackAnimation.GetTime();
				Attack();
				//yield return new WaitForSeconds(attackTime);
				DealDamage();
				//yield return new WaitForSeconds(AttackAnimation.GetTime() - attackTime); // Cooldown to animation speed
				yield return null;
			}*/
			attacking = false;
			// If no target
			if (Vector3.Distance(transform.position, player.transform.position) > aggroDistance)
			{
				// Randomize movement
				moveSpeed = Random.Range(0, normalSpeed / 2);
				transform.Rotate(Vector3.up, 90f);
				yield return new WaitForSeconds(Random.Range(1f, 3f));
				
			} else {
				moveSpeed = normalSpeed; // Max speed
				
				// Get the angle towards the player
				float playerAngle = Vector3.Angle( new Vector3(player.transform.position.x, 0, player.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z),
													transform.forward);
				// Chance to walk away
				float runAway = Random.Range(0, IQ);
				
				// Walk in opposite direction if stuck or if dumb enough
				if (runAway <= 1 || (motor.movement.velocity.magnitude < (normalSpeed / 2f)))
				{
					transform.Rotate(Vector3.up, playerAngle + Random.Range(-180f,180f));
					yield return new WaitForSeconds(Random.Range(0,0.5f));
				}
				// Turn to face player
				if (PlayerRelativePosition.x < 0) {
					transform.Rotate(Vector3.up, -playerAngle / turningDelay);
				} else {
					transform.Rotate(Vector3.up, playerAngle / turningDelay);
				}
				// Turn step time
				yield return new WaitForSeconds(0.1f);
			}
			
			
		}
	}
	protected void Attack ()
	{
		attacking = true;
		moveSpeed = 0;
		audio.pitch = Random.Range(0.9f,1.1f);
		audio.PlayOneShot(attackSound);
	}

	protected void DealDamage()
	{
		if (isClose)
		{
			player.SendMessage("HurtPlayer", attackDamage);
		}
	}
	
	protected void Kill ()
	{
		StopAllCoroutines();
		Destroy(motor);
		Destroy(collider);
		Destroy (transform.Find("HitBox").gameObject);
		animator.SetBool("death", true);
		this.enabled = false;
	}
	
	protected void HandleAnimation ()
	{
		if (attacking)
		{
			animator.SetBool("attack_forward", true);
			return;
		}
		facing_front = false;
		facing_back = false;
		facing_left = false;
		facing_right = false;

		if (FaceDirection == Facing.Front)
			animator.SetBool("facing_front", true);
		else if (FaceDirection == Facing.Back)
			animator.SetBool("facing_back", true);
		else if (FaceDirection == Facing.Left)
			animator.SetBool("facing_left", true);
		else if (FaceDirection == Facing.Right)
			animator.SetBool("facing_right", true);
	}
}
