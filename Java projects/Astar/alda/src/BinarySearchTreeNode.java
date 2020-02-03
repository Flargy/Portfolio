package alda.tree;

/**
 * 
 * Detta är den enda av de tre klasserna ni ska göra några ändringar i. (Om ni
 * inte vill lägga till fler testfall.) Det är också den enda av klasserna ni
 * ska lämna in. Glöm inte att namn och användarnamn ska stå i en kommentar
 * högst upp, och att paketdeklarationen måste plockas bort vid inlämningen för
 * att koden ska gå igenom de automatiska testerna.
 * 
 * De ändringar som är tillåtna är begränsade av följande:
 * <ul>
 * <li>Ni får INTE byta namn på klassen.
 * <li>Ni får INTE lägga till några fler instansvariabler.
 * <li>Ni får INTE lägga till några statiska variabler.
 * <li>Ni får INTE använda några loopar någonstans. Detta gäller också alterntiv
 * till loopar, så som strömmar.
 * <li>Ni FÅR lägga till fler metoder, dessa ska då vara privata.
 * <li>Ni får INTE låta NÅGON metod ta en parameter av typen
 * BinarySearchTreeNode. Enbart den generiska typen (T eller vad ni väljer att
 * kalla den), String, StringBuilder, StringBuffer, samt primitiva typer är
 * tillåtna.
 * </ul>
 * 
 * @author henrikbe
 * 
 * @param <T>
 */
@SuppressWarnings("unused") // Denna rad ska plockas bort. Den finns här
							// tillfälligt för att vi inte ska tro att det är
							// fel i koden. Varningar ska normalt inte döljas på
							// detta sätt, de är (oftast) fel som ska fixas.
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