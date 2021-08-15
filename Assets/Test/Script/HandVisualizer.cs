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

        // Constructor and initial values
        fingers getFingers = new fingers();
        int gesture = 0;
        int flag = 0;

        #endregion

        #region Private members

        HandPipeline _pipeline;
        (Material keys, Material region) _material;

        // Anchor points
        private static Vector3 thumb1;
        private static Vector3 thumb2;
        private static Vector3 thumb3;
        private static Vector3 thumb4;
        private static Vector3 index1;
        private static Vector3 index2;
        private static Vector3 index3;
        private static Vector3 index4;
        private static Vector3 middle1;
        private static Vector3 middle2;
        private static Vector3 middle3;
        private static Vector3 middle4;
        private static Vector3 ring1;
        private static Vector3 ring2;
        private static Vector3 ring3;
        private static Vector3 ring4;
        private static Vector3 pinky1;
        private static Vector3 pinky2;
        private static Vector3 pinky3;
        private static Vector3 pinky4;

        #endregion

        #region MonoBehaviour implementation

        void Start()
        {
            //MediaPipe pipeline
            _pipeline = new HandPipeline(_resources);
            //Hand Visualization
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
            

            // Get the positions of the hand
            thumb1 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Thumb1);
            thumb2 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Thumb2);
            thumb3 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Thumb3);
            thumb4 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Thumb4);
            index1 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Index1);
            index2 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Index2);
            index3 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Index3);
            index4 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Index4);
            middle1 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Middle1);
            middle2 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Middle2);
            middle3 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Middle3);
            middle4 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Middle4);
            ring1 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Ring1);
            ring2 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Ring2);
            ring3 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Ring3);
            ring4 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Ring4);
            pinky1 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Pinky1);
            pinky2 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Pinky2);
            pinky3 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Pinky3);
            pinky4 = _pipeline.GetKeyPoint(HandPipeline.KeyPoint.Pinky4);

            gesture = getFingers.getPosition(thumb4,thumb3,thumb2,thumb1,index4,index3,index2,index1,middle4,middle3,middle2,middle1,ring4,ring3,ring2,ring1,pinky4,pinky3,pinky2,pinky1);
            Debug.Log("POSITION-> " + gesture);
            if (gesture == 1){
                //Pinky out
                flag = 1;
            }

            if (flag == 1){
                if (gesture == 2){
                    // "Peace" sign, index and middle up
                    playFire(_pipeline.GetKeyPoint(HandPipeline.KeyPoint.Index4));
                }
                else if (gesture == 3){
                    // Pinky and Thumb out
                    playLightning(_pipeline.GetKeyPoint(HandPipeline.KeyPoint.Pinky4));
                }
                else if (gesture == 4){ 
                    // Pinky and Index Up
                    playSnow(_pipeline.GetKeyPoint(HandPipeline.KeyPoint.Index4));
                }
                else if (gesture == 5){
                    // "OK" sign; Index and thumb circle
                    playLightning(_pipeline.GetKeyPoint(HandPipeline.KeyPoint.Pinky4));
                    playFire(_pipeline.GetKeyPoint(HandPipeline.KeyPoint.Thumb4));
                }
                else if (gesture == 0){
                    // Fist: resting state
                }
                else if (gesture == -1){
                    // Open palm: Reset state
                    flag = 0;
                }
            }


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

        #endregion
    
        public void playLightning(Vector3 index)
        {
            //start lighting in location of pinky
            Lightning.transform.position = index;
            Lightning.Play();
        }

        public void playFire(Vector3 index)
        {
            //start fire in location of index finger
            Fire.transform.position = index;
            Fire.Play();
        }

        public void playSnow(Vector3 index)
        {
            //start snow
            Snow.Play();
        }
    
    }

} // namespace MediaPipe.HandPose
