using UnityEngine;
using System.Collections;

public class GriswoldController : EnemyController {
	public SpriteAnimation Forward;
	public SpriteAnimation Backward;
	public SpriteAnimation Left;
	public SpriteAnimation Right;
	public SpriteAnimation AttackAnimation;
	public SpriteAnimation Death;
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
	
	void Awake ()
	{
	}
	
	// Use this for initialization
	public override void Start ()
	{
		base.Start();
		motor = GetComponent<CharacterMotor>();
		StartCoroutine("AI");
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		base.Update();
		
		// MAIN CODE
		Vector3 vel = transform.rotation * Vector3.forward * moveSpeed;
		motor.inputMoveDirection = vel;
		
		if (Vector3.Distance(transform.position, player.transform.position) < attackRange && PlayerRelativePosition.z > 0)
		{
			isClose = true;
		} else {
			isClose = false;
		}
		HandleAnimation();
		
	}
	
	IEnumerator AI()
	{
		while (true) 
		{
			while (isClose)
			{
				float attackTime = (AttackAnimation.hitFrame / AttackAnimation.GetFrames()) * AttackAnimation.GetTime();
				Attack();
				yield return new WaitForSeconds(attackTime);
				DealDamage();
				yield return new WaitForSeconds(AttackAnimation.GetTime() - attackTime); // Cooldown to animation speed
				yield return null;
			}
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
		Sprite.BeginAnimation(Death);
		this.enabled = false;
	}
	
	protected void HandleAnimation ()
	{
		if (attacking)
		{
			Sprite.BeginAnimationOnce(AttackAnimation);
			return;
		}
		Sprite.BeginAnimationOnce(FacingSprite());
	}
	
	protected SpriteAnimation FacingSprite()
	{
		if (FaceDirection == Facing.Front)
			return Forward;
		else if (FaceDirection == Facing.Back)
			return Backward;
		else if (FaceDirection == Facing.Left)
			return Right;
		else if (FaceDirection == Facing.Right)
			return Left;
		
		return Forward;
	}
}
