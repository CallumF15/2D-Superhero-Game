using UnityEngine;
using System.Collections;

public interface IPowerups {

	float damage { get; set; }
	float damageMultiplier { get; set; }
	float duration { get; set; }

	void SpecialEffect();
}
