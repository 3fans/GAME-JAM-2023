using Godot;
using System;
using System.Security.AccessControl;

public partial class playerv1 : CharacterBody2D
{
	public const float Speed = 120.0f;
	private AnimationPlayer move;


	private Vector2 target_position;
	private float damage_time = 0;

	private string state = "moving";
    private bool is_dashing = false;
    private bool can_dash = false;
    private float dash_timer = 0;
    private float dash_time = .7f;

	private float attack_timer = 0;

	public Vector2 public_direction;

	[Signal] public delegate void DamageEventHandler();

	[Signal] public delegate string AttackEventHandler(string attack_direction);
    
    public override void _Ready()
    {
        base._Ready();
		move = GetNode<AnimationPlayer>("Move");
        move.Play("down");
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
                attack_Animation(public_direction);
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
            if (Input.IsActionJustPressed("dash") && !is_dashing && can_dash)
            {
                dash_timer = dash_time;
                is_dashing = true;
                velocity = (direction * Speed * 5);
                dashAnimation(direction);

            }
            else if (is_dashing && dash_timer > 0)
            {
                dash_timer -= (float)delta;
                velocity = velocity * .9f;
            }
            else if (is_dashing && dash_timer <= 0)
            {
                is_dashing = false;
            }
            else if (direction != Vector2.Zero && !is_dashing)
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
    }	private void dashAnimation(Vector2 direction)
	{
		if (direction.X > 0)
		{
			move.Play("dash_right");
		}
        else if (direction.X < 0)
        {
            move.Play("dash_left");
        }
		else
		{
			if(direction.Y < 0)
			{
				move.Play("dash_up");
			}
			else if (direction.Y > 0)
			{
				move.Play("dash_down");
			}
		}
    }
    private void attack_Animation(Vector2 direction)
    {
        if (direction.X > 0)
        {
            move.Play("attack_right");
        }
        else if (direction.X < 0)
        {
            move.Play("attack_left");
        }
        else
        {
            if (direction.Y < 0)
            {
                move.Play("attack_up");
            }
            else if (direction.Y > 0)
            {
                move.Play("attack_down");
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
            Velocity = target_position * Speed * 3;
            damage_time = .3f;
            //play damage animation
            move.Play("damage");
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
			return "right";
        }
        else if (public_direction.X < 0)
        {
            return "left";

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
    private void _on_global_handler_on_load(bool global_can_dash, float global_dash_time)
    {
        can_dash = global_can_dash;
        dash_time = global_dash_time;
    }
}

