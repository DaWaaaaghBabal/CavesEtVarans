﻿using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;
using UnityEngine;

namespace CavesEtVarans.graphics {

	public class CharacterAnimator : MonoBehaviour {

		public Character Character { set; get; }
		private float walkAnimLength;
        private AnimationQueue animationQueue;
		void Start() {
			RegisterListeners();
			walkAnimLength = ExtractClip("Walk").length;
            animationQueue = new AnimationQueue();
        }

        void Update() {
            animationQueue.Animate();
		}

		private void RegisterListeners() {
			SkillUseEvent.Listeners += CatchSkillUse;
			MovementEvent.Listeners += CatchMovement;
			OrientationEvent.Listeners += CatchOrientation;
		}
		
		private void CatchOrientation(OrientationEvent e, Context context) {
			if (Character.Equals(e.Source)) {
                //@TODO Fix import to remove the necessity for the 90° offset
                int newAngle = 90 + GraphicBattlefield.AngleForDirection(e.NewOrientation);
                int oldAngle = 90 + GraphicBattlefield.AngleForDirection(e.OldOrientation);
                GraphicTile tile = GraphicBattlefield.GetSceneTile(Character.Tile);
                QueueAnimation("Idle", 0.2f, tile.transform.position, tile.transform.position, 
                    Quaternion.Euler(0, oldAngle, 0), Quaternion.Euler(0, newAngle, 0));
            }
		}

		private void CatchMovement(MovementEvent e, Context context) {
			if (Character.Equals(e.Source))
            {
                int angle = 90 + GraphicBattlefield.AngleForDirection(Character.Orientation);
                Quaternion rotation = Quaternion.Euler(0, angle, 0);
                GraphicTile end = GraphicBattlefield.GetSceneTile(e.EndTile);
                GraphicTile start = GraphicBattlefield.GetSceneTile(e.StartTile);
                Vector3 startPosition = start.transform.position;
                Vector3 endPosition = end.transform.position;
                QueueAnimation("Walk - start", startPosition, startPosition, rotation, rotation);
                QueueAnimation("Walk", startPosition, endPosition, rotation, rotation);
                QueueAnimation("Walk - end", endPosition, endPosition, rotation, rotation);
            }
        }

        private void QueueAnimation(string animation, Vector3 startPosition, Vector3 endPosition, Quaternion startRotation, Quaternion endRotation)
        {
            QueueAnimation(animation, ExtractClip(animation).length, startPosition, endPosition, startRotation, endRotation);
        }

        private void QueueAnimation(string animation, float animLength,
            Vector3 startPosition, Vector3 endPosition, 
            Quaternion startRotation, Quaternion endRotation)
        {
            AnimationElement element = new AnimationElement()
            {
                AnimationName = animation,
                Transform = transform,
                StartPosition = startPosition,
                StartRotation = startRotation,
                EndPosition = endPosition,
                EndRotation = endRotation,
                AnimCallback = PlayAnimation,
                AnimLength = animLength
            };
            animationQueue.Queue(element);
        }

        private void CatchSkillUse(SkillUseEvent e, Context context) {
			if (Character.Equals(e.Source)) {
				QueueAnimation(e.Skill.Attributes.Animation);
			}
		}

        private void QueueAnimation(string animation) {
            GraphicTile tile = GraphicBattlefield.GetSceneTile(Character.Tile);
            //@TODO Fix import to remove the necessity for the 90° offset
            int angle = 90 + GraphicBattlefield.AngleForDirection(Character.Orientation);
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            AnimationElement element = new AnimationElement()
            {
                AnimationName = animation,
                Transform = transform,
                StartPosition = tile.transform.position,
                StartRotation = rotation,
                EndPosition = tile.transform.position,
                EndRotation = rotation,
                AnimCallback = PlayAnimation,
                AnimLength = ExtractClip(animation).length
            };
            animationQueue.Queue(element);
        }

        private void PlayAnimation(string animationName) {
			GetComponent<Animator>().Play(animationName);
		}
		
		private AnimationClip ExtractClip(string animationName) {
			Animator anim = GetComponent<Animator>();
			RuntimeAnimatorController controller = anim.runtimeAnimatorController;
			foreach (AnimationClip clip in controller.animationClips)
				if (animationName.Equals(clip.name))
					return clip;
			return null;
        }
	}
}