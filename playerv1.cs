using Godot;
using System;

public partial class playerv1 : CharacterBody2D
{
	public const float Speed = 100.0f;
	private AnimatedSprite2D move;


	private Vector2 target_position;
	private float damage_time = 0;

	private string state = "moving";

	private float attack_timer = 0;

	public Vector2 public_direction;

	[Signal] public delegate void DamageEventHandler();

	[Signal] public delegate string AttackEventHandler(string attack_direction);

    public override void _Ready()
    {
		move = GetNode<AnimatedSprite2D>("Move");
    }
    public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("attack") && state != "attacking")
			{
			state = "attacking";
			attack_timer = .3f;
			}
		if(state == "attacking")
		{
			if(attack_timer == .3f)
			{
				EmitSignal(SignalName.Attack, directionCheck());
			}
			if(attack_timer > 0)
			{
				attack_timer -= (float)delta;
			}
			else
			{
				state = "moving";
			}
		}
		if (state == "moving")
		{
            Vector2 velocity = Velocity;
            Vector2 direction = Input.GetVector("left", "right", "up", "down");
            if (direction != Vector2.Zero)
            {
                velocity = direction * Speed;
                moveAnimation(direction);
                public_direction = direction;
            }
            else
            {
                velocity = Vector2.Zero;
            }
			
            Velocity = velocity;
            MoveAndSlide();
        }

		if (state == "damage" && damage_time > 0)
		{
			Velocity = Velocity.Lerp(target_position, .1f);
			MoveAndSlide();
			damage_time -= (float)delta;
        }
		if (state == "damage" && damage_time <= 0)
		{
			state = "moving";
		}



    }
	private void moveAnimation(Vector2 direction)
	{
		if (direction.X > 0)
		{
			move.Play("right");
		}
        else if (direction.X < 0)
        {
            move.Play("left");
        }
		else
		{
			if(direction.Y < 0)
			{
				move.Play("up");
			}
			else if (direction.Y > 0)
			{
				move.Play("down");
			}
		}
    }
	private void _on_area_2d_body_entered(CharacterBody2D body)
	{
		if (body.GetCollisionMaskValue(4))
		{
			state = "damage";
			EmitSignal("Damage");
			target_position = (Position - body.GlobalPosition).Normalized();
            Velocity = target_position * Speed * 2;
            damage_time = .3f;
			//play damage animation
		}
	}
    private void _on_label_timer_depleted()
    {
        GetTree().ChangeSceneToFile("res://lobby.tscn");
    }
    public string directionCheck()
	{
        if (public_direction.X > 0)
        {
            if (public_direction.Y < 0)
            {
                return "up_right";
               
            }
            else if (public_direction.Y == 0)
            {
                return "right";
            }
            else
            {

                return "down_right";
            }
        }
        else if (public_direction.X < 0)
        {
            if (public_direction.Y < 0)
            {
                return "up_left";
            }
            else if (public_direction.Y == 0)
            {
                return "left";
            }
            else
            {
                return "down_left";
            }
        }
        else
        {
            if (public_direction.Y < 0)
            {
                return "up";
            }
            else
            {
                return "down";
            }
        }
    }
}

