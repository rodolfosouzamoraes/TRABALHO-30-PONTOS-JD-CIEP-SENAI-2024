using Godot;
using System;

public partial class Enemy : StaticBody2D
{
    float enemyLife = 100;
    Game game;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        game = GetParent().GetParent().GetParent().GetNode<Game>(".");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector2 velocity = new Vector2(0, 100 * (float)delta).Rotated(Rotation);
        Position -= velocity;
    }

    public void OnNode2DAreaEntered(Node2D area)
    {
        switch (area.Name)
        {
            case "LaserBody":
                enemyLife -= 25;
                if (enemyLife <= 0)
                {
                    QueueFree();
                }
                break;
            case "Nave":
                game.DecrementLife();
                QueueFree();
                break;
            case "Planet":
                game.DecrementLifePlanet(25);
                QueueFree();
                break;
        }
    }
}
