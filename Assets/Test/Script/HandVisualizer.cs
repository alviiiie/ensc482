using UnityEngine;
using UnityEngine.UI;

namespace MediaPipe.HandPose {

    public sealed class HandVisualizer : MonoBehaviour
    {
        #region Editable attributes

        [SerializeField] WebcamInput _webcam = null;
        [Space]
        [SerializeField] ResourceSet _resources = null;
        [SerializeField] Shader _keyPointShader = null;
        [SerializeField] Shader _handRegionShader = null;
        [Space]
        [SerializeField] RawImage _mainUI = null;

        [SerializeField] ParticleSystem Lightning = null;
        [SerializeField] ParticleSystem Snow = null;
        [SerializeField] ParticleSystem Fire = null;

        //[SerializeField] RawImage _cropUI = null;

        #endregion

        #region Private members

        HandPipeline _pipeline;
        (Material keys, Material region) _material;

        #endregion

        #region MonoBehaviour implementation

        void Start()
        {
            _pipeline = new HandPipeline(_resources);
            _material = (new Material(_keyPointShader),
                         new Material(_handRegionShader));

            // Material initial setup
            _material.keys.SetBuffer("_KeyPoints", _pipeline.KeyPointBuffer);
            _material.region.SetBuffer("_Image", _pipeline.HandRegionCropBuffer);

        }

        void OnDestroy()
        {
            _pipeline.Dispose();
            Destroy(_material.keys);
            Destroy(_material.region);
        }

        void LateUpdate()
        {
            // Feed the input image to the Hand pose pipeline.
            _pipeline.ProcessImage(_webcam.Texture);

            // UI update
            _mainUI.texture = _webcam.Texture;
            Debug.Log("POSITION-> " + getPosition());

            if(getPosition() == 1)
                playLightning(_pipeline.GetKeyPoint(HandPipeline.KeyPoint.Index4));
            else if (getPosition() == 2)
                playFire(_pipeline.GetKeyPoint(HandPipeline.KeyPoint.Index4));
            else if (getPosition() == 3)
                playSnow(_pipeline.GetKeyPoint(HandPipeline.KeyPoint.Index4));


        }

        void OnRenderObject()
        {
            // Key point circles
            _material.keys.SetPass(0);
            Graphics.DrawProceduralNow(MeshTopology.Triangles, 96, 21);

            // Skeleton lines
            _material.keys.SetPass(1);
            Graphics.DrawProceduralNow(MeshTopology.Lines, 2, 4 * 5 + 1);
        }

            #endregion


        #region gesture positions
        int getPosition()
        {
            if (!(thumb()) && !(index()) && !(middle()) && !(ring()) && !(pinky()))
            {        // fist
                return 0;       // Should ignore 0
            }
            else if (!(thumb()) && !(index()) && !(middle()) && !(ring()) && (pinky()))
            {       // Trigger
                return 1;
            }
            else if (!(thumb()) && (index()) && (middle()) && !(ring()) && !(pinky()))
            {         // Gesture 1
                return 2;
            }
            else if ((thumb()) && !(index()) && !(middle()) && !(ring()) && (pinky()))
            {         // Gesture 2
                return 3;
            }
            else if (!(thumb()) && (index()) && !(middle()) && !(ring()) && (pinky()))
            {        // Gesture 3
                return 4;
            }
            else if (!(thumb()) && !(index()) && (middle()) && (ring()) && (pinky()))
            {          // Gesture 4
                return 5;
            }
            else
            {
                return -1;
            }
        }

        public bool thumb()
        {
            Vector3 p4 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Thumb4);
            Vector3 p3 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Thumb3);
            if (p4.x < p3.x)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        bool index()
        {
            Vector3 p8 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Index4);
            Vector3 p7 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Index3);
            Vector3 p6 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Index2);
            if ((p8.y < p7.y) && (p7.y < p6.y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool middle()
        {
            Vector3 p12 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Middle4);
            Vector3 p11 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Middle3);
            Vector3 p10 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Middle2);
            if ((p12.y < p11.y) && (p11.y < p10.y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool ring()
        {
            Vector3 p16 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Ring4);
            Vector3 p15 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Ring3);
            Vector3 p14 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Ring2);
            if ((p16.y < p15.y) && (p15.y < p14.y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool pinky()
        {
            Vector3 p20 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Pinky4);
            Vector3 p19 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Pinky3);
            Vector3 p18 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Pinky2);
            if ((p20.y < p19.y) && (p19.y < p18.y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    
        public void playLightning(Vector3 index)
        {
            Lightning.transform.position = index;
            Lightning.Play();
        }

        public void playFire(Vector3 index)
        {
            Fire.transform.position = index;

            Fire.Play();
        }

        public void playSnow(Vector3 index)
        {

            Snow.Play();
        }
    
    }

} // namespace MediaPipe.HandPose
