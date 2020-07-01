using UnityEditor;
using UnityEngine;

public class ModelImport : AssetPostprocessor
{
    void OnPostprocessModel(GameObject g)
    {
        Apply(g.transform);
    }

    void Apply(Transform t)
    {
        if (t.name.ToLower().Contains("character"))
        {
            CapsuleCollider collider = t.gameObject.AddComponent<CapsuleCollider>();
            collider.center = new Vector3(0, 0.55f, 0);
            collider.radius = 0.2f;
            collider.height = 1.1f;
            collider.direction = 1;

            Animator animator = t.gameObject.AddComponent<Animator>();
            animator.applyRootMotion = true;
            animator.updateMode = AnimatorUpdateMode.Normal;
            animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
        }

        // Recurse
        foreach (Transform child in t)
            Apply(child);
    }
    /*
    void OnPostprocessGameObjectWithUserProperties(GameObject go, string[] propNames, System.Object[] values)
    {
        for (int i = 0; i < propNames.Length; i++)
        {
            string propName = propNames[i];
            System.Object value = (System.Object)values[i];

            Debug.Log("Propname: " + propName + " value: " + values[i]);
        }
    }
    */
}
