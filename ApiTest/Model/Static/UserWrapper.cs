using ApiTest.Model.Objects;

namespace ApiTest.Model.Static;

public static class UserWrapper
{
    private static readonly Random Random = new Random();
    
    public static readonly List<User> Users = [
        new User(1, "Guilherme", "48357875874"),
        new User(2, "Elisel", "75538486036"),
        new User(3, "Melina", "48357875274"),
        new User(4, "Tadeu", "00760178003"),
        new User(5, "Joana", "48357876874"),
        new User(6, "Melissa", "48757875874"),
        new User(7, "Malone", "48387875874"),
    ];
}