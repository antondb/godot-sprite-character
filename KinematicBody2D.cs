using Godot;
using System;

public class KinematicBody2D : Godot.KinematicBody2D
{
 	public int Speed = 250;
	private Vector2 _velocity = new Vector2();

	private AnimationPlayer _animationPlayer;
	private AnimationTree _animationTree;
	private AnimationNodeStateMachinePlayback _stateMachine;
	private Sprite _playerSprite;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playerSprite = GetNode<Sprite>("Sprite");
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animationTree = GetNode<AnimationTree>("AnimationTree");
		_stateMachine = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
		_animationTree.Active = true;
		_stateMachine.Start("idle");
	}

	public void getInput()
	{

		// Detect up/down/left/right keystate and only move when pressed
		_velocity = new Vector2();
		if (Input.IsActionPressed("ui_right"))
		{
			_velocity.x += 1;
			_playerSprite.FlipH =false ;
			_stateMachine.Travel("run");
		}
		if (Input.IsActionPressed("ui_left"))
		{
			_velocity.x -= 1;
			_playerSprite.FlipH =true ;
			_stateMachine.Travel("run");
		}
		if (Input.IsActionPressed("ui_down"))
		{
			_velocity.y += 1;
		}
		if (Input.IsActionPressed("ui_up"))
		{
			//_velocity.y -= 1;
			_stateMachine.Travel("kick");
		}
		
		if(Input.IsActionJustReleased("ui_right") | Input.IsActionJustReleased("ui_left")){
			_stateMachine.Travel("idle");
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		getInput();
		MoveAndSlide(_velocity * Speed);
	}
}
