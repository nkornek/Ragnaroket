using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Reflection;
using PTG_ParticleLights;
using System;

namespace PTG_ParticleLights{

[CustomEditor(typeof(ParticleLights))]
public class ParticleLightCustomInspector : Editor {

	public override void OnInspectorGUI()
	{
		EditorGUIUtility.LookLikeInspector();
		DrawDefaultInspector();
		
		
		ParticleLights particleLights = target as ParticleLights;
		
		SerializedObject particleLightsSerializedObject = new SerializedObject(particleLights.gameObject.GetComponent<ParticleSystem>());
		
		
		if (particleLights.oneLightOnly)
		{
			EditorGUILayout.BeginVertical("box");
			
			particleLights.useFirstParticle = EditorGUILayout.Toggle(new GUIContent("Use first Particle", "Use the first spawned particle for color and life"), particleLights.useFirstParticle);
			
			EditorGUILayout.EndVertical();
		}

		
		if (GUILayout.Button("Sync With Particles"))
		{
			//TODO: UNITY guys forgot to make public the variable "gradientValue"...
			/*
			SerializedProperty colorOverLifetime = particleLightsSerializedObject.FindProperty("ColorModule.gradient.maxGradient");
			particleLights.colorOverLifeTime = colorOverLifetime.gradientValue;
			*/
			
			SerializedProperty maxParticles = particleLightsSerializedObject.FindProperty("InitialModule.maxNumParticles");
			particleLights.maxParticles = maxParticles.intValue;
			
		}
		
		// Check if there is a particle manager in the scene
		ParticleLightsManager particleLightsManager = GameObject.FindObjectOfType(typeof(ParticleLightsManager)) as ParticleLightsManager;
		
		if(particleLightsManager == null)
				EditorGUILayout.HelpBox("There must be a ParticleLightsManager in the scene", MessageType.Warning);
			
		if (GUI.changed)
				EditorUtility.SetDirty(particleLights);
		
	}
	
}
}