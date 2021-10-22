using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace mdb
{
	public class DialogueController : MonoBehaviour
	{
		public Text[] LineTextDisplays;
		public GameObject TextBackground;

		[SerializeField]
		private string[] splittedText;
		[SerializeField]
		private int normalSpeed;
		[SerializeField]
		private int fastSpeed;
		[SerializeField]
		private int timeBetweenSplits;

		private int currentDisplayIndex = 0;
		private int currentStrIndex = 0;
		private int currentCharIndex = -1;
		private float timer = 0;
		private bool clearLines = true;
		private bool fast = false;
		private Action callback;

		public void SpeedUp(InputAction.CallbackContext callbackContext)
		{
			if (callbackContext.performed && timer > 0)
			{
				timer = 0;
			}

			fast = !callbackContext.canceled;
			if (timer > fastSpeed / 1000f)
			{
				timer = fastSpeed / 1000f;
			}
		}

		public void DisplayDialogue(string[] texts, Action callback = null)
		{
			splittedText = texts;
			this.callback = callback;

			currentDisplayIndex = 0;
			currentStrIndex = 0;
			currentCharIndex = -1;
			timer = 0;
			clearLines = true;

			foreach (Text text in LineTextDisplays)
			{
				text.gameObject.SetActive(true);
			}
			TextBackground.SetActive(true);
			enabled = true;
		}

		private void Update()
		{
			if (timer <= 0)
			{
				if (clearLines)
				{
					foreach (Text text in LineTextDisplays)
					{
						text.text = "";
					}
					clearLines = false;
				}

				if (currentStrIndex >= splittedText.Length || LineTextDisplays.Length <= 0)
				{
					foreach (Text text in LineTextDisplays)
					{
						text.gameObject.SetActive(false);
					}
					TextBackground.SetActive(false);
					callback?.Invoke();
					enabled = false;
					return;
				}

				currentCharIndex++;
				if (currentCharIndex <= splittedText[currentStrIndex].Length)
				{
					LineTextDisplays[currentDisplayIndex].text = splittedText[currentStrIndex].Substring(0, currentCharIndex);
					timer = fast ? fastSpeed / 1000f : normalSpeed / 1000f;
				}
				else
				{
					currentDisplayIndex++;
					currentStrIndex++;
					currentCharIndex = 0;

					if (currentStrIndex >= splittedText.Length || currentDisplayIndex >= LineTextDisplays.Length)
					{
						currentDisplayIndex = 0;
						timer = fast ? fastSpeed / 1000f : timeBetweenSplits;
						clearLines = true;
					}
					else
					{
						timer = fast ? fastSpeed / 1000f : normalSpeed / 1000f;
					}
				}
			}
			else
			{
				timer -= Time.deltaTime;
			}
		}
	}
}