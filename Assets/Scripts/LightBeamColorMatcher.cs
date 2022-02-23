using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamColorMatcher : MonoBehaviour
{
    public Material lightBeamMaterial;
    public Light lightRef;

    // Start is called before the first frame update
    void Start() {
        SetReferences();
        SetColor();
    }

    private void Reset() {
        if(lightRef == null || lightBeamMaterial == null) {
            SetReferences();
        }

        SetColor();
    }

    private void SetReferences() {
        lightBeamMaterial = GetComponent<MeshRenderer>().material;
        lightRef = GetComponentInParent<Light>();
    }

    public void SetColor() {
        lightBeamMaterial.SetColor("_Color", lightRef.color);
    }
}
