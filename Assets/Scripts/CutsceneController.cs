using UnityEngine;
using UnityEngine.SceneManagement;

namespace mdb
{
	public class CutsceneController : MonoBehaviour
	{
		[System.Serializable]
		private struct CutscenePart
		{
			public Animator[] animators;
			public string[] text;
			public int ttl;
		}

		public DialogueController dialogueController;

		[SerializeField]
		private CutscenePart[] parts;
		[SerializeField]
		private string nextScene;
		private int currentPart = -1;
		private bool hasTtl;
		private float timer;

		public void NextPart()
		{
			currentPart++;

			if (currentPart >= parts.Length)
			{
				if (nextScene != null)
				{
					SceneManager.LoadScene(nextScene);
				}
				return;
			}

			foreach (Animator animator in parts[currentPart].animators)
			{
				animator.enabled = true;
			}

			if (parts[currentPart].text.Length > 0 && dialogueController != null)
			{
				dialogueController.DisplayDialogue(parts[currentPart].text, NextPart);
			}

			timer = parts[currentPart].ttl;
			hasTtl = timer > 0;
		}

		void Start()
		{
			NextPart();
		}

		void Update()
		{
			if (hasTtl)
			{
				if (timer <= 0)
				{
					NextPart();
				}
				else
				{
					timer -= Time.deltaTime;
				}
			}
		}
	}
}