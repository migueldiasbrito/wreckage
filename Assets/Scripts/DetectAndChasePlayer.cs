using UnityEngine;

namespace mdb
{
	public class DetectAndChasePlayer : MonoBehaviour
	{
		public WalkingEnemy walkingEnemy;
		public float distance = 5;
		public float startPoint = 0.6f;
		public float chaseFor = 1;

		private float timer = 0;
		private int mask;

		private void Start()
		{
			mask = ~(1 << gameObject.layer);
		}

		void Update()
		{
			if (CanSeePlayer(Vector3.left))
			{
				walkingEnemy.walkDirection = WalkingEnemy.WalkDirections.Left;
				timer = chaseFor;
			}
			else if (CanSeePlayer(Vector3.right))
			{
				walkingEnemy.walkDirection = WalkingEnemy.WalkDirections.Right;
				timer = chaseFor;
			}
			else if (timer <= 0)
			{
				walkingEnemy.walkDirection = WalkingEnemy.WalkDirections.None;
			}
			else
			{
				timer -= Time.deltaTime;
			}
		}

		private bool CanSeePlayer(Vector3 direction)
		{
			Vector3 start = transform.position + startPoint * direction;
			Vector3 end = start + distance * direction;

			RaycastHit2D hit = Physics2D.Linecast(start, end, mask);

			return hit.collider != null && hit.collider.tag == "Player";
		}
	}
}
