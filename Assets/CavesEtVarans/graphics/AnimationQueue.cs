using System;
using System.Collections.Generic;

namespace CavesEtVarans.graphics
{
    public class AnimationQueue
    {
        private Queue<AnimationElement> queue;
        private AnimationElement currentElement;
        private Action AnimCallback;
  
        public AnimationQueue()
        {
            AnimCallback = Wait;
            queue = new Queue<AnimationElement>();
            currentElement = null;
        }
        public void Queue(AnimationElement element)
        {
            queue.Enqueue(element);
            element.NextCallback = Next;
            if (currentElement == null)
            {
                AnimCallback = Anim;
                Next();
            }
        }
 
        public void Next() {
            if (queue.Count != 0)
            {
                currentElement = queue.Dequeue();
                currentElement.Start();
            } else {
                currentElement = null;
                AnimCallback = Wait;
            }
        }

        public void Animate()
        {
            AnimCallback();
        }

        private void Anim()
        {
            currentElement.Animate();
        }
        private void Wait() {
            // Do nothing. That's the point of waiting.
        }
    }
}