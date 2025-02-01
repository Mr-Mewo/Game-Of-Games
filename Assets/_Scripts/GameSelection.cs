using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _Scripts {
	[RequireComponent(typeof(Camera))]
	public class GameSelection : MonoBehaviour {
		private Outline _outlineScript;
		private GameObject _currentObject;
		
		private ComputerGame _computerGame;
		
		private Vector3 _targetPosition;
		private CameraMovement _cameraMovement;
		
		[HideInInspector]
		public bool inGame;
		private GameObject _gameSelected;

		[Tooltip("Time to wait in Lerp functions.")]
		public float lerpTime = 0.5f;
		
		[Header("Reference to the UI elements.")]
		public RectTransform uiCrosshair;
		public TextMeshProUGUI uiTittle;
		public TextMeshProUGUI uiDescription;
		public TextMeshProUGUI uiCreator;
		
		private Coroutine _fadeCoroutine;

		private void Start() {
			_cameraMovement = GetComponent<CameraMovement>();
			inGame = false;
		}

		private void Update() {
			if ( !inGame ) {
				if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity)) {
					GameObject hitObject = hit.collider.gameObject;

					if (hitObject == _currentObject) goto label2;
					RemoveOutline();

					if (!hitObject.CompareTag($"Selectable")) goto label1;

					if (!(_computerGame = hitObject.GetComponent<ComputerGame>())) goto label1;
					AddOutline(hitObject);
					_gameSelected = hitObject;

					// if (_computerGame.sceneCamera) {
						uiTittle.text = _computerGame.title;
						uiDescription.text = _computerGame.description;
						uiCreator.text = _computerGame.creator;
					// }

				}
				else goto label1;
				
				goto label2;
				label1:
				_gameSelected = null;
				RemoveOutline();
			}

			label2:
			if ( inGame ) {
				transform.position = Vector3.Slerp(transform.position, _targetPosition, 0.1f);

				if (!_gameSelected) return;
				transform.LookAt(_gameSelected.transform.position + new Vector3(0, 1.47725f, 0));

				if (Input.GetKeyDown(KeyCode.Escape)) {
					inGame = false;

					_cameraMovement.enabled = true;
					_cameraMovement.playerTransform.gameObject.SetActive(true);

					_gameSelected.GetComponent<ComputerGame>().OnDestroy();
					_gameSelected = null;
					
					Cursor.lockState = CursorLockMode.Locked;
				}

				return;
			}
			
			if ( _gameSelected ) {
				if (Input.GetMouseButtonDown(0)) {
					inGame = true;

					_cameraMovement.enabled = false;
					_cameraMovement.playerTransform.gameObject.SetActive(false);

					Vector3 offset = new(0, -3f, 1.47725f);
					_targetPosition = _gameSelected.transform.position +
					                  (_gameSelected.transform.right * offset.x +
					                   _gameSelected.transform.up * offset.y +
					                   _gameSelected.transform.forward * offset.z);
					
					_computerGame.Activate();
					RemoveOutline();
				}
			}

			
		}

		private void AddOutline(GameObject targetObject) {
			if (!targetObject) return;
			if (!targetObject.GetComponent<Outline>()) {
				_outlineScript = targetObject.AddComponent<Outline>();
				
				_outlineScript.enabled = true;
				_outlineScript.OutlineColor = Color.yellow;
				_outlineScript.OutlineWidth = 7f;
			}

			_currentObject = targetObject;

			StartTextboxFade(true);
		}

		private void RemoveOutline() {
			if (!_currentObject || !_outlineScript) return;
			// Remove the outline script
			// _outlineScript.OnDisable();
			Destroy(_outlineScript);

			_currentObject = null;
			_outlineScript = null;
			_computerGame = null;
			
			StartTextboxFade(false);
		}

		private void StartTextboxFade(bool fadeIn) {
			if (_fadeCoroutine != null) {
				StopCoroutine(_fadeCoroutine);
			}

			_fadeCoroutine = StartCoroutine(FadeTextbox(fadeIn));
		}

		private IEnumerator FadeTextbox(bool fadeIn) {
			float elapsedTime = 0f;

			Color color;
			
			while (elapsedTime < lerpTime) {
				elapsedTime += Time.deltaTime;

				color = uiTittle.color;
				color.a = Mathf.Lerp(uiTittle.color.a, fadeIn ? 1f : 0f, elapsedTime / lerpTime);
				uiTittle.color = color;
				uiDescription.color = color;
				uiCreator.color = color;

				yield return null;
			}

			// Ensure the final alpha is set precisely
			color = uiTittle.color;
			color.a = fadeIn ? 1f : 0f;
			uiTittle.color = color;
			uiDescription.color = color;
			uiCreator.color = color;
		}
		
	}
}