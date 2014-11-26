using UnityEngine;
using System.Collections;
using System;

namespace PTG_ParticleLights {

[RequireComponent(typeof(ParticleSystem))]
public class ParticleLights : MonoBehaviour {
	
	
	public class ParticleLightInfo
	{
		public Vector3 		position;
		public Color   		color;
		public float   		maxIntensity;
		public float		lightRange;
		public LightShadows	lightShadow;
		public GameObject	senderGameObject;
		public float		lastDistanceToCamera;
		
		
		public ParticleLightInfo(Vector3 _position, Color _color, float _maxIntensity, float _lightRange, LightShadows _shadows, GameObject _gameObject)
		{
			position 				= _position;
			color 					= _color;
			maxIntensity 			= _maxIntensity;
			lightRange 				= _lightRange;
			lightShadow 			= _shadows;
			senderGameObject 		= _gameObject;
			lastDistanceToCamera 	= 0;
			
		}	
	}
	
	// Tha max intensity of the light
	public float							lightMaxIntensity = 1;
	
	// The light range
	public float							lightRange = 10;
	
	// The color of the particles over lifetime. Must be set manually
	public Gradient							colorOverLifeTime;
	
	//Tha maximum number of alive particles at the same time
	public int								maxParticles;
	
	//The type of shadows for the particles
	public LightShadows						lightShadow;
	
	//Use only one light in the transform position
	public bool								oneLightOnly;
	
	//Use the first spawned particule or choose other different each frame
	[HideInInspector]
	public bool								useFirstParticle;
	
	
	private ParticleSystem 					myParticleSystem;
	private ParticleSystem.Particle[]		particles;
	private ParticleLightsManager			particleLightManager;
	private Transform						myTransform;
	
	// Use this for initialization
	void Awake () {
		
		//Cache vars
		myParticleSystem 		= particleSystem;
		myTransform 			= transform;
		particleLightManager 	= GameObject.FindObjectOfType(typeof(ParticleLightsManager)) as ParticleLightsManager;
		particles = new  ParticleSystem.Particle[maxParticles];
	}
	
	// Update is called once per frame
	void LateUpdate () {
		int particlesAlive = myParticleSystem.GetParticles(particles);
		
		if (particlesAlive > 0) 
		{
			if (oneLightOnly) //When usign just one light
			{
				int particleToSample = 0;
				
				if (!useFirstParticle)
					particleToSample = UnityEngine.Random.Range(0, particlesAlive);
				
				float normalizedLifetime = 1 - particles[particleToSample].lifetime/particles[particleToSample].startLifetime;  //Get the current life time normalized 
				ParticleLightInfo tempParticleLightInfo = new ParticleLightInfo(myTransform.position, particles[particleToSample].color*colorOverLifeTime.Evaluate(normalizedLifetime), lightMaxIntensity,lightRange, lightShadow, gameObject); //Configure particle info
				particleLightManager.AddParticleToQueque(tempParticleLightInfo); //Send particles to manager
			}
				
			else //When n lights
			{
				
				for (int i = 0; i<particlesAlive; i++)
				{
					float normalizedLifetime = 1 - particles[i].lifetime/particles[i].startLifetime; //Get the current life time normalized 
					ParticleLightInfo tempParticleLightInfo = new ParticleLightInfo(myTransform.TransformPoint(particles[i].position),particles[i].color*colorOverLifeTime.Evaluate(normalizedLifetime), lightMaxIntensity,lightRange, lightShadow, gameObject); //Configure particle info
					particleLightManager.AddParticleToQueque(tempParticleLightInfo); //Send particles to manager
				}
			}
		}
	}
	
	void OnDisable()
	{
		particleLightManager.DisableParticleLights(gameObject);	
	}
	
}
}
