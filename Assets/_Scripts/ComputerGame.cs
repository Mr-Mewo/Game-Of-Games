using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _Scripts {
    public class ComputerGame : MonoBehaviour {
        private static readonly int BaseMap     = Shader.PropertyToID("_BaseMap");
        private static readonly int EmissionMap = Shader.PropertyToID("_EmissionMap");

        [Header("Scene Settings")]
        [Tooltip("The scene to load.")]
        public SceneAsset sceneAsset;
    
        [Tooltip("Screen resolution.")]
        public Vector2Int resolution = new(1920, 1080);
    
        public  Camera        sceneCamera;
        private RenderTexture _renderTexture;
        private MeshRenderer  _meshRenderer;
        
        private bool    _active;
        private Vector3 _targetPosition;
        
        [Header("Text to show")]
        [Tooltip("Title.")]
        public string title = "Default Title";
        [Tooltip("Creator.")]
        public string creator = "Default Creator";
        [Tooltip("Description.")]
        public string description = "Default Description";
        
        private void Start() {
            EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            eventSystem.enabled = false;
        }
        
        public void Activate() {
            if (!sceneAsset) return;
        
            _renderTexture = new RenderTexture( resolution.x, resolution.y, 24 );
            _renderTexture.Create();
        
            Renderer objectRenderer = GetComponent<Renderer>();
            if (objectRenderer == null) {
                Debug.LogWarning("Renderer not found: ");
                return;
            }
        
            _meshRenderer = GetComponent<MeshRenderer>();

            // _meshRenderer.materials[1].mainTexture = _renderTexture; // _renderTexture;
            _meshRenderer.materials[1].SetTexture(BaseMap, _renderTexture);
            _meshRenderer.materials[1].EnableKeyword("_EMISSION");
            _meshRenderer.materials[1].SetTexture(EmissionMap, _renderTexture); // _renderTexture);
        
            LoadSceneAndSetupCamera();

            _active = true;
        }

        private void LoadSceneAndSetupCamera() {
            SceneManager.LoadScene(sceneAsset.name, LoadSceneMode.Additive);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene loadedScene, LoadSceneMode mode) {
            if (loadedScene.name != sceneAsset.name) return;
        
            ChangeLayerOfSceneObjects( loadedScene, LayerMask.NameToLayer($"Another Scene"));
        
            sceneCamera = FindSceneCamera(loadedScene);

            if (sceneCamera != null) {
                sceneCamera.targetTexture = _renderTexture;
            }
        }

        private static Camera FindSceneCamera(Scene scene) {
            foreach (GameObject rootGameObject in scene.GetRootGameObjects()) {
                Camera camera = rootGameObject.GetComponent<Camera>();
                if (camera != null) {
                    camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Default")); // See all layers except "Default"

                    camera.GetComponent<AudioListener>().enabled = false;
                    camera.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;
                    
                    return camera;
                }
            }
            Debug.LogWarning("No camera found on scene: " + scene.name);
            return null;
        }

        public void OnDestroy() {
            if (_renderTexture) {
                _renderTexture.Release();
                Destroy(_renderTexture);
            }

            if (sceneCamera) {
                sceneCamera.targetTexture = null;
            }

            // string scenes = "";
            // for (int i = 0; i < SceneManager.sceneCount; i++) {
            //     scenes += i + " - " + SceneManager.GetSceneAt(i) + " - ";
            // }
            //
            // Debug.Log("A: " + scenes);
            if (!sceneAsset) return;
            SceneManager.UnloadSceneAsync(sceneAsset.name);
            
        }
    
        private void ChangeLayerOfSceneObjects(Scene scene, int newLayer) {
            GameObject[] rootObjects = scene.GetRootGameObjects();

            foreach (GameObject rootObject in rootObjects) {
                SetLayerRecursively(rootObject, newLayer);
            }

            // Debug.Log($"All objects in the scene are now set to layer {newLayer}.");
        }

        private void SetLayerRecursively(GameObject obj, int newLayer) {
            obj.layer = newLayer;

            foreach (Transform child in obj.transform) {
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }
    }
}
