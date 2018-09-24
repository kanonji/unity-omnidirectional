using UnityEngine;

namespace Kanonji.Omnidirectional {

	public class RotateCamera : MonoBehaviour {

		[SerializeField]
		private Camera proveCamera;
		public Camera ProveCamera {
			get { return this.proveCamera; }
			private set { this.proveCamera = value; }
		}

		[SerializeField]
		private float distance;
		public float Distance {
			get { return this.distance; }
			set { this.distance = value; }
		}

		private void Update() {
			var sensitivity = 0.1f;
			this.Distance += Input.GetAxis("Mouse ScrollWheel") * sensitivity * -1;
			this.Distance = Mathf.Clamp(this.Distance, 0, 1);
			this.transform.localPosition = Vector3.zero;
			this.transform.rotation = this.ProveCamera.transform.rotation;
			this.transform.Translate(new Vector3(0, 0, -this.distance));
		}
	}
}
