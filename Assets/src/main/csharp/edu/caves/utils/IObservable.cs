using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavesEtVarans
{
    // Summary:
    //     Defines a provider for push-based notification.
    //
    // Type parameters:
    //   T:
    //     The object that provides notification information.This type parameter is
    //     covariant. That is, you can use either the type you specified or any type
    //     that is more derived. For more information about covariance and contravariance,
    //     see Covariance and Contravariance in Generics.
    public interface IObservable<out T>
    {
        // Summary:
        //     Notifies the provider that an observer is to receive notifications.
        //
        // Parameters:
        //   observer:
        //     The object that is to receive notifications.
        //
        // Returns:
        //     A reference to an interface that allows observers to stop receiving notifications
        //     before the provider has finished sending them.
        IDisposable Subscribe(IObserver<T> observer);
    }

}
