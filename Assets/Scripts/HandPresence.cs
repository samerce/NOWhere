using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class HandPresence : SerializedMonoBehaviour {

    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;

    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    [SerializeField] private Dictionary<string, GameObject> controllerPrefabDictionary;
    private InputDevice targetDevice;
    private List<InputDevice> devices = new List<InputDevice>();
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    private void Reset() {
        controllerPrefabDictionary = new Dictionary<string, GameObject>();
    }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(TryInitialize(2.0f));
    }

    private IEnumerator TryInitialize(float delayTime) {
        yield return new WaitForSeconds(delayTime);

        if(spawnedController != null && spawnedHandModel != null) yield break;

        // InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        Debug.Log("Enumerating XR Devices...");
        foreach (var item in devices)
        {
            Debug.Log(item.name);
        }
        Debug.Log("Finished!");

        if(devices.Count > 0) {
            targetDevice = devices[0];
            controllerPrefabDictionary.TryGetValue(targetDevice.name, out GameObject prefab);//controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if(prefab) {
                spawnedController = Instantiate(prefab, transform);
            } else {
                Debug.LogError("Did not find a corresponding controller model.");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        } 
    }

    void UpdateHandAnimation() {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) {
            handAnimator.SetFloat("Trigger", triggerValue);
        } else {
            handAnimator.SetFloat("Trigger", 0);
        }

        if(targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue)) {
            handAnimator.SetFloat("Grip", gripValue);
        } else {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    // Update is called once per frame
    void Update() {
        //StartCoroutine(GetDevices(0.0f));


        if(!targetDevice.isValid) {
            StartCoroutine(TryInitialize(0.0f));
        } else {
            if(showController) {
            spawnedHandModel.SetActive(false);
            spawnedController.SetActive(true);
            } else {
                spawnedController.SetActive(false);
                spawnedHandModel.SetActive(true);
                UpdateHandAnimation();
            }
        }

        // targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        // if(primaryButtonValue) {
        //     Debug.Log("Pressing Primary");
        // }

        // targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        // if(triggerValue > 0.1f) {
        //     Debug.Log("Trigger Pressed " + triggerValue);
        // }

        // targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxistValue);
        // if(primary2DAxistValue != Vector2.zero) {
        //     Debug.Log("Primary Touchpad " + primary2DAxistValue);
        // }
    }
}
