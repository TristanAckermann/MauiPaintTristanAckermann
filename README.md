# MauiPaint - It just works.

This is a painting application built with .NET MAUI. It doesn't have a million features you don't need. It lets you draw, manage layers, and save your work. 

If you want something bloated, go elsewhere. This is for people who want to draw on a digital canvas without the BS.

## How to build
Don't break the build. Use the standard .NET CLI or your IDE of choice.
```bash
dotnet build
dotnet run
```

## Architecture
I've added some diagrams so you don't have to guess how the code is structured. If you can't read code, read these.

### Class Diagram
How things are connected. Singleton Service, ViewModels, Models. Simple.
![Class Diagram](MauiPaintTristanAckermann/Resources/Images/class_diagram.png)

### Export Process (Sequence)
This is how data flows when you save or export. It's linear. Try not to mess it up.
![Sequence Diagram](MauiPaintTristanAckermann/Resources/Images/sequence_diagram.png)

### Image Lifecycle (State)
A picture goes from empty to saved. It's not rocket science.
![State Diagram](MauiPaintTristanAckermann/Resources/Images/state_diagram.png)

## License
Do whatever you want with it, just don't blame me if it breaks.
