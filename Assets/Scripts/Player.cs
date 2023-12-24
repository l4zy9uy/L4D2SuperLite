using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public int HP = 100;
    public GameObject BloodyScreen;
    //public TextMeshProUGUI playerHealthUI;
    public GameObject gameOverUI;
    private StarterAssets playerControls;

    public bool isDead;

    public ProgressBar healthBar;
    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        playerControls = new StarterAssets();
    }

    private void Start()
    {
        //playerHealthUI.text = $"Health: {HP}";
    }

    public void takeDamage(int damageAmount)
    {
        HP -= damageAmount;
        healthBar.UpdateValue(HP);
        if (HP <= 0)
        {
            print("Player is dead");
            PlayerDead();
            isDead = true;
            StartCoroutine(DelayedLoadScene("GameOverScene", 3.5f));
            playerControls.Disable();
        }
        else
        {
            print("Player hit");
            StartCoroutine(bloodyScreenEffect());
            //playerHealthUI.text = $"Health: {HP}";
        }
    }

    public void PlayerDead()
    {
        // die animation
        GetComponentInChildren<Animator>().enabled = true;
        /*GetComponent<ScreenFader>().StartFade();
        StartCoroutine(ShowGameOverUI());
        InputManager.SetCursorState(false);*/
    }

    private IEnumerator DelayedLoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
        Debug.Log("Scene Loaded");
    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        gameOverUI.gameObject.SetActive(true);
    }

    private IEnumerator bloodyScreenEffect()
    {
        if (BloodyScreen.activeInHierarchy == false)
        {
            BloodyScreen.SetActive(true);
        }

        yield return new WaitForSeconds(4f);
        var image = BloodyScreen.GetComponentInChildren<Image>();

        if (image != null)  // Kiểm tra xem image có tồn tại không
        {
            Color startColor = image.color;
            startColor.a = 1f;
            image.color = startColor;

            float duration = 3f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

                Color newColor = image.color;
                newColor.a = alpha;

                image.color = newColor;  // Gán giá trị mới cho image.color

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            Debug.LogError("Image component not found in bloodyScreen.");
        }

        if (BloodyScreen.activeInHierarchy)
        {
            BloodyScreen.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            if (isDead == false)
            {
                Debug.Log("attacked!");
                var hand = other.gameObject.GetComponent<ZombieHand>();
                takeDamage(hand.damage);
            }
        }
    }
}
