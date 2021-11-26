using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Movement))]
public class MovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Movement movement = (Movement)target;

        if (movement.rb != null)
        {
            movement.rb.velocity = EditorGUILayout.Vector3Field("Velocity", movement.rb.velocity);
        }
    }

}