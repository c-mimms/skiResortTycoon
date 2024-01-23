using Godot;
using System;

public partial class GameController : Node
{

	[Signal]
	public delegate void SwitchPlayerEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		var character = GetTree().Root.GetNode("Mountain1/Character");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("switch_player"))
		{
			GD.Print("Switching player");
			var character = GetTree().Root.GetNode("Mountain1/Character");
			var camera = character.FindChild("Camera") as Camera3D;


			var followCam = GetTree().Root.GetNode("Mountain1/Ball/FollowCamera") as Camera3D;
			var ballController = GetTree().Root.GetNode("Mountain1/Ball/RigidBody3D") as BallController;

			if(camera.Current){
				camera.Current = false;
				followCam.MakeCurrent();
				ballController.IsActive = true;
			} else {
				camera.MakeCurrent();
				ballController.IsActive = false;
				//Make character active
			}
			EmitSignal(SignalName.SwitchPlayer);
		}

		//Move sunlight direction in an arc
		var sun = GetTree().Root.GetNode("Mountain1/sun") as DirectionalLight3D;
		var moon = GetTree().Root.GetNode("Mountain1/moon") as DirectionalLight3D;
		var sunXform = sun.Transform;
		var moonXform = moon.Transform;
		sunXform = sunXform.Rotated(Vector3.Forward, 0.001f);
		moonXform = moonXform.Rotated(Vector3.Forward, (float)Math.PI + 0.001f);
		sun.Transform = sunXform;
		moon.Transform = moonXform;


	}
}
