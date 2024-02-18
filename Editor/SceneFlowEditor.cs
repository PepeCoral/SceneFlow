using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.IO;

public class SceneFlowEditor : MonoBehaviour
{
    [MenuItem("Assets/Create/SceneFlow Transition")]
    static void CreateController()
    {

       
        //Get Active path
        string currentPath = AssetDatabase.GetAssetPath(Selection.activeObject);

        // Creates the controller
        var controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(currentPath+ Path.DirectorySeparatorChar + "Transition.controller");

        // Add parameters
        controller.AddParameter("Start", AnimatorControllerParameterType.Trigger);

        // Add StateMachines
        var rootStateMachine = controller.layers[0].stateMachine;

        // Add States
        var startState = rootStateMachine.AddState("Start");
        var endState = rootStateMachine.AddState("End");
        ; // don’t add an entry transition, should entry to state by default

        // Add Transitions
        rootStateMachine.defaultState = endState;

        var transition = endState.AddTransition(startState);
        transition.AddCondition(AnimatorConditionMode.If, 0, "Start");
        transition.hasExitTime = false;
        transition.duration = 0;

        AnimationClip startClip = new AnimationClip();
        AssetDatabase.CreateAsset(startClip, currentPath + Path.DirectorySeparatorChar +"Start.anim");

        AnimationClip endClip = new AnimationClip();
        AssetDatabase.CreateAsset(endClip, currentPath + Path.DirectorySeparatorChar + "End.anim");

        startState.motion = startClip;
        endState.motion = endClip;

    }


   

}