using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIWidgets;

public class EvolutionProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    public float fillSpeed = 0.5f;
    public static int currentEvo;
    public float timer = 0.01f;
    public float max;
    private bool fill = true;
    private bool paused = false;
    private float initialMax = 10;

    private bool keepTiming = true;
    public GameObject notifyPad;
    public Text nextStatusText;
    public Text newStatusText;
    public Text oldStatusText;
    public Text congratsText;
    public Text evolutionLevelText;
    public Text keepStatusText;
    public Text lossStatusText;
    private float timePassed = 0f; 
    private GameObject pet;

    private void Start()
    {
        initialMax = max;
        currentEvo = 1;
        pet = GameObject.FindWithTag("pets");
        Timer();
        //max = 30;


    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
            fill = true;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            fill = false;
            paused = false;
        }

        if (timer < max && keepTiming)
        {
            Timer();
            progressSlider.value = timer / max;
            if (progressSlider.value == 0 && currentEvo != 1)
                ResetTimer();
        }
        else if (currentEvo == 4 && fill != false)
        {
            timer = max - 0.01f;
            paused = true;
        }
        else
        {
            ResetTimer();
            ChangeSize();
        }
    }

    public void ChangeSize()
    {
        if (pet != null)
        {
            Vector3 size = new Vector3(0.2f, 0.2f, 0.2f);
            if (fill == true)
                pet.transform.localScale = (currentEvo + 1) * size;
            else if (fill == false)
                pet.transform.localScale = 1 * size;
            Timer();
        }
        else
        {
            pet = GameObject.FindWithTag("pets");
        }
    }

    public void Timer()
    {
        if (fill == true && !paused)
            timer += Time.deltaTime;
        else if (fill == false && !paused)
            timer -= Time.deltaTime;
        if (timer < 0)
            timer = 0f;
    }

    public void PauseTimer()
    {
        paused = true;
        keepStatusText.text = "Keep Level: " + currentEvo;
        lossStatusText.text = "Lose Level: " + (currentEvo + 1);
    }

    public void ResumeTimer()
    {
        paused = false;
    }

    public void ResetTimer()
    {
        keepTiming = false;
        notifyPad.SetActive(true);

        GameObject statusPanel = notifyPad.transform.GetChild(2).gameObject;
        GameObject increaseImage = statusPanel.transform.GetChild(0).gameObject;
        GameObject decreaseImage = statusPanel.transform.GetChild(1).gameObject;

        if (fill == true)
        {
            progressSlider.value = 0f;
            increaseImage.SetActive(true);
            decreaseImage.SetActive(false);
            newStatusText.text = "Level " + (currentEvo + 1);
            oldStatusText.text = "Level " + currentEvo;
            congratsText.text = "Congratulations! Your pet has evolved to level " + (currentEvo + 1) + "!";
            if (currentEvo >= 2)
            {
                nextStatusText.text = "Congrats you've reached max level!";
            }
            else
                nextStatusText.text = "Next Evolution Level: " + (currentEvo + 2);
        }
        else if (fill == false)
        {
            progressSlider.value = max;
            increaseImage.SetActive(false);
            decreaseImage.SetActive(true);
            newStatusText.text = "Level " + currentEvo;
            if (currentEvo > 0)
            {
                oldStatusText.text = "Level " + (currentEvo - 1);
                congratsText.text = "Oh no! Your pet has lost all its levels! Your pet is now level 1!";
            }
            nextStatusText.text = "Next Evolution Level: 2";
        }
    }

    public void ClosePanel()
    {
        keepTiming = true;
        notifyPad.SetActive(false);

        if (fill == true)
        {
            max *= 2;
            timer = 0.01f;
            if (currentEvo < 4)
                currentEvo++;
        }
        else if (fill == false)
        {
            max = initialMax;
            timer = 0;
            currentEvo = 1;
        }
        evolutionLevelText.text = "Level " + currentEvo;
    }
}
