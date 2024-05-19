using Godot;
using System;

public partial class InstantiateEnemy : Node2D
{
    [Export] PackedScene enemy = ResourceLoader.Load<PackedScene>("res://Prefabs/enemy.tscn");

    Timer timer;
    bool isInstantiate = false;
    double waitTimerInstantiate = 0.75;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        Callable callable = Callable.From(() => Instantiate());

        timer = new Timer();
        timer.OneShot = true;
        timer.WaitTime = waitTimerInstantiate;
        timer.Autostart = true;
        timer.Connect("timeout", callable);
        AddChild(timer);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (isInstantiate == true)
        {
            isInstantiate = false;
            timer.Start();
        }
    }

    public void Instantiate()
    {
        isInstantiate = true;
        int rotationAngle = new Random().Next(0, 361);
        Node enemyNode = enemy.Instantiate();
        AddChild(enemyNode);
        enemyNode.GetNode<Node2D>(enemyNode.GetPath()).Position = new Vector2(0, 0);
        enemyNode.GetNode<Node2D>(enemyNode.GetPath()).RotationDegrees += rotationAngle;
    }
}
