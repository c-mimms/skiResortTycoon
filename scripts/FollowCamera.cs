using System;
using Godot;

public partial class FollowCamera : Camera3D
{
	[Export]
	public float LerpSpeed = 3.0f;

	[Export]
	public Node3D Target;

	[Export]
	public Vector3 Offset = Vector3.Zero;
	
	public override void _PhysicsProcess(double delta)
	{
		if (Target == null)
			return;


		var velocity = Vector3.Forward;
		var newOffset = Offset;

		//If target is a rigidbody, get its linear velocity
		if(Target is RigidBody3D targetBody)
		{
			velocity = targetBody.LinearVelocity;
			//Rotate offset to be in opposite direction of velocity x and z components
			newOffset = Offset.Rotated(Vector3.Up, Mathf.Atan2(velocity.X, velocity.Z));
			// newOffset = Offset.Rotated(Vector3.Up, Mathf.Acos(velocity.X, velocity.Y));
		}

		var targetXform = Target.GlobalTransform.Translated(newOffset);

		//Raycast from target to camera
		var spaceState = GetWorld3D().DirectSpaceState;
		var query = PhysicsRayQueryParameters3D.Create(Target.GlobalTransform.Origin, targetXform.Origin);
		query.CollideWithAreas = true;
		var raycast = spaceState.IntersectRay(query);
		if (raycast.Count > 0)
		{
			// //If we hit something, move the camera to the hit position
			// GD.Print(raycast["position"]);
			// targetXform.Origin = raycast["position"].AsVector3();
			// targetXform.Origin.Y += 10.0f;
		}

		//Clear out rotational transforms
		// targetXform.Basis = Basis.Identity;

		GlobalTransform = GlobalTransform.InterpolateWith(targetXform, LerpSpeed * (float)delta);

		LookAt(Target.GlobalTransform.Origin, Vector3.Up);
	}
}
