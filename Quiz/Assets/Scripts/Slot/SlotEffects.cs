using UnityEngine;
using DG.Tweening;
using Quiz.Level;

namespace Quiz.Slot
{
    public class SlotEffects : MonoBehaviour, IMovement
    {
        [SerializeField]
        private float sideMoveOffset = 0.3f;

        [SerializeField]
        private float sideMoveDuration = 0.5f;

        [SerializeField]
        private float bounceMidScale = 0.8f;

        [SerializeField]
        private float bounceDuration = 1f;

        [SerializeField]
        private int correctChoiceBounceCount = 4;

        [SerializeField]
        private int awakeBounceCount = 2;

        private float originalPosX;

        private Tween moveSideToSide, bounce;

        private Transform _transform;

        private ParticleSystem correctChoiceEffect;

        private void Awake()
        {
            _transform = transform;

            correctChoiceEffect = GetComponent<ParticleSystem>();
        }

        private void Start()
        {
            if (GetComponentInParent<ILevel>().CurrentLevel == 0)
            {
                BounceAwake(false, 0f, bounceMidScale);
            }

            originalPosX = _transform.position.x;
            moveSideToSide = _transform.DOLocalMoveX(originalPosX - sideMoveOffset, sideMoveDuration)
                .SetEase(Ease.InQuad)
                .SetLoops(6, LoopType.Yoyo)
                .OnComplete(MoveToOriginalPosition).SetAutoKill(false).Pause();
        }

        public void BounceAwake(bool changeLevelOnComplete, float startScale = 1f, float midScale = 0.8f)
        {
            if (bounce != null)
                return;

            if (startScale == 0f)
            {
                bounce = _transform.DOScale(startScale, bounceDuration).From().OnComplete(() => BouceIdle(changeLevelOnComplete, midScale));
            }
            else
            {
                BouceIdle(changeLevelOnComplete, midScale);
            }
        }

        private void BouceIdle(bool changeLevelOnComplete, float midScale = 0.8f)
        {
            if (changeLevelOnComplete)
            {
                correctChoiceEffect.Play();

                bounce = _transform.DOScale(midScale, bounceDuration)
                    .SetLoops(correctChoiceBounceCount, LoopType.Yoyo).OnComplete(GetComponentInParent<ILevel>().ChangeLevel);
            }
            else
            {
                bounce = _transform.DOScale(midScale, bounceDuration).SetLoops(awakeBounceCount, LoopType.Yoyo).OnComplete(ClearBounce);
            }
        }

        private void ClearBounce()
        {
            bounce = null;
        }

        public void MoveSideToSide()
        {
            _transform.position = new Vector3(originalPosX + sideMoveOffset, _transform.position.y, _transform.position.z);

            moveSideToSide.Restart();
        }

        private void MoveToOriginalPosition()
        {
            _transform.DOLocalMoveX(originalPosX, sideMoveDuration);
        }
    }
}
