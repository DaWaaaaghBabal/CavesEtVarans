using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavesEtVarans
{
    // Summary:
    //     Provides a mechanism for receiving push-based notifications.
    //
    // Type parameters:
    //   T:
    //     The object that provides notification information.This type parameter is
    //     contravariant. That is, you can use either the type you specified or any
    //     type that is less derived. For more information about covariance and contravariance,
    //     see Covariance and Contravariance in Generics.
    public interface IObserver<in T>
    {
        // Summary:
        //     Notifies the observer that the provider has finished sending push-based notifications.
        void OnCompleted();
        //
        // Summary:
        //     Notifies the observer that the provider has experienced an error condition.
        //
        // Parameters:
        //   error:
        //     An object that provides additional information about the error.
        void OnError(Exception error);
        //
        // Summary:
        //     Provides the observer with new data.
        //
        // Parameters:
        //   value:
        //     The current notification information.
        void OnNext(T value);
    }

}
