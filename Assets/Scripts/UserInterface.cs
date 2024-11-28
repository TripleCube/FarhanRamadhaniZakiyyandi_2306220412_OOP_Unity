using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UserInterface : MonoBehaviour
{
    public Enemy1 Enemy;
    public CombatManager Waves;
    public int Point;
    private void OnEnable(){
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Player1 Player = GetComponent<Player1>();

        IntegerField health = root.Q<IntegerField>("Health");
        IntegerField points = root.Q<IntegerField>("Points");
        IntegerField wave = root.Q<IntegerField>("Wave");
        IntegerField enemiesLeft = root.Q<IntegerField>("EnemiesLeft");
    }
    
}
