package Star;

import java.util.*;

public class Connections {
	private TreeMap<Integer, Integer> departures;
	private double cost;


	public Connections(int i, int d) {
		cost = i;
		departures = new TreeMap<Integer, Integer>();
		addDepartures(d);
	}

	public void setDepartures(int i) {
		this.departures.put(i, i);
	}
	
	public void addDepartures(int d) {
		for(int i = 0; i < 1440; i+=50+d) {
			this.setDepartures(i);
		}
	}
	
	public void setCost(double d) {
		cost = d;
	}
	
	public double getWeight() {
		return cost;
	}
	
	public TreeMap getTree() {
		return departures;
	}
}

