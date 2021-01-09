using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneContents : MonoBehaviour
{
    public static SceneContents Instance { get; private set; }

    [SerializeField] public List<PlayerCharacter> PlayerCharacters = new List<PlayerCharacter>();
    [SerializeField] public List<Enemy> Enemies = new List<Enemy>();

    private void Awake() {
        if (Instance == null) Instance = this;
    }

    public void AddPlayerCharacter(PlayerCharacter player) {
        PlayerCharacters.Add(player);
        Debug.Log($"PlayerCharacter {player} added to SceneContents");
    }
    
    public void AddEnemy(Enemy enemy) {
        Enemies.Add(enemy);
        Debug.Log($"Enemy {enemy} added to SceneContents");
    }
}
