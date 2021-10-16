using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using DG.Tweening;

public enum Mode
{
    nonKinematic, kinematic, standardGravity, customGravity, timescale, defaultTimeScale
}

public class PostProcessManager : MonoBehaviour
{
    [Tooltip("Volume used during object placement mode")]
    public Volume kinematicVolume;
    [Tooltip("Volume used when special effects are active")]
    public Volume nonKinematicVolume;

    ColorCurves nonKinematicColorCurves;
    ColorAdjustments nonKinematicColorAdj;

    private void Start()
    {
        nonKinematicVolume.profile.TryGet<ColorCurves>(out nonKinematicColorCurves);
        nonKinematicVolume.profile.TryGet<ColorAdjustments>(out nonKinematicColorAdj);
    }

    //Switch with cases for the different states the game can be in, taking in an Enum for which mode to use, see above
    public void ChangeVolume(Mode modeToEnable)
    {
        switch (modeToEnable)
        {
            case Mode.nonKinematic:
                //Interpolate the volume transition using DOTween's DOVirtual method
                DOVirtual.Float(kinematicVolume.weight, 0, .2f, KinematicVolumeWeight).SetUpdate(true).SetEase(Ease.InOutSine);
                break;
            case Mode.kinematic:
                DOVirtual.Float(kinematicVolume.weight, 1, .2f, KinematicVolumeWeight).SetUpdate(true).SetEase(Ease.InOutSine);
                break;
            case Mode.customGravity:
                nonKinematicColorCurves.active = true;
                break;
            case Mode.standardGravity:
                nonKinematicColorCurves.active = false;
                break;
            case Mode.timescale:
                DOVirtual.Float(nonKinematicColorAdj.saturation.value, -80, .2f, NonKinematicSaturation).SetUpdate(true).SetEase(Ease.InOutSine);
                break;
            case Mode.defaultTimeScale:
                DOVirtual.Float(nonKinematicColorAdj.saturation.value, 5, .2f, NonKinematicSaturation).SetUpdate(true).SetEase(Ease.InOutSine);
                break;
        }
    }

    // Use these two methods to update the volume weights and blend between volumes
    void KinematicVolumeWeight(float weight)
    {
        kinematicVolume.weight = weight;
    }

    void NonKinematicSaturation(float sat)
    {
        nonKinematicColorAdj.saturation.value = sat;
    }
}
