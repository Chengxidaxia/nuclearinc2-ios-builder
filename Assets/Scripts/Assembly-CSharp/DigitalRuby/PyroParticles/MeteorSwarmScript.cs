using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

namespace DigitalRuby.PyroParticles
{
	public class MeteorSwarmScript : FireBaseScript, ICollisionHandler
	{
		[Tooltip("The game object prefab that represents the meteor.")]
		public GameObject MeteorPrefab;

		[Tooltip("Explosion particle system that should be emitted for each initial collision.")]
		public ParticleSystem MeteorExplosionParticleSystem;

		[Tooltip("Shrapnel particle system that should be emitted for each initial collision.")]
		public ParticleSystem MeteorShrapnelParticleSystem;

		[Tooltip("A list of materials to use for the meteors. One will be chosen at random for each meteor.")]
		public Material[] MeteorMaterials;

		[Tooltip("A list of meshes to use for the meteors. One will be chosen at random for each meteor.")]
		public Mesh[] MeteorMeshes;

		[Tooltip("The destination radius")]
		public float DestinationRadius;

		[Tooltip("The source of the meteor swarm (in the sky somewhere usually)")]
		public Vector3 Source;

		[Tooltip("The source radius")]
		public float SourceRadius;

		[Tooltip("The time it should take the meteors to impact assuming a clear path to destination.")]
		public float TimeToImpact = 1f;

		[SingleLine("How many meteors should be emitted per second (min and max)")]
		public RangeOfIntegers MeteorsPerSecondRange = new RangeOfIntegers
		{
			Minimum = 5,
			Maximum = 10
		};

		[SingleLine("Scale multiplier for meteors (min and max)")]
		public RangeOfFloats ScaleRange = new RangeOfFloats
		{
			Minimum = 0.25f,
			Maximum = 1.5f
		};

		[SingleLine("Maximum life time of meteors in seconds (min and max).")]
		public RangeOfFloats MeteorLifeTimeRange = new RangeOfFloats
		{
			Minimum = 4f,
			Maximum = 8f
		};

		[Tooltip("Array of emission sounds. One will be chosen at random upon meteor creation.")]
		public AudioClip[] EmissionSounds;

		[Tooltip("Array of explosion sounds. One will be chosen at random upon impact.")]
		public AudioClip[] ExplosionSounds;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private MeteorSwarmCollisionDelegate CollisionDelegate__BackingField;

		private float elapsedSecond = 1f;

		[HideInInspector]
		public event MeteorSwarmCollisionDelegate CollisionDelegate
		{
			add
			{
				MeteorSwarmCollisionDelegate meteorSwarmCollisionDelegate = CollisionDelegate__BackingField;
				MeteorSwarmCollisionDelegate meteorSwarmCollisionDelegate2;
				do
				{
					meteorSwarmCollisionDelegate2 = meteorSwarmCollisionDelegate;
					meteorSwarmCollisionDelegate = Interlocked.CompareExchange(ref CollisionDelegate__BackingField, (MeteorSwarmCollisionDelegate)Delegate.Combine(meteorSwarmCollisionDelegate2, value), meteorSwarmCollisionDelegate);
				}
				while ((object)meteorSwarmCollisionDelegate != meteorSwarmCollisionDelegate2);
			}
			remove
			{
				MeteorSwarmCollisionDelegate meteorSwarmCollisionDelegate = CollisionDelegate__BackingField;
				MeteorSwarmCollisionDelegate meteorSwarmCollisionDelegate2;
				do
				{
					meteorSwarmCollisionDelegate2 = meteorSwarmCollisionDelegate;
					meteorSwarmCollisionDelegate = Interlocked.CompareExchange(ref CollisionDelegate__BackingField, (MeteorSwarmCollisionDelegate)Delegate.Remove(meteorSwarmCollisionDelegate2, value), meteorSwarmCollisionDelegate);
				}
				while ((object)meteorSwarmCollisionDelegate != meteorSwarmCollisionDelegate2);
			}
		}

		private IEnumerator SpawnMeteor()
		{
			float delay = UnityEngine.Random.Range(0f, 1f);
			yield return new WaitForSeconds(delay);
			Vector3 src = Source + UnityEngine.Random.insideUnitSphere * SourceRadius;
			GameObject meteor = UnityEngine.Object.Instantiate(MeteorPrefab);
			float scale = UnityEngine.Random.Range(ScaleRange.Minimum, ScaleRange.Maximum);
			meteor.transform.localScale = new Vector3(scale, scale, scale);
			meteor.transform.position = src;
			Vector3 dest = base.gameObject.transform.position + UnityEngine.Random.insideUnitSphere * DestinationRadius;
			dest.y = 0f;
			Vector3 dir = dest - src;
			Vector3 vel = dir / TimeToImpact;
			Rigidbody r = meteor.GetComponent<Rigidbody>();
			r.velocity = vel;
			float xRot = UnityEngine.Random.Range(-90f, 90f);
			float yRot = UnityEngine.Random.Range(-90f, 90f);
			float zRot = UnityEngine.Random.Range(-90f, 90f);
			r.angularVelocity = new Vector3(xRot, yRot, zRot);
			r.mass *= scale * scale;
			Renderer renderer = meteor.GetComponent<Renderer>();
			renderer.sharedMaterial = MeteorMaterials[UnityEngine.Random.Range(0, MeteorMaterials.Length)];
			meteor.transform.parent = base.gameObject.transform;
			meteor.GetComponent<FireCollisionForwardScript>().CollisionHandler = this;
			Mesh mesh = MeteorMeshes[UnityEngine.Random.Range(0, MeteorMeshes.Length - 1)];
			meteor.GetComponent<MeshFilter>().mesh = mesh;
			TrailRenderer t = meteor.GetComponent<TrailRenderer>();
			t.startWidth = UnityEngine.Random.Range(2f, 3f) * scale;
			t.endWidth = UnityEngine.Random.Range(0.25f, 0.5f) * scale;
			t.time = UnityEngine.Random.Range(0.25f, 0.5f);
			if (EmissionSounds != null && EmissionSounds.Length != 0)
			{
				AudioSource component = meteor.GetComponent<AudioSource>();
				if (component != null)
				{
					int num = UnityEngine.Random.Range(0, EmissionSounds.Length);
					AudioClip clip = EmissionSounds[num];
					component.PlayOneShot(clip, scale);
				}
			}
		}

		private void SpawnMeteors()
		{
			int num = UnityEngine.Random.Range(MeteorsPerSecondRange.Minimum, MeteorsPerSecondRange.Maximum);
			for (int i = 0; i < num; i++)
			{
				StartCoroutine(SpawnMeteor());
			}
		}

		protected override void Update()
		{
			base.Update();
			if (Duration > 0f && (elapsedSecond += Time.deltaTime) >= 1f)
			{
				elapsedSecond--;
				SpawnMeteors();
			}
		}

		private void PlayCollisionSound(GameObject obj)
		{
			if (ExplosionSounds != null && ExplosionSounds.Length != 0)
			{
				AudioSource component = obj.GetComponent<AudioSource>();
				if (!(component == null))
				{
					int num = UnityEngine.Random.Range(0, ExplosionSounds.Length);
					AudioClip clip = ExplosionSounds[num];
					component.PlayOneShot(clip, obj.transform.localScale.x);
				}
			}
		}

		private IEnumerator CleanupMeteor(float delay, GameObject obj)
		{
			yield return new WaitForSeconds(delay);
			UnityEngine.Object.Destroy(obj.GetComponent<Collider>());
			UnityEngine.Object.Destroy(obj.GetComponent<Rigidbody>());
			UnityEngine.Object.Destroy(obj.GetComponent<TrailRenderer>());
		}

		public void HandleCollision(GameObject obj, Collision col)
		{
			Renderer component = obj.GetComponent<Renderer>();
			if (!(component == null))
			{
				if (CollisionDelegate__BackingField != null)
				{
					CollisionDelegate__BackingField(this, obj);
				}
				Vector3 vector;
				Vector3 forward;
				if (col.contacts.Length == 0)
				{
					vector = obj.transform.position;
					forward = -vector;
				}
				else
				{
					vector = col.contacts[0].point;
					forward = col.contacts[0].normal;
				}
				MeteorExplosionParticleSystem.transform.position = vector;
				MeteorExplosionParticleSystem.transform.rotation = Quaternion.LookRotation(forward);
				MeteorExplosionParticleSystem.Emit(UnityEngine.Random.Range(10, 20));
				MeteorShrapnelParticleSystem.transform.position = col.contacts[0].point;
				MeteorShrapnelParticleSystem.Emit(UnityEngine.Random.Range(10, 20));
				PlayCollisionSound(obj);
				UnityEngine.Object.Destroy(component);
				StartCoroutine(CleanupMeteor(0.1f, obj));
				UnityEngine.Object.Destroy(obj, 4f);
			}
		}
	}
}
