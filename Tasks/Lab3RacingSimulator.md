## Racing Simulator
### Laboratory work №3

### [Solution](https://github.com/annchous/OopLabs/tree/lab3/OopLabs/RacingSimulator)

### [Tests](https://github.com/annchous/OopLabs/tree/lab3/OopLabs/RacingSimulatorTest)

There are several vehicles in the simulator:

* Bactrian Camel;
* Speed Camel;
* Centaur;
* All-Terrain Boots;
* Magic Carpet;
* Mortar;
* Broom.

You can add your own types.

Moreover, all types of vehicles are divided into two classes:

1. Ground;
2. Air.

Land types of transport have their own characteristics:

* **Speed**, in conventional terms;
* **Time of movement before rest**, in conventional units;
* **The duration of the rest**, in conventional units, is set by the formula (depends on the number of the stop in the account).

Air types of transport have their own characteristics:

* **Speed**, in conventional terms;
* **The coefficient of distance reduction** due to flights, in % of the distance, is given by the formula (depends on the distance).

There are several types of races in the simulator:

1. Only for land transport;
2. For air transport only;
3. For any type of transport.

The engine should be able to:

1. Create a race;
2. Register a vehicle for the race in accordance with the permissible class of vehicles (it is not possible to register an air vehicle for a race only for land vehicles and vice versa);
3. Start the race (determine the winner).


### Code description

#### Vehicle.cs

An abstract class describing the general properties and methods of vehicles: ``Speed`` and ``Run``.

#### AirVehicle.cs

An abstract derived class that describes the behavior of air vehicles. Contains the method ``DistanceReducer`` and ``Run`` method implementation of the base class.

#### LandVehicle.cs

An abstract derived class that describes the behavior of land vehicles. Contains the property ``MoveTime``, method ``RestDuration`` and ``Run`` method implementation of the base class.

#### [RaceParticipants](https://github.com/annchous/OopLabs/tree/master/OopLabs/RacingSimulator/Implementations/RaceParticipants)

A set of realizations of different types of vehicles specified in the task condition. Depending on the type, the class inherits from an abstract class ``AirVehicle`` or an abstract class ``LandVehicle``.

#### Race.cs

Generic class that implements the race.
The race constructor accepts the length of the distance and the list of participants:
```
public Race(int distance, List<T> participantsList)
```
The race starts with a method ``RunRace`` that returns a winner:
```
public Vehicle RunRace()
```

#### Program.cs

Main class with test run race. Nothing more to say.
