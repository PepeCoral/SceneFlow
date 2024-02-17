using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace SceneFlow { 

    public class SceneFlowManager : MonoBehaviour
    {
        public static SceneFlowManager Instance { get; private set;}

        private void Awake()
        {

            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;
        }

        [SerializeField] private Animator animator;

        /// <summary>
        /// LoadScene facilitates smooth scene transitions in Unity, playing an animation transition before loading the specified scene.  
        /// </summary>
        /// <param sceneName="Scene name">Scene Name.</param>
        public void LoadScene(string sceneName){
            StartCoroutine(LoadSceneRoutine(sceneName));
        }

        /// <summary>
        /// LoadScene facilitates smooth scene transitions in Unity, playing an animation transition before loading the specified scene.  
        /// </summary>
        /// <param sceneBuildIndex="Scene Build Index">Scene Name.</param>
        public void LoadScene(int sceneBuildIndex){
            StartCoroutine(LoadSceneRoutine(sceneBuildIndex));
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
