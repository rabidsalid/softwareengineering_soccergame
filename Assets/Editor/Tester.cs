using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NUnit.Framework;

[TestFixture]
public class Tester
{
    [Test]
    public void FrictionScript_DoesNotApplyForce_WhenVelocityIsZero()
    {
        // ARRANGE
        GameObject testObject = new GameObject();
        Rigidbody2D rigidbody = testObject.AddComponent<Rigidbody2D>();
        rigidbody.mass = 1;
        rigidbody.velocity = Vector2.zero;
        Friction frictionScript = testObject.AddComponent<Friction>();
        frictionScript.setRigidbody(rigidbody); // this is needed because start doesn't run in editor mode

        // ACT, it should act on its own
        Vector2 frictionalForce = frictionScript.getFrictionalForce();

        // ASSERT
        // should be zero because gameobject velocity is zero
        Assert.AreEqual(Vector2.zero, frictionalForce);
    }
    [Test]
    public void FrictionScript_AppliesCorrectForce_WhenVelocityIsTwo()
    {
        // ARRANGE
        GameObject testObject = new GameObject();
        Rigidbody2D rigidbody = testObject.AddComponent<Rigidbody2D>();
        rigidbody.mass = 1;
        rigidbody.velocity = new Vector2(2f, 0f);
        Friction frictionScript = testObject.AddComponent<Friction>();
        frictionScript.setRigidbody(rigidbody);

        // ACT
        Vector2 frictionalForce = frictionScript.getFrictionalForce();

        // ASSERT
        Vector2 expectedForce = new Vector2(-rigidbody.velocity.normalized.x * frictionScript.frictionCoefficient * rigidbody.mass, 0f);

        Assert.AreEqual(frictionalForce, expectedForce); 
    }
}