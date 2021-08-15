# ensc482

To run the program:
1. Download the entire "ensc482-main" folder
2. Open the folder as a project in Unity. In Unity click "ADD" on the top right. Our system requires version "2020.3.6f1"
4. In Unity, double click on the file "Assets/482Visualization" in the file manager on the bottom
5. Hit the run button at the top of Unity. This will access the webcam and start the program

To start an animation, you must first use Gesture 1, then you can go to any of the animation gestures (Gestures 2-5). If you need to use Gesture 0
to transition, that works and will not confused the system. If Gesture -1 is used, the system will be reset and will not accept animations until
Gesture 1 is shown again.

To use the program, we have 7 different gestures:
- Gesture -1: Open hand, all fingers up
- Gesture 0: Fist, all fingers down
- Guesture 1: Closed hand with only pinky up
- Gesture 2: Index and Middle finger up, the rest down
- Gesture 3: Thumb and Pinky up, the rest down
- Gesture 4: Index and Pinky up, the rest down
- Gesture 5: Pinky, Ring, and Middle up, the rest down

What each gesture does:
- Gesture -1: Resets the system, no animations will occur until specified
- Gesture 0: Neutral state, does nothing
- Gesture 1: Initial signal required to start an animation
- Gesture 2: Fire animation where the hand is
- Gesture 3: Lightning animation where the hand is
- Gesture 4: Snow animation
- Gesture 5: Lightning and fire animation where the hand is

Animations were created locally in Unity.

ensc482/Assets/Test/Script/FingerPosition.cs
This is a C# file we wrote in order to determine what hand gesture is being used, based on the locations of the anchors placed on the hands and fingers.

ensc482/Assets/Test/Script/HandVisualizer.cs
This is the main file we used for running and connecting the parts together in the program. Here we call to get a hand gesture that is seen, and based
on the result, will execute various animations, or not allow animations to be triggered.
