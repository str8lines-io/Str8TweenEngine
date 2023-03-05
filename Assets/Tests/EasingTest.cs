#region namespaces
using NUnit.Framework;
using UnityEngine;
using Str8lines.Tweening;
#endregion

public class EasingTest
{
#region Variables
    float elapsed = 1f;
    float duration = 1f;
    float initialFloat = 0f;
    float floatChange = 1f;
    Vector3 initialVector = Vector3.zero;
    Vector3 vectorChange = new Vector3(1f, 1f, 1f);
#endregion

#region linear
    // LINEAR ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingLinearFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.Linear, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingLinearVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.Linear, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }
#endregion

#region sine
    // IN SINE ///////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInSineFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InSine, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInSineVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InSine, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // OUT SINE //////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingOutSineFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.OutSine, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingOutSineVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.OutSine, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // IN OUT SINE ///////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInOutSineFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InOutSine, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInOutSineVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InOutSine, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }
#endregion

#region back
    // IN BACK ///////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInBackFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InBack, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInBackVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InBack, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // OUT BACK //////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingOutBackFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.OutBack, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingOutBackVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.OutBack, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // IN OUT BACK ///////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInOutBackFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InOutBack, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInOutBackVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InOutBack, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }
#endregion

#region cubic
    // IN CUBIC ///////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInCubicFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InCubic, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInCubicVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InCubic, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // OUT CUBIC //////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingOutCubicFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.OutCubic, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingOutCubicVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.OutCubic, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // IN OUT CUBIC ///////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInOutCubicFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InOutCubic, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInOutCubicVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InOutCubic, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }
#endregion

#region quad
    // IN QUAD ///////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInQuadFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InQuad, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInQuadVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InQuad, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // OUT QUAD //////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingOutQuadFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.OutQuad, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingOutQuadVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.OutQuad, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // IN OUT QUAD ///////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInOutQuadFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InOutQuad, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInOutQuadVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InOutQuad, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }
#endregion

#region quart
    // IN QUART ///////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInQuartFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InQuart, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInQuartVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InQuart, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // OUT QUART //////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingOutQuartFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.OutQuart, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingOutQuartVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.OutQuart, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // IN OUT QUART ///////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInOutQuartFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InOutQuart, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInOutQuartVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InOutQuart, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }
#endregion

#region quint
    // IN QUINT ///////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInQuintFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InQuint, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInQuintVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InQuint, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // OUT QUINT //////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingOutQuintFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.OutQuint, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingOutQuintVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.OutQuint, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // IN OUT QUINT ///////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInOutQuintFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InOutQuint, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInOutQuintVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InOutQuint, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }
#endregion

#region expo
    // IN EXPO ///////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInExpoFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InExpo, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInExpoVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InExpo, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // OUT EXPO //////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingOutExpoFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.OutExpo, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingOutExpoVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.OutExpo, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // IN OUT EXPO ///////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInOutExpoFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InOutExpo, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInOutExpoVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InOutExpo, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }
#endregion

#region circ
    // IN CIRC ///////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInCircFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InCirc, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInCircVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InCirc, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // OUT CIRC //////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingOutCircFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.OutCirc, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingOutCircVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.OutCirc, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // IN OUT CIRC ///////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInOutCircFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InOutCirc, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInOutCircVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InOutCirc, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }
#endregion

#region elastic
    // IN ELASTIC ///////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInElasticFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InElastic, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInElasticVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InElastic, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // OUT ELASTIC //////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingOutElasticFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.OutElastic, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingOutElasticVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.OutElastic, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // IN OUT ELASTIC ///////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInOutElasticFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InOutElastic, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInOutElasticVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InOutElastic, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }
#endregion

#region bounce
    // IN BOUNCE ///////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInBounceFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InBounce, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInBounceVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InBounce, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // OUT BOUNCE //////////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingOutBounceFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.OutBounce, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingOutBounceVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.OutBounce, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }

    // IN OUT BOUNCE ///////////////////////////////////////////////////////////////////////////////////////////////
    [Test]
    public void EasingInOutBounceFloatHandled()
    {
        float resultStart = Easing.ease(EasingFunction.InOutBounce, elapsed, initialFloat, floatChange, duration);
        Assert.AreNotEqual(resultStart, initialFloat);
    }

    [Test]
    public void EasingInOutBounceVectorHandled()
    {
        Vector3 resultStart = Easing.ease(EasingFunction.InOutBounce, elapsed, initialVector, vectorChange, duration);
        Assert.AreNotEqual(resultStart, initialVector);
    }
#endregion
}