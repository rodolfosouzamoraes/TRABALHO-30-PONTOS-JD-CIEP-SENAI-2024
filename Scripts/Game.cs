using Godot;
using System;

public partial class Game : Node
{
    Label score;
    int scoreTotal;
    Sprite2D lifeOn1;
    Sprite2D lifeOn2;
    Sprite2D lifeOn3;
    int lifePlayer = 3;

    ProgressBar lifePlanetBar;
    int lifePlanetMax = 500;
    int lifePlanetNow;

    Node2D gameOverNode;
    Label scoreFinal;
    int scoreBestTotal;

    Nave nave;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        score = GetNode<Label>("CanvasLayer/Score");
        scoreTotal = 0;
        score.Text = scoreTotal.ToString();

        lifeOn1 = GetNode<Sprite2D>("CanvasLayer/Hearts/heart1");
        lifeOn2 = GetNode<Sprite2D>("CanvasLayer/Hearts/heart2");
        lifeOn3 = GetNode<Sprite2D>("CanvasLayer/Hearts/heart3");

        lifePlanetBar = GetNode<ProgressBar>("CanvasLayer/PlanetLife");
        lifePlanetBar.MaxValue = lifePlanetMax;
        lifePlanetNow = lifePlanetMax;
        lifePlanetBar.Value = lifePlanetNow;

        nave = GetNode<Nave>("Planet/Nave");

        gameOverNode = GetNode<Node2D>("CanvasLayer/GameOver");
        scoreFinal = GetNode<Label>("CanvasLayer/GameOver/Label3");
        gameOverNode.Hide();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void DecrementLife()
    {
        lifePlayer--;
        switch (lifePlayer)
        {
            case 2:
                lifeOn3.Hide();
                break;
            case 1:
                lifeOn2.Hide();
                break;
            case 0:
                lifeOn1.Hide();
                break;
            default:
                nave.Explosion();
                ShowGameOver();
                break;
        }
    }

    public void DecrementLifePlanet(int value)
    {
        if (lifePlanetNow <= 0) return;
        lifePlanetNow -= value;
        lifePlanetBar.Value = lifePlanetNow;
        if(lifePlanetNow <= 0)
        {
            nave.Explosion();
            ShowGameOver();
        }
    }

    public void IncrementScore(int points)
    {
        scoreTotal += points;
        score.Text = scoreTotal.ToString();
    }

    private void ShowGameOver()
    {
        scoreFinal.Text = scoreTotal.ToString();
        gameOverNode.Show();
    }

    public void TryAgain()
    {
        GetTree().ReloadCurrentScene();
    }

    public void ExitGame()
    {
        GetTree().Quit();
    }
}
