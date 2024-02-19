public static void Main(string[] args) {
  var random = new Random();
  var teams = new Team[20];
  for (int i = 0; i < teams.Length; i++) {
    teams[i] = new Team(random.Next(100));
    Console.WriteLine($"Team: {i} Score: {teams[i].Score}");
  }

  Console.WriteLine("");

  var sorted = teams.OrderByDescending(t => t.Score).ToArray();
  var sum = 0;
  for (int i = 0; i < 3; i++) {
    Console.WriteLine($"Team: {i} Score: {sorted[i].Score}");
    sum += sorted[i].Score;
  }

  Console.WriteLine(sum);
}

public struct Team {
  public int Score;

  public Team(int score) { Score = score; }
}
