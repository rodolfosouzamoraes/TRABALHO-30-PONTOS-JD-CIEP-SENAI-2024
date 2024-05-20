using Godot;
using System;

public partial class Enemy : StaticBody2D
{
    [Export] PackedScene explosion = ResourceLoader.Load<PackedScene>("res://Prefabs/explosion.tscn");
    Game game;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        game = GetParent().GetParent().GetParent().GetNode<Game>(".");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector2 velocity = new Vector2(0, 150 * (float)delta).Rotated(Rotation);
        Position -= velocity;
    }

    public void OnNode2DAreaEntered(Node2D area)
    {
        switch (area.Name)
        {
            case "LaserNave":
                area.QueueFree();
                game.IncrementScore(250);
                Explosion();
                break;
            case "Nave":
                game.DecrementLife();
                Explosion();
                break;
            case "Planet":
                game.DecrementLifePlanet(25);
                Explosion();
                break;
        }
    }

    public void Explosion()
    {
        Node explosionNode = explosion.Instantiate();
        GetParent().AddChild(explosionNode);
        explosionNode.GetNode<Node2D>(explosionNode.GetPath()).Position = new Vector2(Position.X, Position.Y);
        QueueFree();
    }
}
