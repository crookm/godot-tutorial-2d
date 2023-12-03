# Godot Tutorial 2D

My version of the [Godot 2D tutorial](https://docs.godotengine.org/en/stable/getting_started/first_2d_game/index.html),
following along from scratch.

### Requirements

I set out to follow the tutorial with some slight deviations, to learn about Godot and see if it meets my requirements -
which are:

* C# for scripting
    * Proper `async` usage
    * Familiar framework (common .NET methods and libraries)
* Able to structure the project in a .NET solution of my own design
* Easy to pick-up from a programmer background
* Support for desktop, mobile, and web publishing
* Linux-first development
    * Tight integration with JetBrains Rider

### Findings

Godot looks like it will be a great project for me to use as it meets most of my requirements, perhaps even for more
than just games.

I found that in the C# version, there are many custom methods and classes that are similar-but-not the same as some of
the ones built into .NET - but these didn't feel too alien to me, they were similar to what you would find in any other
.NET library. It looked tricky, but I also think I may be able to do dependency injection, too.

One limitation I found that does not meet my requirements is that when using C# and Godot 4(.2), I am not able to
publish to the web - version 3 does not have this limitation, however I don't want to begin as a new Godot user
with the previous version. This appears to be a limitation of .NET at the moment, but is on the radar and intends to be
supported as soon as possible by the team: godotengine/godot#70796