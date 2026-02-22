using Godot;
using System;

public partial class SnowboardController : RigidBody3D
{
	public bool IsActive = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _IntegrateForces(PhysicsDirectBodyState3D state)
	{
		base._IntegrateForces(state);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		//Check the slope of the ground below the snowboarder
		var spaceState = GetWorld3D().DirectSpaceState;
		var query = PhysicsRayQueryParameters3D.Create(GlobalTransform.Origin, GlobalTransform.Origin + Vector3.Down * 10.0f);
		query.CollideWithAreas = true;
		var raycast = spaceState.IntersectRay(query);
		if (raycast.Count > 0)
		{
			//Check the angle to define the 'downhill' direction
			var hitPos = raycast["position"].AsVector3();
			var hitNormal = raycast["normal"].AsVector3();
			var angle = hitNormal.AngleTo(Vector3.Up);
			var downhill = hitNormal.Cross(Vector3.Up);
			var downhillAngle = downhill.AngleTo(Vector3.Forward);

			//Apply a force in the downhill direction
			// ApplyCentralImpulse(downhill * 10.0f);

		}

		//Check left and right keys to turn the snowboarder
		var forward = Input.IsActionPressed("ui_up");
		var backward = Input.IsActionPressed("ui_down");
		var left = Input.IsActionPressed("ui_left");
		var right = Input.IsActionPressed("ui_right");
		if (left)
		{
			//Apply torque
			ApplyTorque(Vector3.Up * 100.0f);
		}
		if (right)
		{
			ApplyTorque(Vector3.Up * -100.0f);
		}

		//Check forward and backward keys to move the snowboarder
		if (forward)
		{
			var localForward = GlobalTransform.Basis.X;
			ApplyCentralForce(localForward * 2000.0f);
		}
		if (backward)
		{
			var localBack = -GlobalTransform.Basis.X;
			ApplyCentralForce(localBack * 2000.0f);
		}

	}
}
