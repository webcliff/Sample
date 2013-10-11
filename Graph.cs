public class Edge {
	public int To { get; set;}
	public int Weight { get; set;}
	public Edge Next { get; set; }
}

public enum Color { White,Gray,Black };

public class State {
	public int Time;
	public Color[] Colors;
	public int[] DTimes;
	public int[] FTimes;
	public int[] Distances;
	public int[] Preds; // precedents

	public static void Init(int nodeCnt) {
		Colors = new Color[nodeCnt];
		DTimes = new int[nodeCnt];
		FTimes = new int[nodeCnt];
		Distances = new int[nodeCnt];
		Preds = new int[nodeCnt];
	}
}

public class Graph {

	private Edge[] mNodes;

	public Graph(int nodeCnt) {
		mNodes = new Edge[nodeCnt];
	}
	public Edge GetEdge(int node) {
		return mNodes[node];
	}
	public int NodeCount() {
		return mNodes.Length;
	}
	public void AddEdge(int from, int to, int weight) {
		Edge e = new Edge { To=to;Weight=weight };
		if(mNodes[from]!=null) {
			e.Next=mNodes[from];
		}
		mNodes[from]=e;
	}
}

	public void BFS(Graph g, State s, int x) {
		s.Init(g.NodeCount);
		Queue<int> q = new Queue<int>();

		q.Enqueue(x);
		s.Colors[x]=Color.Gray;
		s.Distances[x]=0;

		while(q.Count>0) {
			int v = q.Dequeue();
			Edge e = g.GetEdge(v);
			while(e!=null) {
				if(s.Colors[e.To]==Color.White) {
					q.Enqueue(e.To);
					s.Colors[e.To]=Color.Gray;
					s.Distances[e.To]=s.Distance[v]+e.Weight;
					s.Preds[e.To]=v;
				}

				e=e.Next;
			}
			s.Colors[v]=Color.Black;
		}
	}

	public void DFS(Graph g, State s, int x) {
		s.Time+=1;
		s.DTimes[x]=s.Time;
		s.Colors[x]=Color.Gray;

		Edge e=g.GetEdge(x);
		while(e!=null) {
			int n=e.To;
			if(s.Colors[n]==Color.White) {
				s.Preds[n]=x;
				DFS(g,s,n);
			}
			e=e.Next;
		}
		s.Colors[x]=Color.Black;
		s.Time+=1;
		s.FTimes[x]=s.Time;
	}

	public void TopologicalSort(Graph g) {
		State state = new State();
		state.Init(g.NodeCount);
		Stack<int> s = new Stack<int>(g.NodeCount);
		
		for(int i=0;i<g.NodeCount;++i) {
			if(s.Colors[i]==Color.White) Visit(g,state,s,i);
		}

		DumpStack(s);
	}

	private void Visit(Graph g, State state, Stack<int> s, int i) {
		state.Colors[i]=Color.Gray;
		Edge e = g.GetEdge(i);
		while(e!=null) {
			if(state.Colors[e.To]==Color.White) Visit(g,state,s,e.To);
			e=e.Next;
		}
		State.Colors[i] = Color.Black;
		s.Push(i);
	}


	public void PrintPath(State s, int x, int y) {
		if(x==y) return;
		PrintPath(s,x,s.Preds[y]);
		Console.WriteLine("{0}->",y);
	}

