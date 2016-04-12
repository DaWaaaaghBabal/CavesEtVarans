namespace CavesEtVarans.utils
{
	/** Implementation of the Observer pattern ; provides an interface for Observer and an abstract class for Observable.
	 */
	public interface Observer<T> {

		void Update(T data);

	}
}
