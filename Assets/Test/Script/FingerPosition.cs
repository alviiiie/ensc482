using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MediaPipe.HandPose{      // Returns an int indicating a preset number of hand positions

    class fingers{
        // All of the finger coordinates
        private static Vector3 p1;
        private static Vector3 p2;
        private static Vector3 p3;
        private static Vector3 p4;
        private static Vector3 p5;
        private static Vector3 p6;
        private static Vector3 p7;
        private static Vector3 p8;
        private static Vector3 p9;
        private static Vector3 p10;
        private static Vector3 p11;
        private static Vector3 p12;
        private static Vector3 p13;
        private static Vector3 p14;
        private static Vector3 p15;
        private static Vector3 p16;
        private static Vector3 p17;
        private static Vector3 p18;
        private static Vector3 p19;
        private static Vector3 p20;

        public int getPosition(Vector3 ps1, Vector3 ps2, Vector3 ps3, Vector3 ps4, Vector3 ps5, Vector3 ps6, Vector3 ps7, Vector3 ps8, Vector3 ps9, Vector3 ps10, Vector3 ps11, Vector3 ps12, Vector3 ps13, Vector3 ps14, Vector3 ps15, Vector3 ps16, Vector3 ps17, Vector3 ps18, Vector3 ps19, Vector3 ps20){
            // Update position arguments
            p1 = ps1;
            p2 = ps2;
            p3 = ps3;
            p4 = ps4;
            p5 = ps5;
            p6 = ps6;
            p7 = ps7;
            p8 = ps8;
            p9 = ps9;
            p10 = ps10;
            p11 = ps11;
            p12 = ps12;
            p13 = ps13;
            p14 = ps14;
            p15 = ps15;
            p16 = ps16;
            p17 = ps17;
            p18 = ps18;
            p19 = ps19;
            p20 = ps20;


            if ((!thumb()) && (!index()) && (!middle()) && (!ring())&& (!pinky())){        // fist
                return 0;       // Should ignore 0
            }
            else if((!thumb()) && (!index()) && (!middle()) && (!ring())&& (pinky())){       // Trigger
                return 1;
            }
            else if((!thumb()) && (index()) && (middle()) && (!ring())&& (!pinky())){         // Gesture 1
                return 2;
            }
            else if((thumb()) && (!index()) && (!middle()) && (!ring())&& (pinky())){         // Gesture 2
                return 3;
            }
            else if((!thumb()) && (index()) && (!middle()) && (!ring())&& (pinky())){        // Gesture 3
                return 4;
            }
            else if((!thumb()) && (!index()) && (middle()) && (ring())&& (pinky())){          // Gesture 4
                return 5;
            }
            else{
                return -1;                  // Not a recognized gesture
            }
            
        }

        static bool thumb(){        // Gets the thumb state (up or down)
            if (p1.x < p2.x ){
                return false;
            }
            else{
                return true;
            }
        }

        static bool index(){        // Gets the index state (up or down)
            if ((p8.y < p7.y) && (p7.y < p6.y)){
                return true;
            }
            else{ 
                return false;
            }
        }

        static bool middle(){        // Gets the middle state (up or down)
            if ((p12.y < p11.y) && (p11.y < p10.y)){
                return true;
            }
            else{ 
                return false;
            }
        }

        static bool ring(){        // Gets the ring state (up or down)
            if ((p16.y < p15.y) && (p15.y < p14.y)){
                return true;
            }
            else{
                return false;
            } 
        }

        static bool pinky(){        // Gets the pinky state (up or down)
            if ((p20.y < p19.y) && (p19.y < p18.y)){
                return true;
            }
            else{ 
                return false;
            }
        }
    }
}

