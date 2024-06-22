string awsFilePath = $"C:/users/{Environment.UserName}/.aws/credentials";
var profile = args.FirstOrDefault() ?? "default";

Console.WriteLine($"Overriding {profile} profile credentials");

List<string> input = new List<string>();
while (Console.ReadLine() is { } line && line != "")
{
    input.Add(line);
}

var allLines = File.ReadAllLines(awsFilePath);
var targetProfile = allLines.FirstOrDefault(x => x == $"[{profile}]");

if (targetProfile is not null)
{
    var startIndex = Array.IndexOf(allLines, targetProfile) + 1;
    foreach (var value in input.Skip(1))
    {
        allLines[startIndex++] = value;
    }
}
else
{
    var newProfileData = new List<string> { Environment.NewLine, $"[{profile}]" };
    newProfileData.AddRange(input.Skip(1));

    allLines = allLines.Concat(newProfileData).ToArray();
}

Console.WriteLine($"New content: \n{string.Join($"{Environment.NewLine}", allLines)}");
File.WriteAllLines(awsFilePath, allLines);



