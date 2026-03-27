using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Vector2 respawnPosition;
    public DeathTransition deathTransition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        respawnPosition = player.transform.position;
        StaticData.timeSpent = 0f;
        StaticData.timesCaught = 0;
    }

    // Update is called once per frame
    void Update()
    {
        StaticData.timeSpent += Time.deltaTime;
    }

    public void RespawnPlayer(Police police)
    {
        StaticData.timesCaught++;
        player.transform.position = respawnPosition;
        player.state = Player.State.Standard;
        police.Respawn();
    }

    public IEnumerator waiter(Police police)
    {
        Debug.Log("Romeo");
        deathTransition.DoTransition();
        yield return new WaitForSeconds(1.3f);
        RespawnPlayer(police);
    }
}
