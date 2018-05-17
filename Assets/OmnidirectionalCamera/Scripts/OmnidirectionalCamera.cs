using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kanonji.Omnidirectional {

	public class OmnidirectionalCamera : MonoBehaviour {

		[SerializeField] private Renderer targetRenderer;
		public Renderer TargetRenderer {
			get { return this.targetRenderer; }
		}

		[SerializeField] private int cubemapSize;
		public int CubemapSize {
			get { return this.cubemapSize; }
		}
		public int EquirectangularWidth {
			get { return this.CubemapSize * 4; }
		}
		public int EquirectangularHeight {
			get { return this.CubemapSize * 2; }
		}

		[SerializeField] private Material converter;
		public Material Converter {
			get { return this.converter; }
		}

		private RenderTexture renderTextureCube;
		public RenderTexture RenderTextureCube {
			get {
				if (null == this.renderTextureCube) {
					this.renderTextureCube = new RenderTexture(this.CubemapSize, this.CubemapSize, 16);
					this.renderTextureCube.isPowerOfTwo = true;
					this.renderTextureCube.isCubemap = true;
					this.renderTextureCube.useMipMap = false;
					this.renderTextureCube.hideFlags = HideFlags.HideAndDontSave;
				}
				return this.renderTextureCube;
			}
			private set { this.renderTextureCube = value; }
		}

		private RenderTexture renderTexture;
		public RenderTexture RenderTexture{
			get { return this.renderTexture ?? (this.renderTexture = new RenderTexture(this.EquirectangularWidth, this.EquirectangularHeight, 16)); }
			private set { this.renderTexture = value; }
		}

		private Camera _camera;
		public Camera Camera {
			get { return this._camera ?? (this._camera = GetComponent<Camera>()); }
		}

		private void Start() {
			this.Init();
		}

		private void Update() {
			if (this.IsCubemapSizeChanged()) this.Init();

			this.Camera.RenderToCubemap(this.RenderTextureCube);
			Graphics.Blit(this.RenderTextureCube, this.RenderTexture, this.Converter);
		}

		private void Init() {
			this.RenderTextureCube.Release();
			this.RenderTextureCube = null;

			this.RenderTexture.Release();
			this.RenderTexture = null;

			this.TargetRenderer.material.mainTexture = this.RenderTexture;
		}

		private bool IsCubemapSizeChanged() {
			return this.RenderTextureCube.width != this.CubemapSize;
		}
	}
}
