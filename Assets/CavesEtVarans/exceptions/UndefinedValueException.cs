using System;

namespace CavesEtVarans.exceptions {
    public class UndefinedValueException : Exception {
        public UndefinedValueException() {
        }

        public UndefinedValueException(string message) : base(message) {
        }
    }
}