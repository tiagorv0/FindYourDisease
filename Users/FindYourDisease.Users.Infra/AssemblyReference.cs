using System.Reflection;

namespace FindYourDisease.Users.Infra;
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
