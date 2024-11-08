using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    void Awake()
    {
        if (animator == null){
            Debug.LogWarning("Animator not assigned in LevelManager.");
        }
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    IEnumerator LoadSceneAsync(string sceneName)
    {
        if (animator != null){
            animator.enabled = true;
            animator.SetTrigger("TransitionState");
        }
        yield return new WaitForSeconds(1);
        yield return new WaitForEndOfFrame();
        yield return SceneManager.LoadSceneAsync(sceneName);

        if (Player.Instance != null){
            Player.Instance.transform.position = new Vector2(0, -4.5f);
        }
    }
}
