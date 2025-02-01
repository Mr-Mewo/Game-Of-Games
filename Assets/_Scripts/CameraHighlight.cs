using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace _Scripts {
	public class CameraHighlight : MonoBehaviour {
		private Transform  _highlight;
		// private Transform  _selection;
		private RaycastHit _raycastHit;
		private Camera _camera;

		[HideInInspector]
		public bool canHighlight;

		private ComputerGame _computerGame;
		
		private RectTransform   _uiCrosshair;
		private TextMeshProUGUI _uiTittle;
		private TextMeshProUGUI _uiDescription;
		private TextMeshProUGUI _uiCreator;
		
		private Vector3 _targetPosition;
		
		private void Start() {
			_camera = Camera.main;
			
			_uiCrosshair = GameObject.Find("Canva/UI_crosshair").GetComponent<RectTransform>();
			_uiTittle = GameObject.Find("Canvas/UI_Title").GetComponent<TextMeshProUGUI>();
			_uiDescription = GameObject.Find("Canvas/UI_Description").GetComponent<TextMeshProUGUI>();
			_uiCreator = GameObject.Find("Canvas/UI_Creator").GetComponent<TextMeshProUGUI>();
		}

		private void Update() {
			
			StartComputerGame();
			if (_computerGame) return;
			
			if (_highlight) {
				_highlight.gameObject.GetComponent<Outline>().enabled = false;
				_highlight = null;
			}
			Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            
			if (!Physics.Raycast(ray, out _raycastHit)) return;
            
			_highlight = _raycastHit.transform;
			if (_highlight.CompareTag($"Selectable")/* && _highlight != _selection*/) {
				if (_highlight.gameObject.GetComponent<Outline>()) {
					_highlight.gameObject.GetComponent<Outline>().enabled = true;
				}
				else {
					Outline outline = _highlight.gameObject.AddComponent<Outline>();
					outline.enabled = true;
					outline.OutlineColor= Color.yellow;
					outline.OutlineWidth = 7f;
				}
				
				ComputerGame tempComputerGame = _highlight.gameObject.GetComponent<ComputerGame>();
				// _uiCrosshair.Text = tempComputerGame.T
				_uiTittle.text = tempComputerGame.title;
				_uiDescription.text = tempComputerGame.description;
				_uiCreator.text = tempComputerGame.creator;

				if (Input.GetMouseButtonDown(0)) {
					_computerGame = _highlight.gameObject.GetComponent<ComputerGame>();
					if (!_computerGame) return;

					_computerGame.Activate();

					GetComponent<CameraMovement>().enabled = false;
					GetComponent<CameraHighlight>().enabled = false;

					Vector3 offset = new(0, -3f, 1.47725f);
					_targetPosition = _highlight.transform.position +
					                  (_highlight.transform.right * offset.x +
					                   _highlight.transform.up * offset.y +
					                   _highlight.transform.forward * offset.z);
				}
				
			}
			else {
				_highlight = null;
			}

		}

		private void StartComputerGame() {
			if (!_computerGame) return;

			transform.position = _targetPosition; // Vector3.Slerp(_camera.transform.position, _targetPosition, 0.1f);
			transform.LookAt(_highlight.transform.position + new Vector3(0, 1.47725f, 0));
		}
	}
}