# MauiPaint - No Bullshit Painting.

A minimal, robust .NET MAUI painting application. Focuses on core functionality: drawing, layers, and persistence. 

Built for users who just want to draw. No bloat.

## Features
- **Layers:** Add, switch, and merge layers seamlessly.
- **Persistence:** SQLite integration ensures your work survives app restarts.
- **User Sessions:** Simple name-based login to separate workspaces.
- **Undo/Redo:** Basic history management.
- **Dark Mode:** Easy on the eyes.

## Build
```bash
dotnet build
dotnet run
```

## Architecture
Diagrams for the visually inclined.

### Class Diagram
Architecture overview. Singleton Service, ViewModels, SQLite Model.
![Class Diagram](MauiPaintTristanAckermann/Resources/Images/class_diagram.png)

### Save Process (Sequence)
Data flow from Canvas to SQLite Database.
![Sequence Diagram](MauiPaintTristanAckermann/Resources/Images/sequence_diagram.png)

### App Lifecycle (State)
Application states from Login to Persistent Storage.
![State Diagram](MauiPaintTristanAckermann/Resources/Images/state_diagram.png)

## License
MIT. Do whatever.