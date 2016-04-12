using System;
using UnityEngine;

namespace CavesEtVarans.graphics
{
    public class AnimationElement
    {
        public string AnimationName { get; set; }
        public float AnimLength { get; internal set; }
        public Vector3 EndPosition { get; set; }
        public Quaternion EndRotation { get; internal set; }
        public Vector3 StartPosition { get; set; }
        public Quaternion StartRotation { get; internal set; }
        public Transform Transform { get; set; }
        public Action NextCallback { get; set; }
        public Action<string> AnimCallback { get; internal set; }

        private bool moving;
        private float time = 0;

        public void Start()
        {
            moving = !EndPosition.Equals(StartPosition) | !EndRotation.Equals(StartRotation);
            AnimCallback(AnimationName);
        }

        public void Animate()
        {
            time += Time.deltaTime;
            if (moving)
            {
                float progress = time / AnimLength;
                Transform.position = Vector3.Lerp(StartPosition, EndPosition, progress);
                Transform.rotation = Quaternion.Lerp(StartRotation, EndRotation, progress);
            }
            if (time >= AnimLength)
                NextCallback();
        }

        public override string ToString()
        {
            return AnimationName
                + " from " + StartPosition + ", " + StartRotation
                + " to " + EndPosition + ", " + EndRotation;
        }
    }
}