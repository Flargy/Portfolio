package Astar;

import java.util.*;

public class Connections {
	private TreeMap<Integer, Integer> departures;
	private double timeCost;


	public Connections(int distance, int departureDelay) {
		timeCost = distance;
		departures = new TreeMap<Integer, Integer>();
		addDepartures(departureDelay);
	}

	public void setDepartures(int departureDelay) {
		this.departures.put(departureDelay, departureDelay);
	}
	
	public void addDepartures(int departureDelay) {
		for(int i = 0; i < 1440; i+=50+departureDelay) {
			this.setDepartures(i);
		}
	}
	
	public void setCost(double newDistance) {
		timeCost = newDistance;
	}
	
	public double getWeight() {
		return timeCost;
	}
	
	public TreeMap getTree() {
		return departures;
	}
}

