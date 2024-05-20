using Godot;
using System;

public partial class Explosion : Node2D
{
    Timer timer;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        Callable callable = Callable.From(() => QueueFree());

        timer = new Timer();
        timer.OneShot = true;
        timer.WaitTime = 0.5;
        timer.Autostart = true;
        timer.Connect("timeout", callable);
        AddChild(timer);
        timer.Start();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
