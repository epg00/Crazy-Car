using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text recordText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Button playButton;
    [SerializeField] private AndroidNoti androidnotification;
    [SerializeField] private IosNoti iosnotification;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDuration;

    private int energy;

    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";
    

    public void Start(){
        OnApplicationFocus(true);
    }

    private void OnApplicationFocus(bool focusStatus) {

        if(!focusStatus){ //if we are closing the app
            return;
        }

        CancelInvoke();
        
    
        int record = PlayerPrefs.GetInt(ScoreHandler.Record,0);
        
        recordText.text = $"Record: {record}";


        

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if(energy == 0){
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
        
            if(energyReadyString == string.Empty){
                return;
            }

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if(DateTime.Now > energyReady){
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }else{
                playButton.interactable = false;
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
            }
        }

        energyText.text = $"Jugar: ({energy})";
    }

    private void EnergyRecharged(){
        playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
        energyText.text = $"Jugar: ({energy})";
    }

    public void Play(){

        if(energy < 1){
            return;
        }

        energy--;

        PlayerPrefs.SetInt(EnergyKey, energy);

        if(energy == 0){
            DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyKey,energyReady.ToString());
#if UNITY_ANDROID
            androidnotification.ScheduleNotificacion(energyReady);
#elif UNITY_IOS
            iosnotification.ScheduleNotificacion(energyRechargeDuration);
#endif
        }        

        SceneManager.LoadScene(1);
    }
}
