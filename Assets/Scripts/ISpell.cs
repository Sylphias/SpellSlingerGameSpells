using System;

public interface ISpell
{
	float duration {
		get;
		set;
	}
	float cooldown {
		get;
		set;
	}

	bool isOnCooldown();

}

