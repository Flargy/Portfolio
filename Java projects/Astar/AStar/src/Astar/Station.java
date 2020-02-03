package Astar;

import java.util.*;
import java.util.Map.Entry;

public class Station implements Comparable<Station>{
	
	private Map<Station, Connections> connections = new HashMap<Station, Connections>();
	private double heuristic;
	private double x;
	private double y;
	private Station previousStation = null;
	private double timeCost= 0;
	private String name;

	public Station (double xp, double yp,String n) {
		this.x = xp;
		this.y = yp;
		this.name = n;
		
	}
	
	public Station getPreviousStation() {
		return previousStation;
	}

	public void updatePrevious(Station n) {
		previousStation = n;
	}
	
	public void departureTime(Station n) {
		double travelTime = n.timeCost+ n.connections.get(this).getWeight();

		timeCost = travelTime;
		
	}
	
	public void waitTime(Station nextStation, int minutes) {
		
		
		

		int costToInt = (int) timeCost;
		int wait = (int) connections.get(nextStation).getTree().ceilingEntry(costToInt + minutes).getValue() - (minutes + costToInt);
		nextStation.timeCost += wait;

	}
	
	public void firstDeparture(Station nextStation, int minutes) {
		timeCost = (int) connections.get(nextStation).getTree().ceilingEntry(minutes).getValue() - minutes;
		
	}

	
	public double checkWeight(Station n) {
		return getConnections().get(n).getWeight();
	}

	public double getX() {
		return x;
	}
	public double getY() {
		return y;
	}
	public String getName() {
		return name;
	}

	public void createConnections(Station n, int distance,int departureDelay) {
		Connections con = new Connections(distance, departureDelay);
		getConnections().put(n, con);
	}

	public void removeConnection(Station n) {
		getConnections().remove(n);
	}

	public void distance(Collection<Station> nodeList) {
		double tempX = 0;
		double tempY = 0;

		for (Station n : nodeList) {
			if (!n.name.equals(this.name)) {
				tempX = this.getX() - n.getX();
				tempY = this.getY() - n.getY();
				n.heuristic = (Math.sqrt((tempX * tempX) + (tempY * tempY))) / 2.3;
			}
			
		}

	}

	public double getCost() {
		return timeCost;
	}
	public double getHeuristic() {
		return heuristic;
	}

	@Override
	public int compareTo(Station n) {
		
		return (int) ((timeCost + heuristic) - (n.timeCost - n.heuristic));
	}

	public Map<Station, Connections> getConnections() {
		return connections;
	}
	

	public void setConnections(Station n, Connections c) {
		this.connections.put(n, c);
	}
	
	@Override
	public int hashCode() {
		int hashVal = 0;
		
		for (int i = 0; i < name.length(); i++)
			hashVal = 23 * hashVal + name.charAt(i);
		hashVal += (int) (x * 23) + (int) (y * 23);
		return hashVal;
	}
	

}
