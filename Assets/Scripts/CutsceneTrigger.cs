using UnityEngine;

namespace mdb
{
	public class CutsceneTrigger : MonoBehaviour
	{
		public CutsceneController controller;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "Player")
			{
				PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
				Health health = collision.gameObject.GetComponent<Health>();

				if (pc != null) pc.enabled = false;
				if (health != null) health.enabled = false;

				controller.enabled = true;
			}
		}
	}
}