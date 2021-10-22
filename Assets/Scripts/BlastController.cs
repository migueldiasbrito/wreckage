using UnityEngine;

namespace mdb
{
	public class BlastController : MonoBehaviour
	{
		public float speed = 10;

		private void Update()
		{
			transform.position += speed * Time.deltaTime * Vector3.right;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.layer != gameObject.layer)
			{
				Destroy(this.gameObject);
			}
		}
	}
}