package Star;

import java.util.*;
import java.util.Map.Entry;

public class Vertex implements Comparable<Vertex>{
	
	private Map<Vertex, Connections> connections = new HashMap<Vertex, Connections>();
	private boolean known;
	private double heuristic;
	private double x;
	private double y;
	private Vertex previous = null;
	private double cost= 0;

	public Vertex (double xp, double yp) {
		known = false;
		this.x = xp;
		this.y = yp;
		
	}
	
	public Vertex getPrevious() {
		return previous;
	}

	public void path(Vertex n) {
		previous = n;
	}
	public boolean known() {
		return known;
	}
	public void departureTime(Vertex n, int minutes) {
		double travelTime =n.cost+ n.connections.get(this).getWeight();

		cost += travelTime;
	}
	
	public void waitTime(Vertex n, int minutes) {
		int costToInt = (int) n.cost;
		int wait = (int) connections.get(n).getTree().ceilingEntry(costToInt + minutes).getValue() - (minutes + costToInt);
		cost += wait + costToInt;
	}
	
	public void firstDeparture(Vertex n, int minutes) {
		System.out.println("before");
		cost = (int) connections.get(n).getTree().ceilingEntry(minutes).getValue() - minutes;
		System.out.println(cost);
		System.out.println(heuristic);
	}

	
	public double checkWeight(Vertex n) {
		return getConnections().get(n).getWeight();
	}

	public double getX() {
		return x;
	}
	public double getY() {
		return y;
	}

	public void createConnections(Vertex n, int i,int s) {
		Connections con = new Connections(i, s);
		getConnections().put(n, con);
	}

	public void removeConnection(Vertex n) {
		getConnections().remove(n);
	}

	public void distance(Collection<Vertex> nodeList) {
		double tempX = 0;
		double tempY = 0;

		for (Vertex n : nodeList) {
			if (!n.equals(this)) {
				tempX = this.getX() - n.getX();
				tempY = this.getY() - n.getY();
			}
			n.heuristic = (Math.sqrt((tempX * tempX) + (tempY * tempY))) / 2.3;

		}

	}

	public double getCost() {
		return cost;
	}

//	public boolean equals(Vertex n) {
//		
//	}
	@Override
	public int compareTo(Vertex n) {
		
		return (int) ((cost + heuristic) - (n.cost - n.heuristic));
	}

	public Map<Vertex, Connections> getConnections() {
		return connections;
	}
	

	public void setConnections(Vertex n, Connections c) {
		this.connections.put(n, c);
	}
	
	

}
