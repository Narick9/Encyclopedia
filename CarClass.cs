using System;


class Car
{
    public string PetName { get; set; }
    public int CurrentSpeed { get; set; }
    public int MaxSpeed { get; set; }


    public Car() { }
    public Car(string petName, int currentSpeed, int maxSpeed)
    {
        PetName = petName;
        CurrentSpeed = currentSpeed;
        MaxSpeed = maxSpeed;
    }


    public override string ToString() => $"{PetName} is going {CurrentSpeed}";
}