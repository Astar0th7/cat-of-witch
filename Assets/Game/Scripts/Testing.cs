using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class Testing : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}