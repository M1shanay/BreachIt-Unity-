using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FOW
{
    [RequireComponent(typeof(Camera))]
    public class FOWImageEffect : MonoBehaviour
    {
        Camera cam;

        //public bool isGL;
        private void Awake()
        {
            //isGL = SystemInfo.graphicsDeviceVersion.Contains("OpenGL");
            cam = GetComponent<Camera>();
            cam.depthTextureMode = DepthTextureMode.Depth | DepthTextureMode.DepthNormals;
        }

        private void OnPreRender()
        {
            if (!FogOfWarWorld.instance)
                return;

            if (!FogOfWarWorld.instance.is2D)
            {
                Matrix4x4 camToWorldMatrix = cam.cameraToWorldMatrix;

                //Matrix4x4 projectionMatrix = renderingData.cameraData.camera.projectionMatrix;
                //Matrix4x4 inverseProjectionMatrix = GL.GetGPUProjectionMatrix(projectionMatrix, true).inverse;

                //inverseProjectionMatrix[1, 1] *= -1;

                FogOfWarWorld.instance.FogOfWarMaterial.SetMatrix("_camToWorldMatrix", camToWorldMatrix);
                //FogOfWarWorld.instance.fowMat.SetMatrix("_inverseProjectionMatrix", inverseProjectionMatrix);
            }
            else
            {
                FogOfWarWorld.instance.FogOfWarMaterial.SetFloat("_cameraSize", cam.orthographicSize);
                FogOfWarWorld.instance.FogOfWarMaterial.SetVector("_cameraPosition", cam.transform.position);
            }
        }
        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (!FogOfWarWorld.instance || !FogOfWarWorld.instance.enabled)
            {
                Graphics.Blit(src, dest);
                return;
            }

            Graphics.Blit(src, dest, FogOfWarWorld.instance.FogOfWarMaterial);
        }
    }
}