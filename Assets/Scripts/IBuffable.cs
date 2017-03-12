using System;
using UnityEngine;
using System.Collections;

public interface IBuffable
{

	void Apply(Component victim);
	bool finished { get; }
}


