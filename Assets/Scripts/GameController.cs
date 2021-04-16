using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    List<Player> players;
    public List<string> playersName;
    bool someoneDead = false;
    int turnPlayer = 0;
    bool defaultPlayers = true;
    
    // Start is called before the first frame update
    void Start()
    {
        players = new List<Player>();
        CreateDefaultPlayers();
        //ListPlayersName();
    }

    // Update is called once per frame
    void Update()
    {
        looseGame();
        if (someoneDead == true)
        {
            Debug.LogError("El jugador " + (turnPlayer+1) + " " + players[turnPlayer].name + " ha perdido");
            SceneManager.LoadScene(0);
        }
    }

    public void looseGame()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].alive == false)
            {
                someoneDead = true;
                break;
            }
        }
    }

    public void CreateDefaultPlayers()
    {
        if (playersName.Count != 0)
        {
            for (int i = 0; i < playersName.Count; i++)
            {
                var _player = new Player();
                _player.name = playersName[i];
                _player.id = i;
                players.Add(_player);
            }
        }
        else
        {
            Debug.LogError("No hay ningun jugador");
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

    public void ShootPlayer()
    {
        int bullet = 0;
        bullet = Random.Range(0, players.Count);
        players[bullet].alive = false;
        turnPlayer = bullet;
        Debug.Log("La bala le ha tocado al jugador " + (bullet+1));
    }

    public void ReadPlayerName(string name)
    {
        if (defaultPlayers == true)
        {
            // Para eliminar los jugadores por defecto
            players = new List<Player>();
        }
        defaultPlayers = false;
        var _player = new Player();
        _player.name = name;
        _player.id = players.Count;
        players.Add(_player);
    }
}
