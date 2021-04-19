using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    List<Player> players;
    public List<string> PlayersName;
    bool someoneDead = false;
    int turnPlayer = 0;
    bool defaultPlayers = true;
    int bullet = 0;
    SpriteRenderer ImagenUI;

    public TextMeshProUGUI UITurnoText;
    public TextMeshProUGUI UIMenssageText;
    public TextMeshProUGUI UINameText;

    GunScript gunScript;

    private void Awake()
    {
        ImagenUI = FindObjectOfType<SpriteRenderer>();
        gunScript = FindObjectOfType<GunScript>();
    }
    void Start()
    {
        players = new List<Player>();
        ImagenUI.gameObject.SetActive(false);
        CreateDefaultPlayers();
        //ListPlayersName();
        bullet = AssignBullet();
        turnPlayer = Random.Range(0, players.Count);
        UITurnoText.text = "Turno jugador " + (turnPlayer + 1) + " " + players[turnPlayer].name;
        UIMenssageText.text = "El juego comienza con el turno del jugador " + (turnPlayer + 1) + " " + players[turnPlayer].name;
        UINameText.text = players[turnPlayer].name;
    }

    public void LooseGame()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].alive == false)
            {
                someoneDead = true;
                UIMenssageText.text = "El jugador " + (turnPlayer + 1) + " " + players[turnPlayer].name + " ha perdido";
                UINameText.text = players[turnPlayer].name;
                UINameText.color = Color.red;
                ImagenUI.gameObject.SetActive(true);
 
                Invoke("ResetGame", 2.0f);
                break;
            }
        }
    }

    public void CreateDefaultPlayers()
    {
        if (PlayersName.Count != 0)
        {
            for (int i = 0; i < PlayersName.Count; i++)
            {
                CreateInternPlayer(i, PlayersName[i]);
            }
        }
        else
        {
            UIMenssageText.text = "No hay ningun jugador";
        }
    }

    //Función para probar que se guardan los Nombres de los jugadores en Players
    public void ListPlayersName()
    {
        if (players.Count != 0)
        {
            foreach (var item in players)
            {
                print("Jugador " + (item.id + 1) + " : " + item.name);
            }
        }
    }
    public void StopAnim()
    {
        gunScript.GunAnim(false);
    }

    public void ShootPlayer()
    {
        gunScript.GunAnim(true);
        Invoke("StopAnim", 0.6f);

        if (turnPlayer == bullet)
        {
            LooseGame();
        }
        else
        {
            if (turnPlayer == players.Count)
            {
                turnPlayer = 0;
                UIMenssageText.text = "Jugador " + (turnPlayer + 1) + " no es tu día de muerte";
                UITurnoText.text = "Turno jugador " + (turnPlayer + 1) + " " + players[turnPlayer].name;
                UINameText.text = players[turnPlayer].name;
            }
            else
            {
                UIMenssageText.text = players[turnPlayer].name + " no es tu día de muerte";

                turnPlayer++;
                if (turnPlayer == players.Count)
                {
                    turnPlayer = 0;
                    UITurnoText.text = "turno jugador " + (turnPlayer + 1) + " " + players[turnPlayer].name;
                    UINameText.text = players[turnPlayer].name;
                } else
                {
                    UITurnoText.text = "Turno jugador " + (turnPlayer + 1) + " " + players[turnPlayer].name;
                    UINameText.text = players[turnPlayer].name;
                }
            }
        }   
    }

    public int AssignBullet()
    {  
        bullet = Random.Range(0, players.Count);
        players[bullet].alive = false;
        Debug.Log("La bala le ha tocado al jugador " + (bullet + 1));
        return bullet;
    }

    public void ReadPlayerName(string name)
    {
        if (defaultPlayers == true)
        {
            // Para eliminar los jugadores por defecto
            players = new List<Player>();
        }
        defaultPlayers = false;
        CreateInternPlayer(players.Count, name);
    }

    public void CreateInternPlayer(int id, string name)
    {
        var _player = new Player();
        _player.name = name;
        _player.id = id;
        players.Add(_player);
    }

    void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
