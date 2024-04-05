using UnityEngine;

namespace UI
{
    public class AnimatedWindow : Window
    {
        [Header("Animation Settings")]
        [SerializeField] private AnimationClip _showAnimation;
        [SerializeField] private AnimationClip _hideAnimation;
        [SerializeField] private Animation _animation;

        public override void Show()
        {
            if (_animation != null && _showAnimation != null)
            {
                _animation.clip = _showAnimation;
                _animation.Play();
                _isShown = true;
            }
            else
            {
                base.Show();
            }
        }

        public override void Hide(bool ignoreIsShown = false)
        {
            if (_animation != null && _hideAnimation != null)
            {
                _animation.clip = _hideAnimation;
                _animation.Play();
                _isShown = false;
            }
            else
            {
                base.Hide(ignoreIsShown);
            }
        }
    }
}