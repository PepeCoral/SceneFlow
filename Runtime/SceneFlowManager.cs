using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace SceneFlow { 

    public class SceneFlowManager : MonoBehaviour
    {
        private static SceneFlowManager Instance { get; set;}

        private void Awake()
        {

            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;
        }

     
        private void OnDisable()
        {
            Instance = null;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        [SerializeField] private Animator animator;

        /// <summary>
        /// LoadScene facilitates smooth scene transitions in Unity, playing an animation transition before loading the specified scene.  
        /// </summary>
        /// <param sceneName="Scene name">Scene Name.</param>
        public void LoadScene(string sceneName){
            try
            {
                Instance.LoadSceneOnInstance(sceneName);
            }
            catch (System.NullReferenceException)
            {
                Debug.LogError("No SceneFlowManager is active");
                throw;
            }
        }

        /// <summary>
        /// LoadScene facilitates smooth scene transitions in Unity, playing an animation transition before loading the specified scene.  
        /// </summary>
        /// <param sceneBuildIndex="Scene Build Index">Scene Name.</param>
        public static void LoadScene(int sceneBuildIndex)
        {
            try
            {
                Instance.LoadSceneOnInstance(sceneBuildIndex);
            }
            catch (System.NullReferenceException)
            {
                Debug.LogError("No SceneFlowManager is active");
                throw;
            }
        }


        private void LoadSceneOnInstance(int sceneBuildIndex)
        {
            StartCoroutine(LoadSceneRoutine(sceneBuildIndex));
        }

        private void LoadSceneOnInstance(string sceneName)
        {
            StartCoroutine(LoadSceneRoutine(sceneName));
        }

        private IEnumerator LoadSceneRoutine(string sceneName) {
            animator.SetTrigger("Start");
            yield return new WaitForSeconds(animationTime());
            SceneManager.LoadScene(sceneName);
        }

        private IEnumerator LoadSceneRoutine(int sceneBuildIndex)
        {
            animator.SetTrigger("Start");
            yield return new WaitForSeconds(animationTime());
            SceneManager.LoadScene(sceneBuildIndex);
        }


        /// <summary>
        /// Calculates the time of the current "Start" clip.  
        /// </summary>
        private float animationTime()
        {
            float time = 1f;
            try
            {
                time = animator.runtimeAnimatorController.animationClips.Where(c => c.name == "Start").First().length;
            }
            catch (System.Exception)
            {
                Debug.LogError("Error: There is no Start clip in the Animator");
                throw;
            }
            return time;
        }
    }

}
