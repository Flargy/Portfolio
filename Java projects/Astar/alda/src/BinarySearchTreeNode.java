package alda.tree;

/**
 * 
 * Detta �r den enda av de tre klasserna ni ska g�ra n�gra �ndringar i. (Om ni
 * inte vill l�gga till fler testfall.) Det �r ocks� den enda av klasserna ni
 * ska l�mna in. Gl�m inte att namn och anv�ndarnamn ska st� i en kommentar
 * h�gst upp, och att paketdeklarationen m�ste plockas bort vid inl�mningen f�r
 * att koden ska g� igenom de automatiska testerna.
 * 
 * De �ndringar som �r till�tna �r begr�nsade av f�ljande:
 * <ul>
 * <li>Ni f�r INTE byta namn p� klassen.
 * <li>Ni f�r INTE l�gga till n�gra fler instansvariabler.
 * <li>Ni f�r INTE l�gga till n�gra statiska variabler.
 * <li>Ni f�r INTE anv�nda n�gra loopar n�gonstans. Detta g�ller ocks� alterntiv
 * till loopar, s� som str�mmar.
 * <li>Ni F�R l�gga till fler metoder, dessa ska d� vara privata.
 * <li>Ni f�r INTE l�ta N�GON metod ta en parameter av typen
 * BinarySearchTreeNode. Enbart den generiska typen (T eller vad ni v�ljer att
 * kalla den), String, StringBuilder, StringBuffer, samt primitiva typer �r
 * till�tna.
 * </ul>
 * 
 * @author henrikbe
 * 
 * @param <T>
 */
@SuppressWarnings("unused") // Denna rad ska plockas bort. Den finns h�r
							// tillf�lligt f�r att vi inte ska tro att det �r
							// fel i koden. Varningar ska normalt inte d�ljas p�
							// detta s�tt, de �r (oftast) fel som ska fixas.
public class BinarySearchTreeNode<T extends Comparable<T>> {

	private T data;
	private BinarySearchTreeNode<T> left;
	private BinarySearchTreeNode<T> right;

	public BinarySearchTreeNode(T data) {
		this.data = data;
	}

	public boolean add(T data) {
		boolean state = true;
		if (data.compareTo(this.data) == 0) {
			return false;
			}
		else if (data.compareTo(this.data) == 1) {
			if(right == null)
				right = new BinarySearchTreeNode<T>(data);
			else
				state = right.add(data);
			return state;
		}
		else if (data.compareTo(this.data) == -1) {
			if(left == null)
				left = new BinarySearchTreeNode<T>(data);
			else
				state = left.add(data);
			return state;
		}
			
		return false;
	}

	private T findMin() {
		return null;
	}

	public BinarySearchTreeNode<T> remove(T data) {
		return null;
	}

	public boolean contains(T data) {
		if(data.compareTo(this.data) == 0)
			return true;
		else if (data.compareTo(this.data) == -1) {
			if (left != null)
				return left.contains(data);
		}
		else if (data.compareTo(this.data) == 1) {
			if (right != null)
				return right.contains(data);
		}
			return false;
	}

	public int size() {
		int sizeOfTree = 0;
		if (this != null) {
			sizeOfTree = 1;
		}
		if(left != null) {
			sizeOfTree += left.size();
		}
		if(right != null) {
			sizeOfTree += right.size();
		}
	
		return sizeOfTree;
	}

	public int depth() {
		int depthOfTree = 0;
		int depthOfRight = 0;
		int depthOfLeft = 0;
		
		if (this != null)
			depthOfTree = 1;
		if(left != null)
			depthOfLeft += left.depth();
		if(right != null)
			depthOfRight += right.depth();
		
		if(depthOfLeft > depthOfRight)
			depthOfTree += depthOfLeft;
		else if (depthOfRight > depthOfRight)
			depthOfTree += depthOfLeft;
		
		return depthOfTree;
	}

	public String toString() {
		String temp = "";
		int limit = 1;
		if (data != null) {
			if (left != null) {
				temp += left.toString() + ", ";
			}
			
			temp += data;
			
			if (right != null) {
				temp += ", " + right.toString();
			}
		}
		
		
		
		return temp;
	}
}