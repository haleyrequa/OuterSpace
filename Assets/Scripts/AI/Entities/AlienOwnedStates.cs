using UnityEngine;
using System.Collections;

// ------------------------------------------------------------------------
// Alien Global State
//------------------------------------------------------------------------
public class AlienGlobalState : State
{
	private static AlienGlobalState instance;
	private AlienGlobalState(){}
	public static AlienGlobalState Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new AlienGlobalState();
			}
			return instance;
		}
	}//this is a singleton

	public override void Enter(GameEntity alien)
	{

	}

	public override void Execute(GameEntity alien)
	{
		Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		float disToPlayer = Vector3.Distance(alien.transform.position,playerPos);
		float angleToPlayer = Vector3.Angle(playerPos - alien.transform.localPosition, alien.transform.forward);

		if(disToPlayer < Alien.c_siteDepth)// && angleToPlayer < Alien.c_siteAngle)
		{
			((Alien)alien).GetFSM().ChangeState(AlienAttackState.Instance);
		}
		else if(disToPlayer >= Alien.c_siteDepth)
		{
			((Alien)alien).GetFSM().ChangeState(AlienTravelState.Instance);
		}
	}

	public override void Exit(GameEntity alien)
	{}
};


// ------------------------------------------------------------------------
// Alien Attack State
//------------------------------------------------------------------------
public class AlienAttackState : State
{
	private static AlienAttackState instance;
	private AlienAttackState(){}
	public static AlienAttackState Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new AlienAttackState();
			}
			return instance;
		}
	}//this is a singleton


	public override void Enter(GameEntity alien)
	{
		((Alien)alien).m_steeringBehavoir.SetSteering(SteeringBehavoir.Steering.Chase);
		((Alien)alien).m_steeringBehavoir.SetSpeed(Alien.c_alien_chase_speed);	
	}

	public override void Execute(GameEntity alien)
	{
//		Vector3 playerPos = GameObject.Find("Player").transform.localPosition;
//		float disToPlayer = Vector3.Distance(alien.transform.localPosition,playerPos);
//		if(disToPlayer < (float)7)
//		{
//			((Alien)alien).AttackPlayer();
//		}
		((Alien)alien).m_steeringBehavoir.SetSteering(SteeringBehavoir.Steering.Chase);

	}

	public override void Exit(GameEntity alien)
	{
		((Alien)alien).StopShooting();
	}
};


// ------------------------------------------------------------------------
// Alien Attack State
//------------------------------------------------------------------------
public class AlienTravelState : State
{
	private static AlienTravelState instance;
	private AlienTravelState(){}
	public static AlienTravelState Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new AlienTravelState();
			}
			return instance;
		}
	}//this is a singleton


	public override void Enter(GameEntity alien)
	{
		((Alien)alien).m_steeringBehavoir.SetSteering(SteeringBehavoir.Steering.Wander);	
		((Alien)alien).m_steeringBehavoir.SetSpeed(Alien.c_alien_cruisin_speed);	
	}

	public override void Execute(GameEntity alien)
	{

	}
	public override void Exit(GameEntity alien){
	}
};


// ------------------------------------------------------------------------
// Alien Flee State
//------------------------------------------------------------------------
public class AlienFleeState : State
{
	private static AlienFleeState instance;
	private AlienFleeState(){}
	public static AlienFleeState Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new AlienFleeState();
			}
			return instance;
		}
	}//this is a singleton


	public override void Enter(GameEntity alien)
	{
		((Alien)alien).m_steeringBehavoir.SetSteering(SteeringBehavoir.Steering.Flee);	
		((Alien)alien).m_steeringBehavoir.SetSpeed(Alien.c_alien_flee_speed);	
	}

	public override void Execute(GameEntity alien)
	{
		
	}
	public override void Exit(GameEntity alien){
	}
};