using UnityEngine;

namespace mdb
{
	public class DetectAndShootPlayer : MonoBehaviour
	{
		public WalkingEnemy walkingEnemy;
		public SpriteRenderer spriteRenderer;
		public float distance = 5;
		public float startPoint = 0.6f;
		public GameObject blast;
		public float recoil = 1;

		private bool shooting = false;
		private float timer;
		private int mask;

		private void Start()
		{
			mask = ~(1 << gameObject.layer);
		}

		void Update()
		{
			bool canSeePlayer = CanSeePlayer();

			if(shooting && !canSeePlayer)
			{
				walkingEnemy.walkDirection = spriteRenderer.flipX ? WalkingEnemy.WalkDirections.Right : WalkingEnemy.WalkDirections.Left;
				shooting = false;
			}
			else if (!shooting && canSeePlayer)
			{
				walkingEnemy.walkDirection = WalkingEnemy.WalkDirections.None;
				shooting = true;
			}

			if (timer <= 0)
			{
				if (shooting)
				{
					Vector3 direction = spriteRenderer.flipX ? Vector3.right : Vector3.left;
					Vector3 start = transform.position + (startPoint + walkingEnemy.moveSpeed * Time.deltaTime) * direction;
					GameObject newBlast = Instantiate(blast, start, Quaternion.identity);
					newBlast.layer = gameObject.layer;

					BlastController bc = newBlast.GetComponent<BlastController>();
					bc.speed *= direction.x;

					timer = recoil;
				}
			}
			else
			{
				timer -= Time.deltaTime;
			}
		}

		private bool CanSeePlayer()
		{
			Vector3 direction = spriteRenderer.flipX ? Vector3.right : Vector3.left;
			Vector3 start = transform.position + startPoint * direction;
			Vector3 end = start + distance * direction;

			RaycastHit2D hit = Physics2D.Linecast(start, end, mask);

			return hit.collider != null && hit.collider.tag == "Player";
		}
	}
}
