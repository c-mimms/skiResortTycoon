using Godot;
using System;

public partial class BallController : RigidBody3D
{
	public bool IsActive = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (IsActive)
		{
			//Check if forward is pressed
			var forward = Input.IsActionPressed("ui_up");
			var backward = Input.IsActionPressed("ui_down");
			var left = Input.IsActionPressed("ui_left");
			var right = Input.IsActionPressed("ui_right");
			
			//All of these should be relative to the ball's current velocity
			if(forward)
			{
				ApplyCentralImpulse(LinearVelocity.Normalized() * 10.0f);
			}
			if(backward)
			{
				ApplyCentralImpulse(-LinearVelocity.Normalized() * 10.0f);
			}
			if(left)
			{
				ApplyCentralImpulse(LinearVelocity.Normalized().Cross(Vector3.Down) * 10.0f);
			}
			if(right)
			{
				ApplyCentralImpulse(LinearVelocity.Normalized().Cross(Vector3.Up) * 10.0f);
			}
		}

	}
}
