using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PTG_ParticleLights {
	
public class ParticleLightsManager : MonoBehaviour {

	
	public enum LIGHT_POOL_BAHAVIOR 
	{
		CreateNew,
		ReturnNearest
	}
	
	public LIGHT_POOL_BAHAVIOR		poolBehavior;
	
	// Max number of lights at the same time
	public int 		maxLights;
	
	//Number of lights to instantiate in the awake
	public int 		preInstantiatedLights;
	
	//Disable particle lights by distance form camera
	public float		disableByDistance;
	
	
	//When the particle lights will start to fade out
	public float		startFadeOutDistance;
	
	//The camera to check distance 
	public Transform	cameraToCheckDistances;
	
	public class PoolableLight
	{
		public Transform	lightTransform;
		public Light		light;
		public GameObject	parent; //Who the PollableLights Belongs To
		
		
		public PoolableLight()
		{
			GameObject tempGameObject = new GameObject();
			tempGameObject.name = "ParticleLight";
			lightTransform = tempGameObject.transform;
			light = tempGameObject.AddComponent<Light>();
			light.type = LightType.Point;
		}
	}
	
	private List<PoolableLight> 					lightPoolFree = new List<PoolableLight>();
	private List<PoolableLight> 					lightPoolInUse = new List<PoolableLight>();
	private List<ParticleLights.ParticleLightInfo> 	particlesQueque = new List<ParticleLights.ParticleLightInfo>();
	
	private Transform				myTransform;
	
	
	private void Start()
	{
		myTransform = transform;
		if (disableByDistance > 0 )
		{
			if (cameraToCheckDistances == null)	//Try to use the main camera
				cameraToCheckDistances = Camera.main.transform;
			
			if (cameraToCheckDistances == null)
				Debug.LogError("[PARTICLE LIGHTS ERROR] You forgot to assign cameraToCheckDistances");
				
		}
		//Create pre cached lights
		if (preInstantiatedLights > 0)
		{
			for (int i = 0; i< 	preInstantiatedLights; i++)
			 CreateNewLight();
		}
			
	}
	
	private void Update()
	{
		if (lightPoolInUse.Count > 0)
		{
			for (int i = 0; i<lightPoolInUse.Count; i++)
			{
				lightPoolInUse[i].light.enabled = false;
				lightPoolFree.Add(lightPoolInUse[i]);
			}
			lightPoolInUse.Clear(); 
		}
		if (poolBehavior != LIGHT_POOL_BAHAVIOR.CreateNew && particlesQueque.Count > 0)
		{
			SelectParticleLights();
			particlesQueque.Clear();
		}
		
		
	}

		/// <summary>
		/// Gets a light.
		/// </summary>
		/// <returns>
		/// The light.
		/// </returns>
		/// <param name='_poolBehaviour'>
		/// _pool behaviour.
		/// </param>
	public PoolableLight GetLight(LIGHT_POOL_BAHAVIOR _poolBehaviour)
	{
		if (lightPoolFree.Count == 0 && lightPoolInUse.Count == 0)  //There aren't any light in the pool
		{
			return CreateNewLight();
		}
		else if (lightPoolFree.Count == 0 && lightPoolInUse.Count != 0)  //There isn't any free light in the pool
		{
			PoolableLight tempPoolableLight;
			
			if (maxLights != 0)
			{
				if (maxLights > (lightPoolFree.Count + lightPoolInUse.Count))
				{
					tempPoolableLight = CreateNewLight();
				}
				else
					tempPoolableLight = null;
				
			}
			else
			{		
				tempPoolableLight = CreateNewLight();
			}
			
			return tempPoolableLight;
		}
		else  //There is a free lights on the pool
		{
			PoolableLight tempPoolableLight = lightPoolFree[lightPoolFree.Count -1];
			lightPoolFree.RemoveAt(lightPoolFree.Count -1);
			lightPoolInUse.Add(tempPoolableLight);
			return tempPoolableLight;
		}
		
	}
	
	/// <summary>
	/// Creates a new poolable light.
	/// </summary>
	private PoolableLight CreateNewLight()
	{
		PoolableLight tempPoolableLight =  new PoolableLight();
		tempPoolableLight.lightTransform.parent = myTransform;
		lightPoolInUse.Add(tempPoolableLight);
		return tempPoolableLight;
	}
	
	/// <summary>
	/// Returns the light. Not in use
	/// </summary>
	/// <param name='_returnedLight'>
	/// _returned light.
	/// </param>
	public void ReturnLight(PoolableLight _returnedLight)
	{
		lightPoolInUse.Remove(_returnedLight);
		lightPoolFree.Add(_returnedLight);
	}
	
	/// <summary>
	/// Sets the particle poolable light properties
	/// </summary>
	/// <param name='_position'>
	/// _position.
	/// </param>
	/// <param name='_color'>
	/// _color.
	/// </param>
	/// <param name='_intensity'>
	/// _intensity.
	/// </param>
	/// <param name='_range'>
	/// _range.
	/// </param>
	/// <param name='_lightShadows'>
	/// _light shadows.
	/// </param>
	/// <param name='_parent'>
	/// _parent.
	/// </param>
	//public void SetParticleLight(Vector3 _position, Color _color, float _intensity, float _range, LightShadows _lightShadows, GameObject _parent)
	public void SetParticleLight(ParticleLights.ParticleLightInfo _particleInfo)
	{
		float intensityByDistanceModifier = 1;
		
		if (disableByDistance > 0)
		{
			float distanceToCamera = (_particleInfo.position - cameraToCheckDistances.position).sqrMagnitude;
			if (startFadeOutDistance > 0)
				intensityByDistanceModifier = Mathf.Clamp01((disableByDistance - distanceToCamera)/(disableByDistance - startFadeOutDistance));
			
			if (disableByDistance < distanceToCamera )
				return;
		}

		PoolableLight tempLight = GetLight(poolBehavior);
		if (tempLight != null)
		{
			tempLight.light.enabled  = true; 
			tempLight.lightTransform.position = _particleInfo.position;
			tempLight.light.color = _particleInfo.color;
			tempLight.light.range = _particleInfo.lightRange;
			tempLight.parent = _particleInfo.senderGameObject;
			tempLight.light.intensity = _particleInfo.color.a*_particleInfo.maxIntensity*intensityByDistanceModifier;
			tempLight.light.shadows = _particleInfo.lightShadow;		
		}
	}
	
	/// <summary>
	/// Adds the particle to queque, so it can be reorder by distance
	/// </summary>
	/// <param name='_particleInfo'>
	/// _particle info.
	/// </param>
	public void AddParticleToQueque(ParticleLights.ParticleLightInfo _particleInfo)
	{
		//Reorder the particles so the furthest form the camera is last
		if (poolBehavior == LIGHT_POOL_BAHAVIOR.CreateNew)
		{
			SetParticleLight(_particleInfo);
			return;
		}
		
		float currentParticleDistance = (_particleInfo.position - cameraToCheckDistances.position).sqrMagnitude;
		_particleInfo.lastDistanceToCamera = currentParticleDistance;
		
		if (particlesQueque.Count == 0)
		{
			particlesQueque.Add(_particleInfo);
		}
		else
		{
			if (cameraToCheckDistances != null)
			{
				
				bool addLast = true;
				
				for (int i = 0; i < particlesQueque.Count; i++)
				{
					
					if (currentParticleDistance < particlesQueque[i].lastDistanceToCamera)
					{
						particlesQueque.Insert(i, _particleInfo);
						addLast = false;
						break;
					}
				}
				
				if (addLast)
					particlesQueque.Add(_particleInfo);
			}
			else
			{
				Debug.LogError("[Particle lights ERROR] You have to assign a camera to check distances");	
			}
		}
	}
	
	
	/// <summary>
	/// Disables all poolable lights form a particle system
	/// </summary>
	public void DisableParticleLights(GameObject _target)
	{
		for (int i = 0; i<lightPoolInUse.Count; i++)
		{
			if (lightPoolInUse[i].parent = _target)
			{
				if (lightPoolInUse[i].light != null)
					//lightPoolInUse[i].light.gameObject.SetActive(false);
					lightPoolInUse[i].light.enabled = false; //Decreased overhead
			}
		}
	}
	
	
	private void SelectParticleLights()
	{
		for (int i = 0; i<Mathf.Min(maxLights,particlesQueque.Count); i++)
		{
			SetParticleLight(particlesQueque[i]);
		}
	}
	
	//Draw distance gizmos
	private void  OnDrawGizmosSelected()
	{
		if (cameraToCheckDistances != null && disableByDistance > 0) 
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(cameraToCheckDistances.position,Mathf.Sqrt(disableByDistance));
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(cameraToCheckDistances.position,Mathf.Sqrt(startFadeOutDistance));
		}
	}
	
	
}
}
