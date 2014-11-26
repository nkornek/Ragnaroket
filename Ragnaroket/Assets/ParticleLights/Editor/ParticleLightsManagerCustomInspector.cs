using UnityEngine;
using System.Collections;
using UnityEditor;
using PTG_ParticleLights;

[CustomEditor(typeof(ParticleLightsManager))]
public class ParticleLightsManagerCustomInspector : Editor {

	private ParticleLightsManager particleLightsManager;
	
	void OnEnable()
	{
		particleLightsManager = target as ParticleLightsManager;
	}
	
	
	public override void OnInspectorGUI ()
	{
		
		EditorGUIUtility.LookLikeInspector();
		
		particleLightsManager.poolBehavior = (ParticleLightsManager.LIGHT_POOL_BAHAVIOR)EditorGUILayout.EnumPopup(new GUIContent("Pool behavior", "How to create lights"), particleLightsManager.poolBehavior);
		
		if (particleLightsManager.poolBehavior == ParticleLightsManager.LIGHT_POOL_BAHAVIOR.CreateNew)
		{
			EditorGUILayout.HelpBox("CREATE NEW will create as many lights as needed", MessageType.Warning);
			particleLightsManager.maxLights = 0;
		}
		else
			particleLightsManager.maxLights = EditorGUILayout.IntField(new GUIContent("Max lights", "Max number of lights that will be used"), particleLightsManager.maxLights);	
			
		if (particleLightsManager.maxLights > 0)
			particleLightsManager.preInstantiatedLights = EditorGUILayout.IntField(new GUIContent("Pre instantiated lights", "Instantiated this amount of lights at the beggining"),Mathf.Clamp(Mathf.Min(particleLightsManager.preInstantiatedLights, particleLightsManager.maxLights), 0, int.MaxValue));
		else
			particleLightsManager.preInstantiatedLights = EditorGUILayout.IntField(new GUIContent("Pre instantiated lights", "Instantiated this amount of lights at the beggining"),particleLightsManager.preInstantiatedLights);
		
		particleLightsManager.disableByDistance = EditorGUILayout.FloatField(new GUIContent("Disable by distance", "Disable the lights when are further than this distance in SQR magnitude. 0 will not disable them by distance"), particleLightsManager.disableByDistance);
		
		if (particleLightsManager.disableByDistance > 0)
		{
			
			EditorGUILayout.BeginVertical("box");
			
			particleLightsManager.startFadeOutDistance 		= EditorGUILayout.Slider(new GUIContent("Start fadeout distance", "Start fading out the lights when reaching this distance"), particleLightsManager.startFadeOutDistance, 0, particleLightsManager.disableByDistance);
			particleLightsManager.cameraToCheckDistances 	= EditorGUILayout.ObjectField(new GUIContent("Camera to check distances", "The camera used to check the distance with the particles"), particleLightsManager.cameraToCheckDistances, typeof(Transform), true) as Transform;
			
			if (particleLightsManager.cameraToCheckDistances == null)
				EditorGUILayout.HelpBox("Helper distance spheres are shown only if you assign a camera", MessageType.Info);
			
			
			
			EditorGUILayout.EndVertical();
		}
		
		
		if (GUI.changed)
			EditorUtility.SetDirty(particleLightsManager);
	}	
	
	
	public  void OnSceneGUI()
	{
		if (particleLightsManager.cameraToCheckDistances != null && particleLightsManager.disableByDistance > 0)
		{
			Handles.Label(particleLightsManager.cameraToCheckDistances.position+Vector3.right*Mathf.Sqrt(particleLightsManager.disableByDistance), "Disable");
			Handles.Label(particleLightsManager.cameraToCheckDistances.position+Vector3.right*Mathf.Sqrt(particleLightsManager.startFadeOutDistance), "Start Fade Out");
		}
	}

}
