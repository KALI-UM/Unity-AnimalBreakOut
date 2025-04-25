// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using UnityEngine;


namespace AmazingAssets.CurvedWorld
{
    [AddComponentMenu("Amazing Assets/Curved World/Camera")]
    [ExecuteAlways]
    [RequireComponent(typeof(Camera))]
    public class CurvedWorldCamera : MonoBehaviour
    {
        public enum MatrixType { Perspective, Orthographic }


        public MatrixType matrixType = MatrixType.Perspective;
        [Range(1, 179)]
        public float fieldOfView = 60;
        public float size = 5;
        public bool nearClipPlaneSameAsCamera = true;
        public float nearClipPlane = 0.3f;

        Camera activeCamera;

        public bool drawGizmos;
        

        private void OnEnable()
        {
            if (activeCamera == null)
                activeCamera = GetComponent<Camera>();
        }
        private void OnDisable()
        {
            if (activeCamera != null)
                activeCamera.ResetCullingMatrix();
        }
        private void Start()
        {
            if (activeCamera == null)
                activeCamera = GetComponent<Camera>();
        }
        private void Update()
        {
            if (activeCamera == null)
                activeCamera = GetComponent<Camera>();

            if (activeCamera == null)
            {
                enabled = false;
                return;
            }


            if (nearClipPlane >= activeCamera.farClipPlane)
                nearClipPlane = activeCamera.farClipPlane - 0.01f;


            if (matrixType == MatrixType.Perspective)
            {
                fieldOfView = Mathf.Clamp(fieldOfView, 1, 179);
                activeCamera.cullingMatrix = Matrix4x4.Perspective(fieldOfView, 1, activeCamera.nearClipPlane, activeCamera.farClipPlane) * activeCamera.worldToCameraMatrix;
            }
            else
            {
                size = size < 1 ? 1 : size;
                activeCamera.cullingMatrix = Matrix4x4.Ortho(-size, size, -size, size, (nearClipPlaneSameAsCamera ? activeCamera.nearClipPlane : nearClipPlane), activeCamera.farClipPlane) * activeCamera.worldToCameraMatrix;
            }
        }
        private void Reset()
        {
            if (activeCamera != null)
            {
                activeCamera.ResetCullingMatrix();

                fieldOfView = activeCamera.fieldOfView;
                size = activeCamera.orthographicSize;
                nearClipPlane = activeCamera.nearClipPlane;
            }
        }

        private void OnDrawGizmos()
        {
            if (activeCamera == null || drawGizmos == false)
                return;


            Gizmos.color = Color.magenta * 0.85f;

            if (matrixType == MatrixType.Perspective)
            {
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawFrustum(Vector3.zero, fieldOfView, activeCamera.farClipPlane, activeCamera.nearClipPlane, 1);
            }
            else
            {
                //Top
                Vector3 c = transform.position + transform.forward * (nearClipPlaneSameAsCamera ? activeCamera.nearClipPlane : nearClipPlane);

                Vector3 t1 = c + transform.up * size - transform.right * size;
                Vector3 t2 = c + transform.up * size + transform.right * size;
                Vector3 t3 = c - transform.up * size - transform.right * size;
                Vector3 t4 = c - transform.up * size + transform.right * size;

                Gizmos.DrawLine(t1, t2);
                Gizmos.DrawLine(t2, t4);
                Gizmos.DrawLine(t3, t4);
                Gizmos.DrawLine(t3, t1);


                //Bottom
                c = transform.position + transform.forward * activeCamera.farClipPlane;

                Vector3 b1 = c + transform.up * size - transform.right * size;
                Vector3 b2 = c + transform.up * size + transform.right * size;
                Vector3 b3 = c - transform.up * size - transform.right * size;
                Vector3 b4 = c - transform.up * size + transform.right * size;

                Gizmos.DrawLine(b1, b2);
                Gizmos.DrawLine(b2, b4);
                Gizmos.DrawLine(b3, b4);
                Gizmos.DrawLine(b3, b1);


                //Connection
                Gizmos.DrawLine(t1, b1);
                Gizmos.DrawLine(t2, b2);
                Gizmos.DrawLine(t3, b3);
                Gizmos.DrawLine(t4, b4);
            }
        }
    }
}
